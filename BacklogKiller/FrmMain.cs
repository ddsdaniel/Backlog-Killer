using BacklogKiller.ClassLibrary.Services;
using BacklogKiller.ClassLibrary.ValueObjects;
using BacklogKiller.ClassLibrary.ViewModels;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BacklogKiller
{
    //TODO: renomear form
    public partial class FrmMain : Form
    {
        private enum EnumColumns
        {
            Find = 0,
            ReplaceWith
        }

        //TODO: salvar no caminho temporário do usuário
        private const string FILE_FORM_STATUS = "form_status.xml";

        //TODO: resource string file
        private const string MENSAGEM_INICIAL = "Informe um diretório e uma máscara para procurar os arquivos";

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            //TODO: icon
            //Icon = Properties.Resources.magic_ico;
            stsStatus.Text = MENSAGEM_INICIAL;

            FormatDgvSubstitutions();
            RecoveryFormStatus();
        }

        private void RecoveryFormStatus()
        {
            var serializeService = new SerializeService<ConfigurationViewModel>();

            if (File.Exists(FILE_FORM_STATUS))
            {
                var configurationViewModel = serializeService.Deserialize(FILE_FORM_STATUS);
                txtProjectDirectoryRoot.Text = configurationViewModel.Directory;

                dgvSubstitutions.AllowUserToAddRows = false;
                dgvSubstitutions.AllowUserToDeleteRows = false;

                foreach (var subs in configurationViewModel.Substitutions)
                {
                    dgvSubstitutions.Rows.Add();

                    dgvSubstitutions.Rows[dgvSubstitutions.Rows.Count - 1].Cells[(int)EnumColumns.Find].Value = subs.Find;
                    dgvSubstitutions.Rows[dgvSubstitutions.Rows.Count - 1].Cells[(int)EnumColumns.ReplaceWith].Value = subs.ReplaceWith;
                }
            }

            dgvSubstitutions.AllowUserToAddRows = true;
            dgvSubstitutions.AllowUserToDeleteRows = true;
        }

        private void FormatDgvSubstitutions()
        {
            dgvSubstitutions.ColumnCount = 2;
            dgvSubstitutions.Columns[(int)EnumColumns.Find].HeaderText = "Localizar";
            dgvSubstitutions.Columns[(int)EnumColumns.Find].Width = (dgvSubstitutions.Width / 2) - 20;

            dgvSubstitutions.Columns[(int)EnumColumns.ReplaceWith].HeaderText = "Substituir por";
            dgvSubstitutions.Columns[(int)EnumColumns.ReplaceWith].Width = dgvSubstitutions.Columns[(int)EnumColumns.Find].Width;
        }

        private void SaveConfiguration()
        {
            if (File.Exists(FILE_FORM_STATUS))
                File.Delete(FILE_FORM_STATUS);

            var configuration = GetConfiguration();
            var configurationViewModel = new ConfigurationViewModel { Directory = configuration.ProjectDirectory.Path };
            foreach (var subs in configuration.Substitutions)
            {
                configurationViewModel.Substitutions.Add(
                    new ReplacementViewModel
                    {
                        Find = subs.Find,
                        ReplaceWith = subs.ReplaceWith
                    });
            }

            var serializeService = new SerializeService<ConfigurationViewModel>();
            serializeService.Serialize(configurationViewModel, FILE_FORM_STATUS);
        }

        private List<Replacement> GetSubstitutions()
        {
            var substitutions = new List<Replacement>();
            foreach (DataGridViewRow linha in dgvSubstitutions.Rows)
            {
                if (linha.IsNewRow)
                    continue;

                var subs = new Replacement(
                    find: linha.Cells[(int)EnumColumns.Find].Value.ToString(),
                    replaceWith: linha.Cells[(int)EnumColumns.ReplaceWith].Value.ToString()
                    );
                substitutions.Add(subs);
            }

            return substitutions;
        }

        private void tsbAnalisar_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                //TODO: loading gif
                stsStatus.Text = "Aplicando...";

                var configuration = GetConfiguration();
                var analiseService = new AnalyzeService(configuration);

                if (analiseService.Invalid)
                {
                    ShowNotifications(analiseService);
                }
                else
                {
                    SaveConfiguration();

                    var files = analiseService.GetFiles();
                    if (files.Count == 0)
                        MessageBox.Show("Nenhuma sugestão compatível com as configurações inseridas.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    else
                        ShowResult(files, analiseService);
                }

                //    stsStatus.Text = lvwArquivos.CheckedItems.Count + " arquivo(s) gerado(s) instantaneamente, rápido não? Eu sei, de nada!";
                stsStatus.Text = "Pronto";
            }
            catch (Exception erro)
            {
                MessageBox.Show($"{erro.Message}\n\n{erro.StackTrace}", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ShowResult(List<ModifiedCodeFile> files, AnalyzeService analiseService)
        {
            var formResult = new FrmResult(files, analiseService)
            {
                Icon = Icon
            };
            formResult.ShowDialog();
        }

        private Configuration GetConfiguration()
        {
            var substitutions = GetSubstitutions();
            var directory = new CodeDirectory(txtProjectDirectoryRoot.Text);
            var config = new Configuration(directory, substitutions);
            return config;
        }

        private void ShowNotifications(Notifiable notifiable)
        {
            //TODO: extension method
            var messages = new StringBuilder();
            foreach (var item in notifiable.Notifications)
            {
                messages.AppendLine(item.Message);
            }
            MessageBox.Show(messages.ToString(), "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnOpenDirectoryDialog_Click(object sender, EventArgs e)
        {
            var folderBrowserDialog = new FolderBrowserDialog();
            if (String.IsNullOrEmpty(txtProjectDirectoryRoot.Text) == false)
                folderBrowserDialog.SelectedPath = txtProjectDirectoryRoot.Text;

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                txtProjectDirectoryRoot.Text = folderBrowserDialog.SelectedPath;

            txtProjectDirectoryRoot.SelectAll();
            txtProjectDirectoryRoot.Focus();
        }
    }
}
