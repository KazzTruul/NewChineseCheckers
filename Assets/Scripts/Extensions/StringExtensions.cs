using System.Text.RegularExpressions;

public static class StringExtensions
{
    private const string _validTranslationKeyPattern = @"^\b([A-Z][a-z]*[_ ]?)*([A-Z][a-z]*)\b$";

    public static bool IsValidTranslationKey(this string potentialTranslationKey)
    {
        return !string.IsNullOrEmpty(potentialTranslationKey)
            && Regex.IsMatch(potentialTranslationKey, _validTranslationKeyPattern);
    }
}