using BacklogKiller.Resources.Languages;
using BacklogKiller.Resources.Languages.Services;
using Flunt.Notifications;
using Flunt.Validations;

namespace BacklogKiller.ClassLibrary.ValueObjects
{
    public class Replacement : Notifiable
    {
        public string Find { get; private set; }
        public string ReplaceWith { get; private set; }

        public Replacement(string find, string replaceWith)
        {
            var languageService = new LanguageService();

            AddNotifications(new Contract()
                .IsNotNullOrEmpty(find, nameof(Find), languageService.GetString(Strings.FindCannotBeNullOrEmpty))
                .AreNotEquals(find, replaceWith, nameof(Find), languageService.GetString(Strings.FindAndReplaceWithMustBeDifferent))
                );

            Find = find;
            ReplaceWith = replaceWith;
        }

        public string Replace(string target)
        {
            return target.Replace(Find, ReplaceWith);
        }
    }
}
