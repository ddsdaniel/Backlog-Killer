using BacklogKiller.ClassLibrary.Services;
using BacklogKiller.ClassLibrary.ValueObjects;
using BacklogKiller.Resources.Languages;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BacklogKiller
{
    public partial class FrmResult : Form
    {
        private enum EnumColumns
        {
            Path
        }

        private List<ModifiedCodeFile> _files;
        private AnalyzeService _analyzeService;
        private LanguageHelper _languageHelper;

        public FrmResult(List<ModifiedCodeFile> files, AnalyzeService analyzeService, LanguageHelper languageHelper)
        {
            InitializeComponent();

            _files = files;
            _analyzeService = analyzeService;
            _languageHelper = languageHelper;
        }

        private void FrmResult_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            RecoveryStrings();            

            FormatListView(lvwModifiedFiles);
            FormatListView(lvwNewFiles);
            lvwNewFiles.CheckBoxes = true;

            LoadFiles();

            Cursor = Cursors.Default;
        }

        private void RecoveryStrings()
        {
            Text = _languageHelper.GetString(Strings.Results);
            tabNewFiles.Text = _languageHelper.GetString(Strings.NewFiles);
            tabModifiedFiles.Text = _languageHelper.GetString(Strings.ModifiedFiles);
            btnGenerateSelectedFiles.Text = _languageHelper.GetString(Strings.GenerateSelectedFiles);
        }

        private void FormatListView(ListView listView)
        {
            listView.Columns.Clear();
            listView.View = View.Details;
            listView.MultiSelect = false;
            listView.FullRowSelect = true;
            listView.AllowDrop = false;

            listView.Columns.Add("File", listView.Width - 50);
            listView.Scrollable = true;

            listView.MouseDoubleClick += ListView_MouseDoubleClick;
        }

        private void ListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var listView = (ListView)sender;

            if (listView.SelectedItems.Count == 1)
            {
                var file = (ModifiedCodeFile)listView.SelectedItems[0].Tag;

                string parameters, exePath;

                //TODO: configurar exe e parâmetros
                var tortoiseGitMergePath = @"C:\Program Files\TortoiseGit\bin\TortoiseGitMerge.exe";
                if (File.Exists(tortoiseGitMergePath))
                {
                    exePath = tortoiseGitMergePath;

                    parameters = " /base:" + CharConstants.ASPAS + file.ModifiedFile.FullPath + CharConstants.ASPAS;
                    parameters += " /basename:" + CharConstants.ASPAS + file.ModifiedFile.RootDirectory.Path + CharConstants.ASPAS;

                    parameters += " /theirs:" + CharConstants.ASPAS + file.OriginalFile.FullPath + CharConstants.ASPAS;
                    parameters += " /theirsname:" + CharConstants.ASPAS + file.OriginalFile.RootDirectory.Path + CharConstants.ASPAS;

                    if (file.IsNew())
                        parameters += " /readonly";
                }
                else
                {
                    var notepadPlusPlusPath = "notepad++";
                    exePath = notepadPlusPlusPath;

                    parameters = CharConstants.ASPAS + file.OriginalFile.FullPath + CharConstants.ASPAS + " " +
                        CharConstants.ASPAS + file.ModifiedFile.FullPath + CharConstants.ASPAS;
                }

                OpenProcess(exePath, parameters);
            }
        }

        public static void OpenProcess(string path, string parameters)
        {
            using (var process = new System.Diagnostics.Process())
            {
                process.StartInfo.FileName = path;
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized;

                if (String.IsNullOrEmpty(parameters) == false)
                    process.StartInfo.Arguments = parameters;

                process.Start();
            }
        }

        private void LoadFiles()
        {
            var imageList = new ImageList();
            lvwModifiedFiles.SmallImageList = imageList;
            lvwNewFiles.SmallImageList = imageList;

            foreach (var file in _files)
            {
                var extension = Path.GetExtension(file.OriginalFile.FullPath);
                if (!imageList.Images.Keys.Contains(extension))
                {
                    imageList.Images.Add(extension, GetIcon(file.OriginalFile.FullPath));
                }

                var listView = file.IsNew()
                    ? lvwNewFiles
                    : lvwModifiedFiles;

                var item = new ListViewItem();
                for (var i = 0; i < listView.Columns.Count - 1; i++)
                {
                    item.SubItems.Add("");
                }
                item.Tag = file;
                item.ImageKey = extension;
                item.SubItems[(int)EnumColumns.Path].Text = file.ModifiedFile.RelativePath;

                listView.Items.Add(item);
            }
        }

        public Bitmap GetIcon(string fullPath)
        {
            var icon = Icon.ExtractAssociatedIcon(fullPath);
            var bitmap = new Bitmap(icon.Width, icon.Height);
            using (Graphics gp = Graphics.FromImage(bitmap))
            {
                gp.Clear(Color.Transparent);
                gp.DrawIcon(icon, new Rectangle(0, 0, icon.Width, icon.Height));
            }
            return bitmap;
        }

        private void FrmResult_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }

        private void btnGenerateSelectedFiles_Click(object sender, EventArgs e)
        {
            var files = new List<ModifiedCodeFile>();
            foreach (ListViewItem item in lvwNewFiles.CheckedItems)
            {
                files.Add((ModifiedCodeFile)item.Tag);
            }

            _analyzeService.GenerateFiles(files);

            //TODO: feedback
        }
    }
}
