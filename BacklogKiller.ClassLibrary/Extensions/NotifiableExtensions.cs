using Flunt.Notifications;
using System.Text;

namespace BacklogKiller.ClassLibrary.Extensions
{
    public static class NotifiableExtensions
    {
        public static string GetAllMessages(this Notifiable notifiable)
        {
            var messages = new StringBuilder();
            if (notifiable.Invalid)
            {
                foreach (var item in notifiable.Notifications)
                {
                    messages.AppendLine(item.Message);
                }
            }
            return messages.ToString();
        }
    }
}
