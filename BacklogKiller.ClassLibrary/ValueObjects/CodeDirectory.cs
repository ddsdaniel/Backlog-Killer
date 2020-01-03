using BacklogKiller.Resources.Languages;
using BacklogKiller.Resources.Languages.Services;
using Flunt.Notifications;
using Flunt.Validations;
using System.IO;

namespace BacklogKiller.ClassLibrary.ValueObjects
{
    public class CodeDirectory : Notifiable
    {
        public string Path { get; private set; }

        public CodeDirectory(string path)
        {
            var languageService = new LanguageService();

            AddNotifications(new Contract()
               .IsNotNullOrEmpty(path, nameof(Path), languageService.GetString(Strings.DirectoryPathMustNotBeEmpty))
               .IsTrue(Directory.Exists(path), nameof(Path), languageService.GetString(Strings.DirectoryNotFound))
               );

            Path = path;
        }

        public bool IsHidden()
        {
            var dir = new DirectoryInfo(System.IO.Path.GetDirectoryName(Path));
            return dir.Attributes.HasFlag(FileAttributes.Hidden);
        }
    }
}
