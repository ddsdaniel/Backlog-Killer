namespace BacklogKiller
{
    partial class FrmMain
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvSubstitutions = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAnalyze = new System.Windows.Forms.ToolStripButton();
            this.lblSubstitutions = new System.Windows.Forms.Label();
            this.txtProjectDirectoryRoot = new System.Windows.Forms.TextBox();
            this.btnOpenDirectoryDialog = new System.Windows.Forms.Button();
            this.lblRootDirectory = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubstitutions)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvSubstitutions
            // 
            this.dgvSubstitutions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSubstitutions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSubstitutions.Location = new System.Drawing.Point(12, 93);
            this.dgvSubstitutions.Name = "dgvSubstitutions";
            this.dgvSubstitutions.Size = new System.Drawing.Size(935, 335);
            this.dgvSubstitutions.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAnalyze});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(959, 25);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbAnalyze
            // 
            this.tsbAnalyze.Image = global::BacklogKiller.Properties.Resources.png_play_32_32;
            this.tsbAnalyze.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAnalyze.Name = "tsbAnalyze";
            this.tsbAnalyze.Size = new System.Drawing.Size(68, 22);
            this.tsbAnalyze.Text = "Analyze";
            this.tsbAnalyze.Click += new System.EventHandler(this.tsbAnalyze_Click);
            // 
            // lblSubstitutions
            // 
            this.lblSubstitutions.AutoSize = true;
            this.lblSubstitutions.Location = new System.Drawing.Point(9, 77);
            this.lblSubstitutions.Name = "lblSubstitutions";
            this.lblSubstitutions.Size = new System.Drawing.Size(67, 13);
            this.lblSubstitutions.TabIndex = 9;
            this.lblSubstitutions.Text = "Substitutions";
            // 
            // txtProjectDirectoryRoot
            // 
            this.txtProjectDirectoryRoot.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtProjectDirectoryRoot.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.txtProjectDirectoryRoot.Location = new System.Drawing.Point(12, 44);
            this.txtProjectDirectoryRoot.Name = "txtProjectDirectoryRoot";
            this.txtProjectDirectoryRoot.Size = new System.Drawing.Size(437, 20);
            this.txtProjectDirectoryRoot.TabIndex = 0;
            // 
            // btnOpenDirectoryDialog
            // 
            this.btnOpenDirectoryDialog.Location = new System.Drawing.Point(455, 42);
            this.btnOpenDirectoryDialog.Name = "btnOpenDirectoryDialog";
            this.btnOpenDirectoryDialog.Size = new System.Drawing.Size(32, 23);
            this.btnOpenDirectoryDialog.TabIndex = 1;
            this.btnOpenDirectoryDialog.Text = "...";
            this.btnOpenDirectoryDialog.UseVisualStyleBackColor = true;
            this.btnOpenDirectoryDialog.Click += new System.EventHandler(this.btnOpenDirectoryDialog_Click);
            // 
            // lblRootDirectory
            // 
            this.lblRootDirectory.AutoSize = true;
            this.lblRootDirectory.Location = new System.Drawing.Point(9, 28);
            this.lblRootDirectory.Name = "lblRootDirectory";
            this.lblRootDirectory.Size = new System.Drawing.Size(104, 13);
            this.lblRootDirectory.TabIndex = 13;
            this.lblRootDirectory.Text = "Project root directory";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 440);
            this.Controls.Add(this.lblRootDirectory);
            this.Controls.Add(this.btnOpenDirectoryDialog);
            this.Controls.Add(this.txtProjectDirectoryRoot);
            this.Controls.Add(this.lblSubstitutions);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dgvSubstitutions);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Backlog Killer - DDS Sistemas";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubstitutions)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSubstitutions;        
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Label lblSubstitutions;
        private System.Windows.Forms.ToolStripButton tsbAnalyze;
        private System.Windows.Forms.TextBox txtProjectDirectoryRoot;
        private System.Windows.Forms.Button btnOpenDirectoryDialog;
        private System.Windows.Forms.Label lblRootDirectory;
    }
}

