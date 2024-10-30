using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Clyde.Globalization.Test
{
    [TestClass]
    public class Translation
    {
        [TestMethod("Check if all translations are in the Resource.resx file available")]
        public void CheckResourceFileTranslations()
        {
            ILanguageManager languageManager = ClydeGlobalization.DefaultLanguageManager;
            List<string> errors = new List<string>();

            foreach (ClydeCulture culture in Enum.GetValues(typeof(ClydeCulture)))
            {
                languageManager.Culture = culture;
                foreach (string key in GlobalizeString.GetAllKeys())
                {
                    if (languageManager.GetCulturedString(key) == ClydeGlobalization.NOT_TRANSLATED)
                    {
                        errors.Add($"\n{key} is missing in {languageManager.Culture} ({languageManager.Culture.TwoLetterISOCode()}).");
                    }
                }
            }

            if (errors.Count > 0)
            {
                Assert.Fail(string.Join(string.Empty, errors));
            }
        }
    }
}
