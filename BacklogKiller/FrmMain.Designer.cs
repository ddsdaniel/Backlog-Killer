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
            this.tsbAnalisar = new System.Windows.Forms.ToolStripButton();
            this.label3 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stsStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtProjectDirectoryRoot = new System.Windows.Forms.TextBox();
            this.btnOpenDirectoryDialog = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubstitutions)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
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
            this.dgvSubstitutions.Size = new System.Drawing.Size(935, 315);
            this.dgvSubstitutions.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAnalisar});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(959, 25);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbAnalisar
            // 
            this.tsbAnalisar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAnalisar.Name = "tsbAnalisar";
            this.tsbAnalisar.Size = new System.Drawing.Size(53, 22);
            this.tsbAnalisar.Text = "Analisar";
            this.tsbAnalisar.ToolTipText = "Copia todos os arquivos e aplica todas as substituições";
            this.tsbAnalisar.Click += new System.EventHandler(this.tsbAnalisar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Substituições";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stsStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 418);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(959, 22);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // stsStatus
            // 
            this.stsStatus.Name = "stsStatus";
            this.stsStatus.Size = new System.Drawing.Size(100, 17);
            this.stsStatus.Text = "Mensagem inicial";
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Project directory root";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 440);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOpenDirectoryDialog);
            this.Controls.Add(this.txtProjectDirectoryRoot);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dgvSubstitutions);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Multiple Search, Copy and Replace Files - DDS Sistemas";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubstitutions)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSubstitutions;        
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripButton tsbAnalisar;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel stsStatus;
        private System.Windows.Forms.TextBox txtProjectDirectoryRoot;
        private System.Windows.Forms.Button btnOpenDirectoryDialog;
        private System.Windows.Forms.Label label1;
    }
}

