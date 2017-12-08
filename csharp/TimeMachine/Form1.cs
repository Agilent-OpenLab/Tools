using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Agilent.OpenLab.SharedServices;
using Agilent.OpenLab.SharedServices.Login;
using Agilent.OpenLab.Storage;
using Agilent.OpenLab.Storage.Versioned;

namespace TimeMachine
{
	public partial class Form1 : Form
	{
		private readonly Connection _connection;
		private readonly StorageConnection _storageConnection;
		private DateTime? _selectedRevision;

		public Form1()
		{
			InitializeComponent();
			var loginService = new LoginService();
			_connection = loginService.Login();
			_storageConnection = new StorageConnection(_connection.Storage.ConnectionParameters);

			Text = string.Format(@"{0} - {1}", Text, _connection.ConnectionUri.Host);
			_btnDownloadLatest.Enabled = false;
			_btnDownloadSelected.Enabled = false;
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			Cursor = Cursors.WaitCursor;
			try
			{
				var repo = _storageConnection.Get<IRepository>();
				var rootNode = new TreeNode("Root");
				PopulateTree(repo, _connection.Storage.RootPath, rootNode);
				_treeView.Nodes.Add(rootNode);
			}
			finally
			{
				Cursor = DefaultCursor;
			}
		}

		private void PopulateTree(IRepository repo, UnifiedPath path, TreeNode rootNode)
		{
			FolderInfo[] folders = repo.GetFolders(path);
			foreach (var folder in folders)
			{
				var newNode = new TreeNode(folder.Name) {Tag = folder.Path};
				PopulateTree(repo, folder.Path, newNode);
				rootNode.Nodes.Add(newNode);
			}
		}

		private IEnumerable<UnifiedPath> GetFilesRecursively(IRepository repository, UnifiedPath root)
		{
			foreach (var file in repository.GetFiles(root))
			{
				yield return file.Path;
			}
			foreach (var folder in repository.GetFolders(root))
			{
				foreach (var path in GetFilesRecursively(repository, folder.Path))
				{
					yield return path;
				}
			}
		}

		private string GetRelativePath(IPathHelper pathHelper, UnifiedPath root, UnifiedPath path)
		{
			int rootParts = pathHelper.Split(root).Length;
			return string.Join(pathHelper.PathSeparator.ToString(), pathHelper.Split(path).Skip(rootParts));
		}

		private void RefreshClick(object sender, EventArgs e)
		{
			_revisionList.Items.Clear();
			Cursor = Cursors.WaitCursor;
			try
			{
				var repo = _storageConnection.Get<IVersionedRepository>();
				if (repo == null)
				{
					MessageBox.Show(@"Repository does not support versioning");
					return;
				}
				_revisionList.Items.Clear();
				var node = _treeView.SelectedNode;
				if (node == null)
				{
					return;
				}
				var path = node.Tag as UnifiedPath;
				if (path == null)
				{
					return;
				}
				var filePaths = GetFilesRecursively(repo, path);//repo.GetFiles(path).Select(fi => fi.Path);
				var changeList = new List<Change>();
				foreach (var filePath in filePaths)
				{
					FileRevisionInfo[] history = repo.GetFileHistory(filePath);
					foreach (var revisionInfo in history)
					{
						if (!revisionInfo.Created.HasValue)
						{
							continue;
						}
						changeList.Add(new Change(revisionInfo));
					}
				}
				var itemGroups = changeList
					.OrderByDescending(x => x.Time)
					.GroupBy(x => x.Time.Date);
				var items = new List<ListViewItem>();
				foreach (var group in itemGroups)
				{
					DateTime endOfDay = group.Key.AddDays(1).AddMilliseconds(-1);
					items.Add(new ListViewItem(group.Key.ToShortDateString()) {BackColor = Color.Silver, Tag = endOfDay});
					foreach (var item in group)
					{
						items.Add(new ListViewItem(new[] { item.Time.ToLongTimeString(), item.Revision, GetRelativePath(repo, path, item.Path) }) { Tag = item.Time });
					}
				}
				_revisionList.Items.AddRange(items.ToArray());
				_btnDownloadLatest.Enabled = true;
				_btnDownloadSelected.Enabled = false;
			}
			finally
			{
				Cursor = DefaultCursor;
			}
		}

		private void RevisionSelected(object sender, EventArgs e)
		{
			if (_revisionList.SelectedItems.Count > 0)
			{
				_btnDownloadSelected.Enabled = _btnDownloadLatest.Enabled;
				_selectedRevision = _revisionList.SelectedItems[0].Tag as DateTime?;
			}
			else
			{
				_btnDownloadSelected.Enabled = false;
				_selectedRevision = null;
			}
		}

		private void DownloadRecursive(IVersionedRepository repository, UnifiedPath source, string target, DateTime? upToDateTime = null)
		{
			var files = repository.GetFiles(source);
			foreach (var file in files)
			{
				var history = repository.GetFileHistory(file.Path);
				var revision = history
					.Where(r => r.Created.HasValue && (upToDateTime == null || r.Created.Value <= upToDateTime.Value))
					.OrderByDescending(r => r.Created.Value)
					.FirstOrDefault();
				if (revision != null)
				{
					Directory.CreateDirectory(target);
					repository.EndTransfer(repository.BeginDownloadVersionTransfer(revision.RevisionPath, target, true, null));
					// TODO: Investigate
					//File.SetLastWriteTime(Path.Combine(target, file.Name), revision.Created.Value);
				}
			}
			var folders = repository.GetFolders(source);
			foreach (var folder in folders)
			{
				string targetFolder = Path.Combine(target, folder.Name);
				DownloadRecursive(repository, folder.Path, targetFolder, upToDateTime);
			}
		}

		private void DownloadRevision(object sender, EventArgs e)
		{
			if (!_selectedRevision.HasValue)
			{
				return;
			}
			var node = _treeView.SelectedNode;
			if (node == null)
			{
				return;
			}
			var path = node.Tag as UnifiedPath;
			if (path == null)
			{
				return;
			}
			var repo = _storageConnection.Get<IVersionedRepository>();
			using (var dialog = new FolderBrowserDialog())
			{
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					DownloadRecursive(repo, path, dialog.SelectedPath, _selectedRevision);
				}
			}
		}

		private void DownloadLatest(object sender, EventArgs e)
		{
			var node = _treeView.SelectedNode;
			if (node == null)
			{
				return;
			}
			var path = node.Tag as UnifiedPath;
			if (path == null)
			{
				return;
			}
			var repo = _storageConnection.Get<IVersionedRepository>();
			using (var dialog = new FolderBrowserDialog())
			{
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					DownloadRecursive(repo, path, dialog.SelectedPath);
				}
			}
		}
	}

	class Change
	{
		public DateTime Time { get; private set; }
		public UnifiedPath Path { get; private set; }
		public string Revision { get; private set; }

		public Change(FileRevisionInfo revisionInfo)
		{
			if (!revisionInfo.Created.HasValue)
			{
				throw new ArgumentException();
			}
			Time = revisionInfo.Created.Value;
			Path = revisionInfo.RevisionPath;
			Revision = revisionInfo.RevisionPath.VersionLabel;
		}
	}
}
