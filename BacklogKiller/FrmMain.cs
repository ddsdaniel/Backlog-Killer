using BacklogKiller.ClassLibrary.Extensions;
using BacklogKiller.ClassLibrary.Services;
using BacklogKiller.ClassLibrary.ValueObjects;
using BacklogKiller.ClassLibrary.ViewModels;
using BacklogKiller.Resources.Languages;
using BacklogKiller.Resources.Languages.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BacklogKiller
{
    public partial class FrmMain : Form
    {
        private enum Column
        {
            Find = 0,
            ReplaceWith
        }

        private string _pathFileFormState;
        private LanguageService _languageService;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            Icon = Properties.Resources.ico_main;            

            RecoveryStrings();

            ShowVersion();

            FormatDgvSubstitutions();
            RecoveryFormState();
        }

        private void RecoveryStrings()
        {
            _languageService = new LanguageService();

            lblSubstitutions.Text = _languageService.GetString(Strings.Substitutions);
            lblRootDirectory.Text = _languageService.GetString(Strings.ProjectRootDirectory);
            tsbAnalyze.Text = _languageService.GetString(Strings.Analyze);
            tsbAnalyze.ToolTipText = tsbAnalyze.Text;
        }

        private void ShowVersion()
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            Text += $" - {fvi.FileVersion}";
        }

        private void RecoveryFormState()
        {
            var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Backlog Killer");
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            _pathFileFormState = Path.Combine(directory, "form-state.xml");

            var serializeService = new SerializeService<ConfigurationViewModel>();

            if (File.Exists(_pathFileFormState))
            {
                var configurationViewModel = serializeService.Deserialize(_pathFileFormState);
                txtProjectDirectoryRoot.Text = configurationViewModel.Directory;

                dgvSubstitutions.AllowUserToAddRows = false;
                dgvSubstitutions.AllowUserToDeleteRows = false;

                foreach (var subs in configurationViewModel.Substitutions)
                {
                    dgvSubstitutions.Rows.Add();

                    dgvSubstitutions.Rows[dgvSubstitutions.Rows.Count - 1].Cells[(int)Column.Find].Value = subs.Find;
                    dgvSubstitutions.Rows[dgvSubstitutions.Rows.Count - 1].Cells[(int)Column.ReplaceWith].Value = subs.ReplaceWith;
                }

                dgvSubstitutions.AllowUserToAddRows = true;
                dgvSubstitutions.AllowUserToDeleteRows = true;
            }
        }

        private void FormatDgvSubstitutions()
        {
            dgvSubstitutions.ColumnCount = 2;
            dgvSubstitutions.Columns[(int)Column.Find].HeaderText = _languageService.GetString(Strings.ToLocate);
            dgvSubstitutions.Columns[(int)Column.Find].Width = (dgvSubstitutions.Width / 2) - 20;

            dgvSubstitutions.Columns[(int)Column.ReplaceWith].HeaderText = _languageService.GetString(Strings.ReplaceWith);
            dgvSubstitutions.Columns[(int)Column.ReplaceWith].Width = dgvSubstitutions.Columns[(int)Column.Find].Width;

            dgvSubstitutions.AllowUserToAddRows = true;
            dgvSubstitutions.AllowUserToDeleteRows = true;
        }

        private void SaveConfiguration()
        {
            if (File.Exists(_pathFileFormState))
                File.Delete(_pathFileFormState);

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
            serializeService.Serialize(configurationViewModel, _pathFileFormState);
        }

        private List<Replacement> GetSubstitutions()
        {
            var substitutions = new List<Replacement>();
            foreach (DataGridViewRow linha in dgvSubstitutions.Rows)
            {
                if (linha.IsNewRow)
                    continue;

                var subs = new Replacement(
                    find: linha.Cells[(int)Column.Find].Value.ToString(),
                    replaceWith: linha.Cells[(int)Column.ReplaceWith].Value.ToString()
                    );
                substitutions.Add(subs);
            }

            return substitutions;
        }

        private void tsbAnalyze_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                //TODO: loading gif

                var configuration = GetConfiguration();
                var analiseService = new AnalyzeService(configuration);

                if (analiseService.Invalid)
                {
                    MessageBox.Show(
                        analiseService.GetAllMessages(),
                        _languageService.GetString(Strings.Alert),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation
                        );
                }
                else
                {
                    SaveConfiguration();

                    var files = analiseService.GetFiles();
                    if (files.Count == 0)
                    {
                        MessageBox.Show(
                            _languageService.GetString(Strings.EmptyResult),
                            _languageService.GetString(Strings.Alert),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation
                            );
                    }
                    else
                        ShowResult(files, analiseService);
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show($"{erro.Message}\n\n{erro.StackTrace}",
                    _languageService.GetString(Strings.Alert),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                    );
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ShowResult(List<ModifiedCodeFile> files, AnalyzeService analiseService)
        {
            var formResult = new FrmResult(files, analiseService, _languageService)
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

        private void dgvSubstitutions_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            ProcessSuggestions(e);
            SaveConfiguration();
        }

        private void ProcessSuggestions(DataGridViewCellEventArgs e)
        {
            var findValue = dgvSubstitutions.Rows[e.RowIndex].Cells[(int)Column.Find].Value;
            var replaceWithValue = dgvSubstitutions.Rows[e.RowIndex].Cells[(int)Column.ReplaceWith].Value;

            if (findValue != null && replaceWithValue != null)
            {
                var camelCaseReplacement = new Replacement
                    (
                        find: findValue.ToString(),
                        replaceWith: replaceWithValue.ToString()
                    );

                var substitutions = GetSubstitutions();
                var caseSubstitutionsService = new CaseSubstitutionsService(camelCaseReplacement, substitutions);
                if (caseSubstitutionsService.Valid)
                {
                    var suggestions = caseSubstitutionsService.Suggest();
                    if (suggestions.Any())
                    {
                        AskAcceptSuggestions(suggestions);
                    }
                }
            }
        }

        private void AskAcceptSuggestions(List<Replacement> suggestions)
        {
            var question = new StringBuilder();
            question.AppendLine(_languageService.GetString(Strings.DoYouAcceptTheFollowingSuggestions));
            foreach (var item in suggestions)
            {
                question.AppendLine(item.ToString());
            }

            var alert = _languageService.GetString(Strings.Alert);
            if (MessageBox.Show(question.ToString(), alert, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                AddToGrid(suggestions);
            }
        }

        private void AddToGrid(List<Replacement> suggestions)
        {
            dgvSubstitutions.AllowUserToAddRows = false;
            dgvSubstitutions.AllowUserToDeleteRows = false;

            foreach (var item in suggestions)
            {
                dgvSubstitutions.Rows.Add();

                dgvSubstitutions.Rows[dgvSubstitutions.Rows.Count - 1].Cells[(int)Column.Find].Value = item.Find;
                dgvSubstitutions.Rows[dgvSubstitutions.Rows.Count - 1].Cells[(int)Column.ReplaceWith].Value = item.ReplaceWith;
            }

            dgvSubstitutions.AllowUserToAddRows = true;
            dgvSubstitutions.AllowUserToDeleteRows = true;
        }      
    }
}
