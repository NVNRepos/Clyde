using System.Collections.Generic;
using System.Reflection;

namespace Clyde.Globalization
{
    /// <summary> Key values for <see cref="ILanguageManager.GetCulturedString(GlobalizeString)"/> </summary>
    public static class GlobalizeString
    {
        #region GlobalizeStringKey

        /// <summary> New Update available! </summary>
        public const string UPDATE = "update";

        /// <summary> Running </summary>
        public const string RUNNING = "run";

        /// <summary> Idle </summary>
        public const string IDLE = "idle";

        /// <summary> Application is already running </summary>
        public const string SINGLE_INSTANCE = "single";

        /// <summary> Start </summary>
        public const string START = "start";

        /// <summary> Stop </summary>
        public const string STOP = "stop";

        /// <summary> Quit </summary>
        public const string QUIT = "exit";

        /// <summary> Show </summary>
        public const string SHOW = "show";

        #endregion

        #region Helper

        /// <summary>
        /// Gets all definded <see cref="GlobalizeString"/>
        /// </summary>
        /// <returns>A List with all resource key values </returns>
        public static IEnumerable<string> GetAllKeys()
        {
            FieldInfo[] fieldInfos = typeof(GlobalizeString)
            .GetFields(BindingFlags.Public | BindingFlags.Static);

            foreach (FieldInfo field in fieldInfos)
            {
                if (field.GetValue(null) is string key)
                    yield return key;
            }
        }

        #endregion
    }
}

