using System;
using System.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Pms.Common.Variable;

using Pms.Entity;
using Pms.Entity.Interface;
using Pms.Repository;

namespace Pms.Common.Helper
{
    public class Utility
    {
        public static string GenerateFullName(string firstName, string middleName, string lastName)
        {
            const string format = "{0}, {1}";

            return string.Format(format, lastName, firstName);
        }

        public static bool DateHasValue(DateTime? date)
        {
            bool hasValue = false;

            if (date.HasValue && date != default(DateTime))
            {
                hasValue = true;
            }

            return hasValue;
        }

        public static string LanguagePrefix()
        {
            const string stringFormat = "lang_{0}_";
            return string.Format(CultureInfo.CurrentCulture, stringFormat, Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);
        }

        public static IList<KeyValuePair<string, string>> GetLanguages()
        {
            IList<KeyValuePair<string, string>> languages = new List<KeyValuePair<string, string>>();

            if (ConfigurationManager.AppSettings.AllKeys.Contains(ConfigurationKey.CacheTimeOut))
            {
                string[] languageStored = ConfigurationManager.AppSettings[Variable.ConfigurationKey.Languages].Split(Convert.ToChar(Variable.GeneralString.CharacterComma));

                foreach (string language in languageStored)
                {
                    string[] split = language.Split(Convert.ToChar(Variable.GeneralString.CharacterColon));
                    languages.Add(new KeyValuePair<string, string>(split[0], split[1]));
                }
            }
            else
            {
                throw new Exception(Variable.ErrorMessage.ConfigurationMissing);
            }

            return languages;
        }

        public static IList<ICountry> GetCountries1(ILookupServiceRepository lookupServiceRepository)
        {
            string cacheKey = LanguagePrefix() + CacheKeys.CountryList;
            object countries = CacheManager.Get(cacheKey);

            if (countries == null)
            {
                IList<ICountry> returnList = lookupServiceRepository.GetAllCountry();
                CacheManager.Add(cacheKey, returnList);
                return returnList;
            }
            else
            {
                return (IList<ICountry>)countries;
            }
        }
    }
}
