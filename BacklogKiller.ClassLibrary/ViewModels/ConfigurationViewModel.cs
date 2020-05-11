using System.Collections.Generic;

namespace BacklogKiller.ClassLibrary.ViewModels
{
    public class ConfigurationViewModel
    {
        public string Directory { get; set; }
        public List<ReplacementViewModel> Substitutions { get; set; }
        public List<ReplacementViewModel> AfterSubstitutions { get; set; }
        public string Filters { get; set; }
        public ConfigurationViewModel()
        {
            Substitutions = new List<ReplacementViewModel>();
            AfterSubstitutions = new List<ReplacementViewModel>();
        }
    }
}
