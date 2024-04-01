namespace AnlysisToolUI
{
    partial class ImageForm
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
            this.lb_ImageInform = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.panel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // lb_ImageInform
            // 
            this.lb_ImageInform.AutoSize = true;
            this.lb_ImageInform.Font = new System.Drawing.Font("굴림", 10F);
            this.lb_ImageInform.Location = new System.Drawing.Point(2, 2);
            this.lb_ImageInform.Name = "lb_ImageInform";
            this.lb_ImageInform.Size = new System.Drawing.Size(45, 14);
            this.lb_ImageInform.TabIndex = 0;
            this.lb_ImageInform.Text = "label1";
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(-1, -1);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(232, 192);
            this.pictureBox.TabIndex = 1;
            this.pictureBox.TabStop = false;
            this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            this.pictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseClick);
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pictureBox.MouseEnter += new System.EventHandler(this.pictureBox_MouseEnter);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // panel
            // 
            this.panel.Controls.Add(this.pictureBox);
            this.panel.Location = new System.Drawing.Point(5, 19);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(250, 226);
            this.panel.TabIndex = 2;
            // 
            // ImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 259);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.lb_ImageInform);
            this.Name = "ImageForm";
            this.Text = "Form2";
            this.Activated += new System.EventHandler(this.ImageForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImageForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ImageForm_KeyDown);
            this.Resize += new System.EventHandler(this.ImageForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_ImageInform;
        private System.Windows.Forms.Panel panel;
        public System.Windows.Forms.PictureBox pictureBox;
    }
}