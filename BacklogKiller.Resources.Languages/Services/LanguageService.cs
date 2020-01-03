using System;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace BacklogKiller.Resources.Languages.Services
{
    public class LanguageService
    {
        private readonly ResourceManager _resourceManager;
        public CultureInfo _cultureInfo { get; private set; }


        public LanguageService()
        {
            _cultureInfo = CultureInfo.CurrentUICulture;
            var assembly = Assembly.GetExecutingAssembly();
            _resourceManager = new ResourceManager(
                baseName: "BacklogKiller.Resources.Languages.Resources.Resource",
                assembly: assembly
                );
        }

        public string GetString(Strings key)
        {
            try
            {
                return _resourceManager.GetString(key.ToString(), _cultureInfo);
            }
            catch (MissingManifestResourceException)
            {
                //defines English as the default culture
                _cultureInfo = new CultureInfo("en");
                return GetString(key);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
