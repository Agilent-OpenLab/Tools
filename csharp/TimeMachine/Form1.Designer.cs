namespace TimeMachine
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.ColumnHeader clmDateTime;
			System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			System.Windows.Forms.ColumnHeader clmVersion;
			System.Windows.Forms.ColumnHeader clmFile;
			System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
			this._treeView = new System.Windows.Forms.TreeView();
			this._imageList = new System.Windows.Forms.ImageList(this.components);
			this._revisionList = new System.Windows.Forms.ListView();
			this._btnRefresh = new System.Windows.Forms.Button();
			this._btnDownloadLatest = new System.Windows.Forms.Button();
			this._btnDownloadSelected = new System.Windows.Forms.Button();
			clmDateTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			clmVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			clmFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			tableLayoutPanel1.SuspendLayout();
			flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// clmDateTime
			// 
			clmDateTime.Text = "Date/Time";
			clmDateTime.Width = 120;
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.ColumnCount = 3;
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.14888F));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.85112F));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
			tableLayoutPanel1.Controls.Add(this._treeView, 0, 0);
			tableLayoutPanel1.Controls.Add(this._revisionList, 1, 0);
			tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 2, 0);
			tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 1;
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			tableLayoutPanel1.Size = new System.Drawing.Size(937, 504);
			tableLayoutPanel1.TabIndex = 0;
			// 
			// _treeView
			// 
			this._treeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this._treeView.ImageIndex = 0;
			this._treeView.ImageList = this._imageList;
			this._treeView.Location = new System.Drawing.Point(3, 3);
			this._treeView.Name = "_treeView";
			this._treeView.SelectedImageIndex = 0;
			this._treeView.Size = new System.Drawing.Size(244, 498);
			this._treeView.TabIndex = 3;
			// 
			// _imageList
			// 
			this._imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_imageList.ImageStream")));
			this._imageList.TransparentColor = System.Drawing.Color.Transparent;
			this._imageList.Images.SetKeyName(0, "FolderIcon.png");
			// 
			// _revisionList
			// 
			this._revisionList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            clmDateTime,
            clmVersion,
            clmFile});
			this._revisionList.Dock = System.Windows.Forms.DockStyle.Fill;
			this._revisionList.FullRowSelect = true;
			this._revisionList.GridLines = true;
			this._revisionList.Location = new System.Drawing.Point(253, 3);
			this._revisionList.Name = "_revisionList";
			this._revisionList.Size = new System.Drawing.Size(575, 498);
			this._revisionList.TabIndex = 4;
			this._revisionList.UseCompatibleStateImageBehavior = false;
			this._revisionList.View = System.Windows.Forms.View.Details;
			this._revisionList.SelectedIndexChanged += new System.EventHandler(this.RevisionSelected);
			// 
			// clmVersion
			// 
			clmVersion.Text = "Version";
			// 
			// clmFile
			// 
			clmFile.Text = "File";
			clmFile.Width = 360;
			// 
			// flowLayoutPanel1
			// 
			flowLayoutPanel1.Controls.Add(this._btnRefresh);
			flowLayoutPanel1.Controls.Add(this._btnDownloadLatest);
			flowLayoutPanel1.Controls.Add(this._btnDownloadSelected);
			flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			flowLayoutPanel1.Location = new System.Drawing.Point(834, 3);
			flowLayoutPanel1.Name = "flowLayoutPanel1";
			flowLayoutPanel1.Size = new System.Drawing.Size(100, 498);
			flowLayoutPanel1.TabIndex = 5;
			// 
			// _btnRefresh
			// 
			this._btnRefresh.Location = new System.Drawing.Point(3, 3);
			this._btnRefresh.Name = "_btnRefresh";
			this._btnRefresh.Size = new System.Drawing.Size(94, 100);
			this._btnRefresh.TabIndex = 3;
			this._btnRefresh.Text = "Show history";
			this._btnRefresh.UseVisualStyleBackColor = true;
			this._btnRefresh.Click += new System.EventHandler(this.RefreshClick);
			// 
			// _btnDownloadLatest
			// 
			this._btnDownloadLatest.Location = new System.Drawing.Point(3, 109);
			this._btnDownloadLatest.Name = "_btnDownloadLatest";
			this._btnDownloadLatest.Size = new System.Drawing.Size(94, 100);
			this._btnDownloadLatest.TabIndex = 4;
			this._btnDownloadLatest.Text = "Download folder as of now";
			this._btnDownloadLatest.UseVisualStyleBackColor = true;
			this._btnDownloadLatest.Click += new System.EventHandler(this.DownloadLatest);
			// 
			// _btnDownloadSelected
			// 
			this._btnDownloadSelected.Location = new System.Drawing.Point(3, 215);
			this._btnDownloadSelected.Name = "_btnDownloadSelected";
			this._btnDownloadSelected.Size = new System.Drawing.Size(94, 100);
			this._btnDownloadSelected.TabIndex = 5;
			this._btnDownloadSelected.Text = "Download folder as of the selected time";
			this._btnDownloadSelected.UseVisualStyleBackColor = true;
			this._btnDownloadSelected.Click += new System.EventHandler(this.DownloadRevision);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(937, 504);
			this.Controls.Add(tableLayoutPanel1);
			this.Name = "Form1";
			this.Text = "Folder History";
			tableLayoutPanel1.ResumeLayout(false);
			flowLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.TreeView _treeView;
		private System.Windows.Forms.ListView _revisionList;
		private System.Windows.Forms.Button _btnRefresh;
		private System.Windows.Forms.Button _btnDownloadLatest;
		private System.Windows.Forms.Button _btnDownloadSelected;
		private System.Windows.Forms.ImageList _imageList;
	}
}

