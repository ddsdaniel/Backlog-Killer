using Flunt.Notifications;
using BacklogKiller.ClassLibrary.ValueObjects;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BacklogKiller.ClassLibrary.Services
{
    public class AnalyzeService : Notifiable
    {
        public Configuration Configuration { get; private set; }

        public AnalyzeService(Configuration configuration)
        {
            AddNotifications(configuration);

            Configuration = configuration;
        }

        public List<ModifiedCodeFile> GetFiles()
        {
            var codeFiles = Configuration.Filters
                .Split(';')
                .SelectMany(filter => Directory.GetFiles(Configuration.ProjectDirectory.Path, filter, SearchOption.AllDirectories))
                .ToList()
                .FindAll(f => !CodeFile.IsLocked(f))
                .Select(path => new CodeFile(path, Configuration.ProjectDirectory, File.ReadAllText(path)))
                .ToList();

            codeFiles.RemoveAll(cf => !cf.IsUseful());

            var modifiedFiles = codeFiles
                .Select(cf => ToModifiedCodeFile(cf))
                .ToList();

            var duplicatedFiles = modifiedFiles.FindAll(mf => mf.OriginalFile.Equals(mf.ModifiedFile));
            foreach (var dupFile in duplicatedFiles)
            {
                File.Delete(dupFile.ModifiedFile.FullPath);
                modifiedFiles.RemoveAll(mf => mf.ModifiedFile.FullPath == dupFile.ModifiedFile.FullPath);
            }

            return modifiedFiles;
        }

        private ModifiedCodeFile ToModifiedCodeFile(CodeFile originalFile)
        {
            var modifiedRelativePath = Configuration.ReplaceAll(originalFile.RelativePath);
            var modifiedFullPath = Path.GetTempPath() + modifiedRelativePath;
            var modifiedContent = Configuration.ReplaceAll(originalFile.Content);

            var directoryPath = Path.GetDirectoryName(modifiedFullPath);

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            var tempDirectory = new CodeDirectory(Path.GetTempPath());

            if (File.Exists(modifiedFullPath))
                File.Delete(modifiedFullPath);

            File.WriteAllText(modifiedFullPath, modifiedContent);

            CodeFile modifiedFile = new CodeFile(modifiedFullPath, tempDirectory, modifiedContent);

            return new ModifiedCodeFile(originalFile, modifiedFile);
        }

        public void GenerateFiles(List<ModifiedCodeFile> files)
        {
            foreach (var file in files)
            {
                var path = file.OriginalFile.RootDirectory.Path + file.ModifiedFile.RelativePath;
                var directory = Path.GetDirectoryName(path);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);
                File.WriteAllText(path, file.ModifiedFile.Content);
            }
        }
    }
}
