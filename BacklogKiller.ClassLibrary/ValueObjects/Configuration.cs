using BacklogKiller.Resources.Languages;
using BacklogKiller.Resources.Languages.Services;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.IO;

namespace BacklogKiller.ClassLibrary.ValueObjects
{
    public class Configuration : Notifiable
    {
        public string Filters { get; private set; }
        public CodeDirectory ProjectDirectory { get; private set; }
        public List<Replacement> Substitutions { get; private set; }

        public Configuration(CodeDirectory projectDirectory, List<Replacement> substitutions, string filters)
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
            Filters = String.IsNullOrEmpty(filters) ? "*.*" : filters;
        }

        public string ReplaceAll(string text)
        {
            foreach (var subs in Substitutions)
                text = subs.Replace(text);

            return text;
        }
    }
}
