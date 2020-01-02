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
            AddNotifications(new Contract()
               .IsNotNullOrEmpty(path, nameof(Path), "Caminho do diretório não deve ser vazio")
               .IsTrue(Directory.Exists(path), nameof(Path), "Diretório não encontrado")
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
