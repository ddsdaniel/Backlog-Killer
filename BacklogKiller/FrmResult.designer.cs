namespace BacklogKiller
{
    partial class FrmResult
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabNewFiles = new System.Windows.Forms.TabPage();
            this.btnGenerateSelectedFiles = new System.Windows.Forms.Button();
            this.lvwNewFiles = new System.Windows.Forms.ListView();
            this.tabModifiedFiles = new System.Windows.Forms.TabPage();
            this.lvwModifiedFiles = new System.Windows.Forms.ListView();
            this.tabControl1.SuspendLayout();
            this.tabNewFiles.SuspendLayout();
            this.tabModifiedFiles.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabNewFiles);
            this.tabControl1.Controls.Add(this.tabModifiedFiles);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(775, 426);
            this.tabControl1.TabIndex = 12;
            // 
            // tabNewFiles
            // 
            this.tabNewFiles.Controls.Add(this.btnGenerateSelectedFiles);
            this.tabNewFiles.Controls.Add(this.lvwNewFiles);
            this.tabNewFiles.Location = new System.Drawing.Point(4, 22);
            this.tabNewFiles.Name = "tabNewFiles";
            this.tabNewFiles.Padding = new System.Windows.Forms.Padding(3);
            this.tabNewFiles.Size = new System.Drawing.Size(767, 400);
            this.tabNewFiles.TabIndex = 0;
            this.tabNewFiles.Text = "New files";
            this.tabNewFiles.UseVisualStyleBackColor = true;
            // 
            // btnGenerateSelectedFiles
            // 
            this.btnGenerateSelectedFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGenerateSelectedFiles.Location = new System.Drawing.Point(6, 371);
            this.btnGenerateSelectedFiles.Name = "btnGenerateSelectedFiles";
            this.btnGenerateSelectedFiles.Size = new System.Drawing.Size(161, 23);
            this.btnGenerateSelectedFiles.TabIndex = 11;
            this.btnGenerateSelectedFiles.Text = "Generate selected(s) file(s)";
            this.btnGenerateSelectedFiles.UseVisualStyleBackColor = true;
            this.btnGenerateSelectedFiles.Click += new System.EventHandler(this.btnGenerateSelectedFiles_Click);
            // 
            // lvwNewFiles
            // 
            this.lvwNewFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwNewFiles.HideSelection = false;
            this.lvwNewFiles.Location = new System.Drawing.Point(6, 6);
            this.lvwNewFiles.Name = "lvwNewFiles";
            this.lvwNewFiles.Size = new System.Drawing.Size(755, 359);
            this.lvwNewFiles.TabIndex = 10;
            this.lvwNewFiles.UseCompatibleStateImageBehavior = false;
            // 
            // tabModifiedFiles
            // 
            this.tabModifiedFiles.Controls.Add(this.lvwModifiedFiles);
            this.tabModifiedFiles.Location = new System.Drawing.Point(4, 22);
            this.tabModifiedFiles.Name = "tabModifiedFiles";
            this.tabModifiedFiles.Padding = new System.Windows.Forms.Padding(3);
            this.tabModifiedFiles.Size = new System.Drawing.Size(767, 400);
            this.tabModifiedFiles.TabIndex = 1;
            this.tabModifiedFiles.Text = "Modified files";
            this.tabModifiedFiles.UseVisualStyleBackColor = true;
            // 
            // lvwModifiedFiles
            // 
            this.lvwModifiedFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwModifiedFiles.HideSelection = false;
            this.lvwModifiedFiles.Location = new System.Drawing.Point(6, 6);
            this.lvwModifiedFiles.Name = "lvwModifiedFiles";
            this.lvwModifiedFiles.Size = new System.Drawing.Size(755, 388);
            this.lvwModifiedFiles.TabIndex = 12;
            this.lvwModifiedFiles.UseCompatibleStateImageBehavior = false;
            // 
            // FrmResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 450);
            this.Controls.Add(this.tabControl1);
            this.KeyPreview = true;
            this.Name = "FrmResult";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Results";
            this.Load += new System.EventHandler(this.FrmResult_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmResult_KeyDown);
            this.tabControl1.ResumeLayout(false);
            this.tabNewFiles.ResumeLayout(false);
            this.tabModifiedFiles.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabNewFiles;
        private System.Windows.Forms.ListView lvwNewFiles;        
        private System.Windows.Forms.TabPage tabModifiedFiles;
        private System.Windows.Forms.ListView lvwModifiedFiles;
        private System.Windows.Forms.Button btnGenerateSelectedFiles;
    }
}