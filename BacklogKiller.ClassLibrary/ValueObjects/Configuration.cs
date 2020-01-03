using BacklogKiller.Resources.Languages;
using BacklogKiller.Resources.Languages.Services;
using Flunt.Notifications;
using Flunt.Validations;
using System.Collections.Generic;

namespace BacklogKiller.ClassLibrary.ValueObjects
{
    public class Configuration : Notifiable
    {
        public CodeDirectory ProjectDirectory { get; private set; }
        public List<Replacement> Substitutions { get; private set; }

        public Configuration(CodeDirectory projectDirectory, List<Replacement> substitutions)
        {
            var languageService = new LanguageService();

            AddNotifications(new Contract()
                .IsNotNull(projectDirectory, nameof(ProjectDirectory), languageService.GetString(Strings.DirectoryPathMustNotBeEmpty))                
                .IsTrue(substitutions != null && substitutions.Count > 0, nameof(Substitutions), languageService.GetString(Strings.OverrideListMustNotBeEmpty))
                );

            AddNotifications(projectDirectory);
            AddNotifications(substitutions.ToArray());

            ProjectDirectory = projectDirectory;
            Substitutions = substitutions;
        }

        public string ReplaceAll(string text)
        {
            foreach (var subs in Substitutions)
                text = subs.Replace(text);

            return text;
        }
    }
}
