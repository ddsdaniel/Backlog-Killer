using BacklogKiller.Resources.Languages;
using BacklogKiller.Resources.Languages.Services;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;

namespace BacklogKiller.ClassLibrary.ValueObjects
{
    public class Configuration : Notifiable
    {
        public string Filters { get; private set; }
        public CodeDirectory ProjectDirectory { get; private set; }
        public List<Replacement> Substitutions { get; private set; }
        public List<Replacement> AfterSubstitutions { get; private set; }

        public Configuration(CodeDirectory projectDirectory, List<Replacement> substitutions, List<Replacement> afterSubstitutions, string filters)
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
            AfterSubstitutions = afterSubstitutions;
            Filters = String.IsNullOrEmpty(filters) ? "*.*" : filters;
        }

        public string ReplaceAll(string text, bool checkChanged = true)
        {
            var changed = false;
            foreach (var subs in Substitutions)
            {
                var newText = subs.Replace(text);
                if (text != newText)
                    changed = true;
                text = newText;
            }

            //if (!checkChanged || changed)
            //{
            //    foreach (var subs in AfterSubstitutions)
            //        text = subs.Replace(text);
            //}

            return text;
        }
    }
}
