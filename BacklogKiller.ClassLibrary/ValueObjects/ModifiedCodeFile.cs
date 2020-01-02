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
            AddNotifications(new Contract()
                .AreNotEquals(originalFile, modifiedFile,nameof(ModifiedFile), "Arquivos devem ser diferentes.")
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
