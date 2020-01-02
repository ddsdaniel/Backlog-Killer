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
            AddNotifications(new Contract()
                .IsNotNull(projectDirectory, nameof(ProjectDirectory), "Diretório de Código-Fonte não deve ser nulo")                
                .IsTrue(substitutions != null && substitutions.Count > 0, nameof(Substitutions), "A lista de substituições não deve estar vazia")
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
