
namespace GoogleDriveSender
{
    partial class MainView
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainView));
            this.progress = new System.Windows.Forms.ProgressBar();
            this.lbStatus = new System.Windows.Forms.Label();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSetting = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbResourcePath = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.pnlArgs = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlBottom.SuspendLayout();
            this.pnlArgs.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // progress
            // 
            this.progress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progress.Location = new System.Drawing.Point(12, 29);
            this.progress.MarqueeAnimationSpeed = 10;
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(404, 23);
            this.progress.TabIndex = 0;
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Location = new System.Drawing.Point(12, 11);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(38, 15);
            this.lbStatus.TabIndex = 1;
            this.lbStatus.Text = "label1";
            // 
            // tbPath
            // 
            this.tbPath.Location = new System.Drawing.Point(12, 75);
            this.tbPath.Name = "tbPath";
            this.tbPath.ReadOnly = true;
            this.tbPath.Size = new System.Drawing.Size(374, 23);
            this.tbPath.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(352, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(64, 24);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "閉じる";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnCopy
            // 
            this.btnCopy.Image = global::GoogleDriveSender.Properties.Resources.content_copy_16;
            this.btnCopy.Location = new System.Drawing.Point(392, 74);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(24, 24);
            this.btnCopy.TabIndex = 4;
            this.btnCopy.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "URL";
            // 
            // btnSetting
            // 
            this.btnSetting.Image = global::GoogleDriveSender.Properties.Resources.cog;
            this.btnSetting.Location = new System.Drawing.Point(12, 4);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(24, 24);
            this.btnSetting.TabIndex = 6;
            this.btnSetting.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "Resource Path";
            // 
            // tbResourcePath
            // 
            this.tbResourcePath.Location = new System.Drawing.Point(12, 27);
            this.tbResourcePath.Name = "tbResourcePath";
            this.tbResourcePath.Size = new System.Drawing.Size(404, 23);
            this.tbResourcePath.TabIndex = 7;
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point(352, 55);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(64, 24);
            this.btnSend.TabIndex = 9;
            this.btnSend.Text = "送信";
            this.btnSend.UseVisualStyleBackColor = true;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnClose);
            this.pnlBottom.Controls.Add(this.btnSetting);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 191);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(428, 32);
            this.pnlBottom.TabIndex = 10;
            // 
            // pnlArgs
            // 
            this.pnlArgs.Controls.Add(this.label2);
            this.pnlArgs.Controls.Add(this.tbResourcePath);
            this.pnlArgs.Controls.Add(this.btnSend);
            this.pnlArgs.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlArgs.Location = new System.Drawing.Point(0, 0);
            this.pnlArgs.Name = "pnlArgs";
            this.pnlArgs.Size = new System.Drawing.Size(428, 83);
            this.pnlArgs.TabIndex = 11;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tbPath);
            this.panel2.Controls.Add(this.progress);
            this.panel2.Controls.Add(this.lbStatus);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btnCopy);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 83);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(428, 108);
            this.panel2.TabIndex = 12;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 223);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlArgs);
            this.Controls.Add(this.pnlBottom);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GoogleDriveSender";
            this.pnlBottom.ResumeLayout(false);
            this.pnlArgs.ResumeLayout(false);
            this.pnlArgs.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progress;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSetting;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbResourcePath;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Panel pnlArgs;
        private System.Windows.Forms.Panel panel2;
    }
}

