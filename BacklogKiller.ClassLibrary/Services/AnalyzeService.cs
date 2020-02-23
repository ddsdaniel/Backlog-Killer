using Flunt.Notifications;
using BacklogKiller.ClassLibrary.ValueObjects;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System;

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
            var skippedFilesGitIgnore = GetSkippedFilesGitIgnore(Configuration.ProjectDirectory.Path);

            var codeFiles = Configuration.Filters
                .Split(';')
                .SelectMany(filter => Directory.GetFiles(Configuration.ProjectDirectory.Path, filter, SearchOption.AllDirectories))
                .ToList()
                .FindAll(f => !skippedFilesGitIgnore.Any(sf => f.StartsWith(sf)))
                .FindAll(f => !CodeFile.IsLocked(f))
                .Select(path => new CodeFile(path, Configuration.ProjectDirectory, File.ReadAllText(path, Encoding.UTF8)))
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

        private static List<string> GetSkippedFilesGitIgnore(string rootPath)
        {
            var result = new List<string>();

            var gitIgnoreFiles = Directory.GetFiles(rootPath, ".gitignore", SearchOption.AllDirectories);

            foreach (var gitIgnoreFile in gitIgnoreFiles)
            {
                var repositoryPath = Path.GetDirectoryName(gitIgnoreFile);
                var disk = repositoryPath.Split(':')[0];

                var msDosService = new MsDosService();
                var skippedFiles = msDosService.Execute($"{disk}: && cd {repositoryPath} && git status --ignored")
                    .Replace("\t", "")
                    .Split('\n')
                    .ToList();

                var start = skippedFiles.FindIndex(s => s.StartsWith("Ignored files:"));
                if (start >= 0)
                {
                    start += 3;//first file

                    var count = skippedFiles.FindIndex(start + 1, s => String.IsNullOrEmpty(s)) - start;
                    skippedFiles = skippedFiles
                        .GetRange(start, count)
                        .Select(sf => sf.Replace("/", "\\"))
                        .Select(sf => Path.Combine(repositoryPath, sf))
                        .Select(sf => Path.GetFullPath(sf))
                        .ToList();

                    result.AddRange(skippedFiles);
                }
            }

            return result;
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

            File.WriteAllText(modifiedFullPath, modifiedContent, Encoding.UTF8);

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
                File.WriteAllText(path, file.ModifiedFile.Content, Encoding.UTF8);
            }
        }
    }
}
