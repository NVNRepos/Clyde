using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Clyde.Globalization.Test
{
    [TestClass]
    public class Translation
    {
        [TestMethod("Check all Fallback values")]
        public void CheckFallback()
        {
            ILanguageManager languageManager = ClydeGlobalization.DefaultLanguageManager;
            languageManager.Culture = ClydeCulture.English; //English is the fallback culture
            List<string> errors = new List<string>();

            foreach (string key in GlobalizeString.GetAllKeys())
                if (languageManager.GetCulturedString(key) == ClydeGlobalization.NOT_TRANSLATED)
                    errors.Add($"\nMissing fallback value for Key: {key}");
            

            if (errors.Count > 0)
                Assert.Fail(string.Join(string.Empty, errors));
        }

        [TestMethod("Check if all translations are in the Resource.resx file available")]
        public void CheckResourceTranslations()
        {
            ILanguageManager languageManager = ClydeGlobalization.DefaultLanguageManager;
            languageManager.Culture = ClydeCulture.English;
            Dictionary<string, string> fallBack = GlobalizeString.GetAllKeys()
                .ToDictionary(key => key, key => languageManager.GetCulturedString(key));
            List<string> errors = new List<string>();

            foreach (ClydeCulture culture in Enum.GetValues(typeof(ClydeCulture))) //.Except is not available and .net8 collection expressions are also not available, sucks
            {
                if (culture == ClydeCulture.English) // english is the fallback culture
                    continue;

                languageManager.Culture = culture;
                foreach (string key in GlobalizeString.GetAllKeys())
                    if (languageManager.GetCulturedString(key) == fallBack[key])
                        errors.Add($"\nTranslation is missing in {languageManager.Culture} ({languageManager.Culture.TwoLetterISOCode()}) for {key}.");
            }

            if (errors.Count > 0)
                Assert.Fail(string.Join(string.Empty, errors));
        }
    }
}
