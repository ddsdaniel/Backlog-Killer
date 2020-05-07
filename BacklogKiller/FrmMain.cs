using BacklogKiller.ClassLibrary.Extensions;
using BacklogKiller.ClassLibrary.Services;
using BacklogKiller.ClassLibrary.ValueObjects;
using BacklogKiller.ClassLibrary.ViewModels;
using BacklogKiller.Resources.Languages;
using BacklogKiller.Resources.Languages.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

            FormatDataGridViewSubstitutions(dgvSubstitutions);
            FormatDataGridViewSubstitutions(dgvAfterSubstitutions);
            RecoveryFormState();
        }

        private void RecoveryStrings()
        {
            _languageService = new LanguageService();

            tabSubstitutions.Text = _languageService.GetString(Strings.Substitutions);
            tabAfterSubstitutions.Text = _languageService.GetString(Strings.AfterSubstitutions);
            lblRootDirectory.Text = _languageService.GetString(Strings.ProjectRootDirectory);
            tsbAnalyze.Text = _languageService.GetString(Strings.Analyze);
            tsbAnalyze.ToolTipText = tsbAnalyze.Text;
            lblFilters.Text = _languageService.GetString(Strings.Filters);
        }

        private void ShowVersion()
        {
            Text += $" - {Assembly.GetExecutingAssembly().GetName().Version.ToString(3)}";
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
                txtFilters.Text = configurationViewModel.Filters;
                LoadSubstitutions(dgvSubstitutions, configurationViewModel.Substitutions);
                LoadSubstitutions(dgvAfterSubstitutions, configurationViewModel.AfterSubstitutions);
            }
        }

        private void LoadSubstitutions(DataGridView dataGridView, List<ReplacementViewModel> replacementViewModels)
        {
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;

            foreach (var subs in replacementViewModels)
            {
                dataGridView.Rows.Add();

                dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[(int)Column.Find].Value = subs.Find;
                dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[(int)Column.ReplaceWith].Value = subs.ReplaceWith;
            }

            dataGridView.AllowUserToAddRows = true;
            dataGridView.AllowUserToDeleteRows = true;
        }

        private void FormatDataGridViewSubstitutions(DataGridView dataGridView)
        {
            dataGridView.ColumnCount = 2;
            dataGridView.Columns[(int)Column.Find].HeaderText = _languageService.GetString(Strings.ToLocate);
            dataGridView.Columns[(int)Column.Find].Width = (dataGridView.Width / 2) - 20;

            dataGridView.Columns[(int)Column.ReplaceWith].HeaderText = _languageService.GetString(Strings.ReplaceWith);
            dataGridView.Columns[(int)Column.ReplaceWith].Width = dataGridView.Columns[(int)Column.Find].Width;

            dataGridView.AllowUserToAddRows = true;
            dataGridView.AllowUserToDeleteRows = true;
        }

        private void SaveConfiguration()
        {
            if (File.Exists(_pathFileFormState))
                File.Delete(_pathFileFormState);

            var configuration = GetConfiguration();
            var configurationViewModel = new ConfigurationViewModel { 
                Directory = configuration.ProjectDirectory.Path ,
                Filters = txtFilters.Text
            };
            foreach (var subs in configuration.Substitutions)
            {
                configurationViewModel.Substitutions.Add(
                    new ReplacementViewModel
                    {
                        Find = subs.Find,
                        ReplaceWith = subs.ReplaceWith
                    });
            }

            foreach (var subs in configuration.AfterSubstitutions)
            {
                configurationViewModel.AfterSubstitutions.Add(
                    new ReplacementViewModel
                    {
                        Find = subs.Find,
                        ReplaceWith = subs.ReplaceWith
                    });
            }

            var serializeService = new SerializeService<ConfigurationViewModel>();
            serializeService.Serialize(configurationViewModel, _pathFileFormState);
        }

        private List<Replacement> GetSubstitutions(DataGridView dataGridView)
        {
            var substitutions = new List<Replacement>();
            foreach (DataGridViewRow linha in dataGridView.Rows)
            {
                if (linha.IsNewRow || linha.Cells[(int)Column.Find].Value == null || linha.Cells[(int)Column.ReplaceWith].Value == null)
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
            var substitutions = GetSubstitutions(dgvSubstitutions);
            var afterSubstitutions = GetSubstitutions(dgvAfterSubstitutions);
            var directory = new CodeDirectory(txtProjectDirectoryRoot.Text);
            var config = new Configuration(directory, substitutions, afterSubstitutions, txtFilters.Text);
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

                var substitutions = GetSubstitutions(dgvSubstitutions);
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
                AddToGrid(dgvSubstitutions, suggestions);
            }
        }

        private void AddToGrid(DataGridView dataGridView, List<Replacement> suggestions)
        {
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;

            foreach (var item in suggestions)
            {
                dataGridView.Rows.Add();

                dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[(int)Column.Find].Value = item.Find;
                dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[(int)Column.ReplaceWith].Value = item.ReplaceWith;
            }

            dataGridView.AllowUserToAddRows = true;
            dataGridView.AllowUserToDeleteRows = true;
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveConfiguration();
        }
    }
}
