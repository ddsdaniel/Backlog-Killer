using BacklogKiller.ClassLibrary.Extensions;
using BacklogKiller.ClassLibrary.ValueObjects;
using BacklogKiller.Resources.Languages;
using BacklogKiller.Resources.Languages.Services;
using Flunt.Notifications;
using Flunt.Validations;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BacklogKiller.ClassLibrary.Services
{
    public class CaseSubstitutionsService : Notifiable
    {
        private readonly Replacement _camelCaseReplacement;
        private readonly List<Replacement> _currentReplacements;

        public CaseSubstitutionsService(Replacement camelCaseReplacement, List<Replacement> currentReplacements)
        {
            var languageService = new LanguageService();

            AddNotifications(camelCaseReplacement);

            AddNotifications(new Contract()
                    .IsTrue(
                        camelCaseReplacement.Find.IsCamelCase(), 
                        nameof(camelCaseReplacement.Find),
                        languageService.GetString(Strings.FindNotInCamelCaseFormat))

                    .IsTrue(
                        camelCaseReplacement.ReplaceWith.IsCamelCase(), 
                        nameof(camelCaseReplacement.ReplaceWith),
                        languageService.GetString(Strings.ReplaceWithNotInCamelCaseFormat))
                );

            _camelCaseReplacement = camelCaseReplacement;
            _currentReplacements = currentReplacements;
        }

        public List<Replacement> Suggest()
        {
            if (Invalid)
                return null;

            var findWords = _camelCaseReplacement.Find.SplitCamelCase();
            var replaceWithWords = _camelCaseReplacement.ReplaceWith.SplitCamelCase();

            var suggestions = new List<Replacement>
            {
                new Replacement(ToFirstLower(findWords), ToFirstLower(replaceWithWords)),
                new Replacement(ToAllCaps(findWords), ToAllCaps(replaceWithWords)),
                new Replacement(ToTitle(findWords), ToTitle(replaceWithWords)),
                new Replacement(ToHyphenSeparated(findWords), ToHyphenSeparated(replaceWithWords)),                
            };

            //remove already added
            suggestions.RemoveAll(s => _currentReplacements.Any(cr => cr.Find == s.Find));

            return suggestions;
        }

        private string ToFirstLower(string[] words)
        {
            if (words.Length == 0)
                return "";

            var result = new StringBuilder();

            result.Append(words[0][0].ToString().ToLower() + words[0].Substring(1));

            for (int i = 1; i < words.Length; i++)
            {
                result.Append(words[i]);
            }

            return result.ToString();
        }

        private string ToAllCaps(string[] words)
        {
            if (words.Length == 0)
                return "";

            return string.Join("_", words).ToUpper();            
        }

        private string ToTitle(string[] words)
        {
            if (words.Length == 0)
                return "";

            return string.Join(" ", words);
        }

        private string ToHyphenSeparated(string[] words)
        {
            if (words.Length == 0)
                return "";

            return string.Join("-", words).ToLower();
        }
    }
}
