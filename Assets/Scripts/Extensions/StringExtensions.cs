using System.Text.RegularExpressions;

public static class StringExtensions
{
    private const string ValidTranslationKeyRegexPattern = @"^\b([A-Z][a-z]*[_ ]?)*([A-Z][a-z]*)\b$";

    private const string FindInitialLettersRegexPattern = @"(^|[\s_])([a-z])";

    private const string ValidLanguageRegexPattern = @"^[a-z]{2,3}$";

    public static bool IsValidTranslationKey(this string potentialTranslationKey)
    {
        return !string.IsNullOrEmpty(potentialTranslationKey)
            && Regex.IsMatch(potentialTranslationKey, ValidTranslationKeyRegexPattern);
    }

    public static string CapitalizeInitialLetters(this string unformattedString)
    {
        return Regex.Replace(unformattedString, FindInitialLettersRegexPattern, c => c.ToString().ToUpper());
    }

    public static bool IsValidLanguage(this string potentialLanguage)
    {
        return !string.IsNullOrEmpty(potentialLanguage)
            && Regex.IsMatch(potentialLanguage, ValidLanguageRegexPattern);
    }
}