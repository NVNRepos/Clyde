using Clyde.Domain;
using Clyde.Globalization;

namespace Clyde.App.Resources
{
    /// <summary>
    /// Facade to manage different culture in the Application
    /// </summary>
    public static class Language
    {
        #region Fields

        private static ILanguageManager _languageManager = null;

        #endregion

        #region Management

        /// <summary>
        /// Sets a specified <see cref="ILanguageManager"/>
        /// </summary>
        /// <param name="languageManager">The implementation</param>
        /// <param name="override">Override the currently used on</param>
        public static void SetLanguageManager(ILanguageManager languageManager, bool @override = false)
        {
            if (@override)
            {
                _languageManager = languageManager;
            }
            else
            {
                _languageManager = _languageManager ?? languageManager;
            }
        }

        /// <summary>
        /// The currently used <see cref="ILanguageManager"/>
        /// </summary>
        public static ILanguageManager LanguageManager
        {
            get
            {
                if (_languageManager is null)
                    SetLanguageManager(ClydeGlobalization.DefaultLanguageManager);
                return _languageManager;
            }
        }

        /// <summary>
        /// The currently used <see cref="ClydeCulture"/>
        /// </summary>
        public static ClydeCulture Culture
        {
            get => LanguageManager.Culture;
            set => LanguageManager.Culture = value;
        }

        #endregion

        #region ApplicationStrings
        //importent strings for the application UI

        public static string ApplicationName
            => ClydeDomain.APPLICATION_NAME;

        public static string Start
            => LanguageManager.GetCulturedString(GlobalizeString.START);

        public static string Stop
            => LanguageManager.GetCulturedString(GlobalizeString.STOP);

        public static string Quit
        => LanguageManager.GetCulturedString(GlobalizeString.QUIT);

        public static string Running
            => LanguageManager.GetCulturedString(GlobalizeString.RUNNING);

        public static string Idle
            => LanguageManager.GetCulturedString(GlobalizeString.IDLE);

        public static string Update
            => LanguageManager.GetCulturedString(GlobalizeString.UPDATE);

        #endregion
    }
}
