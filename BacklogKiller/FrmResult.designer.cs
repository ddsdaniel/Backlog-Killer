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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lvwNewFiles = new System.Windows.Forms.ListView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lvwModifiedFiles = new System.Windows.Forms.ListView();
            this.btnGenerateSelectedFiles = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(775, 426);
            this.tabControl1.TabIndex = 12;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnGenerateSelectedFiles);
            this.tabPage1.Controls.Add(this.lvwNewFiles);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(767, 400);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "New files";
            this.tabPage1.UseVisualStyleBackColor = true;
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
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lvwModifiedFiles);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(767, 400);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Modified files";
            this.tabPage2.UseVisualStyleBackColor = true;
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
            // FrmResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 450);
            this.Controls.Add(this.tabControl1);
            this.KeyPreview = true;
            this.Name = "FrmResult";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Result";
            this.Load += new System.EventHandler(this.FrmResult_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmResult_KeyDown);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListView lvwNewFiles;        
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView lvwModifiedFiles;
        private System.Windows.Forms.Button btnGenerateSelectedFiles;
    }
}