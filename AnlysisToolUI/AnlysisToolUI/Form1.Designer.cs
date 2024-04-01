namespace AnlysisToolUI
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thresholdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analyzeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.histogramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripRect = new System.Windows.Forms.ToolStripButton();
            this.toolStripMagnify = new System.Windows.Forms.ToolStripButton();
            this.toolStripScroll = new System.Windows.Forms.ToolStripButton();
            this.lb_inform = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.imageToolStripMenuItem,
            this.analyzeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(423, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // imageToolStripMenuItem
            // 
            this.imageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.thresholdToolStripMenuItem});
            this.imageToolStripMenuItem.Name = "imageToolStripMenuItem";
            this.imageToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.imageToolStripMenuItem.Text = "Image";
            // 
            // thresholdToolStripMenuItem
            // 
            this.thresholdToolStripMenuItem.Name = "thresholdToolStripMenuItem";
            this.thresholdToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.thresholdToolStripMenuItem.Text = "Threshold";
            this.thresholdToolStripMenuItem.Click += new System.EventHandler(this.thresholdToolStripMenuItem_Click);
            // 
            // analyzeToolStripMenuItem
            // 
            this.analyzeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.histogramToolStripMenuItem});
            this.analyzeToolStripMenuItem.Name = "analyzeToolStripMenuItem";
            this.analyzeToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.analyzeToolStripMenuItem.Text = "Analyze";
            // 
            // histogramToolStripMenuItem
            // 
            this.histogramToolStripMenuItem.Name = "histogramToolStripMenuItem";
            this.histogramToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.histogramToolStripMenuItem.Text = "Histogram";
            this.histogramToolStripMenuItem.Click += new System.EventHandler(this.histogramToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripRect,
            this.toolStripMagnify,
            this.toolStripScroll});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(423, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripRect
            // 
            this.toolStripRect.Checked = true;
            this.toolStripRect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripRect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripRect.Image = ((System.Drawing.Image)(resources.GetObject("toolStripRect.Image")));
            this.toolStripRect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripRect.Name = "toolStripRect";
            this.toolStripRect.Size = new System.Drawing.Size(23, 22);
            this.toolStripRect.Text = "Rect";
            this.toolStripRect.ToolTipText = "toolStripRect";
            this.toolStripRect.Click += new System.EventHandler(this.toolStripRect_Click);
            this.toolStripRect.MouseEnter += new System.EventHandler(this.toolStripButton1_MouseEnter);
            // 
            // toolStripMagnify
            // 
            this.toolStripMagnify.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripMagnify.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMagnify.Image")));
            this.toolStripMagnify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMagnify.Name = "toolStripMagnify";
            this.toolStripMagnify.Size = new System.Drawing.Size(23, 22);
            this.toolStripMagnify.Text = "Magnify";
            this.toolStripMagnify.ToolTipText = "toolStripMagnify";
            this.toolStripMagnify.Click += new System.EventHandler(this.toolStripMagnify_Click);
            this.toolStripMagnify.MouseEnter += new System.EventHandler(this.toolStripMagnify_MouseEnter);
            // 
            // toolStripScroll
            // 
            this.toolStripScroll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripScroll.Image = ((System.Drawing.Image)(resources.GetObject("toolStripScroll.Image")));
            this.toolStripScroll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripScroll.Name = "toolStripScroll";
            this.toolStripScroll.Size = new System.Drawing.Size(23, 22);
            this.toolStripScroll.Text = "Scroll";
            this.toolStripScroll.ToolTipText = "toolStripScroll";
            this.toolStripScroll.Click += new System.EventHandler(this.toolStripScroll_Click);
            this.toolStripScroll.MouseEnter += new System.EventHandler(this.toolStripScroll_MouseEnter);
            // 
            // lb_inform
            // 
            this.lb_inform.AutoSize = true;
            this.lb_inform.Font = new System.Drawing.Font("굴림", 10F);
            this.lb_inform.Location = new System.Drawing.Point(-2, 51);
            this.lb_inform.Name = "lb_inform";
            this.lb_inform.Size = new System.Drawing.Size(0, 14);
            this.lb_inform.TabIndex = 2;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(423, 70);
            this.Controls.Add(this.lb_inform);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Image Analysis Tool";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripRect;
        private System.Windows.Forms.ToolStripButton toolStripMagnify;
        private System.Windows.Forms.ToolStripButton toolStripScroll;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem analyzeToolStripMenuItem;
        public System.Windows.Forms.Label lb_inform;
        public System.Windows.Forms.ToolStripMenuItem histogramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thresholdToolStripMenuItem;
    }
}

