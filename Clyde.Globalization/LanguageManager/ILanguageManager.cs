namespace Clyde.Globalization
{
    /// <summary>
    /// Interface to manage different cultures
    /// </summary>
    public interface ILanguageManager
    {
        /// <summary>
        /// The currently used <see cref="ClydeCulture"/>
        /// </summary>
        ClydeCulture Culture { get; set; }

        /// <summary>
        /// Gets a localized <see cref="string"/> from the resources
        /// </summary>
        /// <param name="key">Key of the resource</param>
        /// <returns>The localized version of the <see cref="string"/> if found else <see cref="ClydeGlobalization.NOT_TRANSLATED"/></returns>
        string GetCulturedString(string key);
    }
}
