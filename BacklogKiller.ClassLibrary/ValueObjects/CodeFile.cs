using BacklogKiller.Resources.Languages;
using BacklogKiller.Resources.Languages.Services;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.IO;

namespace BacklogKiller.ClassLibrary.ValueObjects
{
    public class CodeFile : Notifiable
    {

        public string RelativePath { get; private set; }
        public string FullPath { get; private set; }
        public string Content { get; private set; }
        public CodeDirectory RootDirectory { get; private set; }

        public CodeFile(string fullPath, CodeDirectory rootDirectory, string content)
        {
            var languageService = new LanguageService();

            AddNotifications(new Contract()
               .IsNotNullOrEmpty(fullPath, nameof(FullPath), languageService.GetString(Strings.FilePathMustNotBeEmpty))
               .IsTrue(File.Exists(fullPath), nameof(FullPath), languageService.GetString(Strings.FileNotFound))
               );

            AddNotifications(rootDirectory);

            FullPath = fullPath;
            Content = content;
            RelativePath = fullPath.Substring(rootDirectory.Path.Length);
            RootDirectory = rootDirectory;
        }

        public bool IsUseful()
        {
            var directoryPath = Path.GetDirectoryName(FullPath);
            var dir = new CodeDirectory(directoryPath);

            return !dir.IsHidden() && !IsHidden() && HasExtension() && !IsBinary();
        }

        private bool HasExtension()
        {
            return Path.GetFileName(FullPath) != Path.GetFileNameWithoutExtension(FullPath);
        }

        private bool IsHidden()
        {
            var fileInfo = new FileInfo(FullPath);
            return fileInfo.Attributes.HasFlag(FileAttributes.Hidden);
        }

        private bool IsBinary()
        {
            using (StreamReader stream = new StreamReader(FullPath))
            {
                int ch;
                while ((ch = stream.Read()) != -1)
                {
                    if ((ch > CharConstants.NULL && ch < CharConstants.BACK_SPACE)
                        || (ch > CharConstants.CARRIAGE_RETURN && ch < CharConstants.SUBSTITUTE))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            var outro = obj as CodeFile;
            if (outro == null)
                return false;

            return outro.RelativePath == this.RelativePath &&
                outro.Content == this.Content;
        }

        public static bool IsLocked(string filename)
        {
            bool Locked = false;
            try
            {
                FileStream fs = File.Open(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                fs.Close();
            }
            catch
            {
                Locked = true;
            }
            return Locked;
        }
    }
}
