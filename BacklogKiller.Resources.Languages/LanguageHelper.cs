using System.Globalization;
using System.Reflection;
using System.Resources;

namespace BacklogKiller.Resources.Languages
{
    public class LanguageHelper
    {
        private readonly ResourceManager _resourceManager;
        public CultureInfo _cultureInfo { get; private set; }


        public LanguageHelper(CultureInfo cultureInfo)
        {
            _cultureInfo = cultureInfo;
            var assembly = Assembly.GetExecutingAssembly();
            _resourceManager = new ResourceManager(
                baseName: "BacklogKiller.Resources.Languages.Resources.Resource",
                assembly: assembly
                );
        }

        public string GetString(Strings key)
        {
            return _resourceManager.GetString(key.ToString(), _cultureInfo);
        }

    }
}
