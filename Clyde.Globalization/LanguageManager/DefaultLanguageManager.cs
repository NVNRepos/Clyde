using System.Globalization;
using System.Threading;

namespace Clyde.Globalization
{
    /// <summary>
    /// Default Languagemanager. Uses resources of this assembly
    /// </summary>
    internal class DefaultLanguageManager : ILanguageManager
    {
        private CultureInfo _cultureInfo;

        internal DefaultLanguageManager() : this(Thread.CurrentThread.CurrentCulture) { }

        internal DefaultLanguageManager(ClydeCulture culture) : this(culture.GetCultureInfo()) { }

        internal DefaultLanguageManager(CultureInfo cultureInfo)
            => SetCulture(cultureInfo);

        public ClydeCulture Culture
        {
            get => ClydeGlobalization.Convert(_cultureInfo);
            set => SetCulture(value.GetCultureInfo());
        }

        public string GetCulturedString(string key)
        {
            string result = Properties.Resource.ResourceManager.GetString(key ?? string.Empty, _cultureInfo);
            if (string.IsNullOrEmpty(result))
                return ClydeGlobalization.NOT_TRANSLATED;
            return result;
        }

        private void SetCulture(CultureInfo cultureInfo)
        {
            _cultureInfo = cultureInfo;
            Thread.CurrentThread.CurrentCulture = _cultureInfo;
            Thread.CurrentThread.CurrentUICulture = _cultureInfo;
        }
    }
}
