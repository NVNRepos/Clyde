using System.Globalization;

namespace Clyde.Globalization
{
    /// <summary>
    /// Utility class to provide basic globalization helper methods
    /// </summary>
    public static class ClydeGlobalization
    {
        #region Two_Letter_ISO_Language_Codes

        /// <summary> Two letter ISO code for german </summary>
        public const string GERMAN_ISO = "de";

        /// <summary> Two letter ISO code for english </summary>
        public const string ENGLISH_ISO = "en";

        /// <summary> Two letter ISO code for spanish </summary>
        public const string SPANISH_ISO = "es";

        /// <summary> Two letter ISO code for portuguese </summary>
        public const string PORTUGUESE_ISO = "pt";

        /// <summary> Two letter ISO code for russian </summary>
        public const string RUSSIAN_ISO = "ru";

        /// <summary> Two letter ISO code for arabic </summary>
        public const string ARABIC_ISO = "ar";

        /// <summary> Two letter ISO code for punjabi </summary>
        public const string PUNJABI_ISO = "pa";

        /// <summary> Two letter ISO code for bengali </summary>
        public const string BENGALI_ISO = "bn";

        /// <summary> Two letter ISO code for hindi </summary>
        public const string HINDI_ISO = "hi";

        /// <summary> Two letter ISO code for chinese </summary>
        public const string CHINESE_ISO = "zh";

        /// <summary> Two letter ISO code for japanese </summary>
        public const string JAPANESE_ISO = "ja";

        /// <summary> Two letter ISO code for korean </summary>
        public const string KOREAN_ISO = "ko";

        /// <summary> Two letter ISO code for italian </summary>
        public const string ITALIAN_ISO = "it";

        /// <summary> Two letter ISO code for french </summary>
        public const string FRENCH_ISO = "fr";

        #endregion

        #region NotTranslated

        /// <summary>
        /// If no localized string can be found this will take it place
        /// </summary>
        public const string NOT_TRANSLATED = "Resource not found";

        #endregion

        #region Helper

        /// <summary>
        /// Get the two letter language ISO code from a <see cref="ClydeCulture"/>
        /// </summary>
        /// <param name="culture">The <see cref="ClydeCulture"/></param>
        /// <returns>The two letter ISO code for <paramref name="culture"/></returns>
        public static string TwoLetterISOCode(this ClydeCulture culture)
        {
            switch (culture)
            {
                case ClydeCulture.English: return ENGLISH_ISO;
                case ClydeCulture.German: return GERMAN_ISO;
                case ClydeCulture.Spanish: return SPANISH_ISO;
                case ClydeCulture.Portuguese: return PORTUGUESE_ISO;
                case ClydeCulture.Russian: return RUSSIAN_ISO;
                case ClydeCulture.Arabic: return ARABIC_ISO;
                case ClydeCulture.Punjabi: return PUNJABI_ISO;
                case ClydeCulture.Bengali: return BENGALI_ISO;
                case ClydeCulture.Hindi: return HINDI_ISO;
                case ClydeCulture.Chinese: return CHINESE_ISO;
                case ClydeCulture.Japanese: return JAPANESE_ISO;
                case ClydeCulture.Korean: return KOREAN_ISO;
                case ClydeCulture.Italian: return ITALIAN_ISO;
                case ClydeCulture.French: return FRENCH_ISO;
                default: return ENGLISH_ISO;
            }
        }

        /// <summary>
        /// Convert a two letter language ISO code to a <see cref="ClydeCulture"/>
        /// <para>If two letter ISO code is not supported, default will be <see cref="ClydeCulture.English"/></para>
        /// </summary>
        /// <param name="culture">The two letter ISO Code for a language</param>
        /// <returns>The <see cref="ClydeCulture"/></returns>
        public static ClydeCulture Parse(string culture)
        {
            switch (culture?.ToLower() ?? ENGLISH_ISO) 
            { 
                case ENGLISH_ISO: return ClydeCulture.English;
                case GERMAN_ISO: return ClydeCulture.German;
                case SPANISH_ISO: return ClydeCulture.Spanish;
                case RUSSIAN_ISO: return ClydeCulture.Russian;
                case ARABIC_ISO: return ClydeCulture.Arabic;
                case PUNJABI_ISO: return ClydeCulture.Punjabi;
                case BENGALI_ISO: return ClydeCulture.Bengali;
                case HINDI_ISO: return ClydeCulture.Hindi;
                case CHINESE_ISO: return ClydeCulture.Chinese;
                case JAPANESE_ISO: return ClydeCulture.Japanese;
                case KOREAN_ISO: return ClydeCulture.Korean;
                case ITALIAN_ISO: return ClydeCulture.Italian;
                case FRENCH_ISO: return ClydeCulture.French;
                default: return ClydeCulture.English;
            }
        }

        /// <summary>
        /// Get the <see cref="CultureInfo"/> from a <see cref="ClydeCulture"/>
        /// </summary>
        /// <param name="culture">The <see cref="ClydeCulture"/></param>
        /// <returns>A instance of <see cref="CultureInfo"/> for <paramref name="culture"/></returns>
        public static CultureInfo GetCultureInfo(this ClydeCulture culture)
            => CultureInfo.GetCultureInfo(TwoLetterISOCode(culture));

        /// <summary>
        /// Converts a <see cref="CultureInfo"/> to the corresponding <see cref="ClydeCulture"/>
        /// </summary>
        /// <param name="culture">The <see cref="CultureInfo"/> that should be converted </param>
        /// <returns>
        /// The corresponding <see cref="ClydeCulture"/>
        /// <para>If the corresponding <see cref="ClydeCulture"/> does not exists <see cref="ClydeCulture.English"/> will be returned </para>
        /// </returns>
        public static ClydeCulture Convert(CultureInfo culture)
            => culture.TwoLetterISOLanguageName.Length == 2 ? Parse(culture.TwoLetterISOLanguageName) : ClydeCulture.English;

        /// <summary>
        /// Creates a instance of the default <see cref="ILanguageManager"/>
        /// </summary>
        public static ILanguageManager DefaultLanguageManager
            => new DefaultLanguageManager();

        #endregion
    }

}

