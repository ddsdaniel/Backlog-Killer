using BacklogKiller.Resources.Languages;
using BacklogKiller.Resources.Languages.Services;
using Flunt.Notifications;
using Flunt.Validations;
using System.IO;

namespace BacklogKiller.ClassLibrary.ValueObjects
{
    public class ModifiedCodeFile : Notifiable
    {
        public CodeFile OriginalFile { get; private set; }
        public CodeFile ModifiedFile { get; private set; }
        public Configuration Configuration { get; private set; }

        public ModifiedCodeFile(CodeFile originalFile, CodeFile modifiedFile, Configuration configuration)
        {
            var languageService = new LanguageService();

            AddNotifications(new Contract()
                .AreNotEquals(originalFile, modifiedFile, nameof(ModifiedFile), languageService.GetString(Strings.FilesMustBeDifferent))
                );

            AddNotifications(originalFile);
            AddNotifications(modifiedFile);
            AddNotifications(configuration);

            OriginalFile = originalFile;
            ModifiedFile = modifiedFile;
            Configuration = configuration;
        }

        public bool IsNew() => !File.Exists(Path.Combine(Configuration.ProjectDirectory.Path, ModifiedFile.RelativePath.Substring(1)));

        public override string ToString() => ModifiedFile.RelativePath;
    }
}
