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
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(find, nameof(Find), "Localizar não pode ser nulo ou vazio")
                .AreNotEquals(find, replaceWith, nameof(Find), "Localizar e Substituir por devem ser diferentes")
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
