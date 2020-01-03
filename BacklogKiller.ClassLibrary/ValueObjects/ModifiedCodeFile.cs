using BacklogKiller.Resources.Languages;
using BacklogKiller.Resources.Languages.Services;
using Flunt.Notifications;
using Flunt.Validations;

namespace BacklogKiller.ClassLibrary.ValueObjects
{
    public class ModifiedCodeFile : Notifiable
    {
        public CodeFile OriginalFile { get; private set; }
        public CodeFile ModifiedFile { get; private set; }

        public ModifiedCodeFile(CodeFile originalFile, CodeFile modifiedFile)
        {
            var languageService = new LanguageService();

            AddNotifications(new Contract()
                .AreNotEquals(originalFile, modifiedFile,nameof(ModifiedFile), languageService.GetString(Strings.FilesMustBeDifferent))
                );

            AddNotifications(originalFile);
            AddNotifications(modifiedFile);

            OriginalFile = originalFile;
            ModifiedFile = modifiedFile;
        }                

        public bool IsNew()
        {
            return OriginalFile.RelativePath != ModifiedFile.RelativePath;
        }
        
    }
}
