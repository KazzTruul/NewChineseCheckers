public static class TranslationCatalogExtensions
{
    private const string NotImplementedTranslation = "NOT IMPLEMENTED";
    public static void AddTranslation(this TranslationCatalog translationCatalog, string newTranslation)
    {
        translationCatalog.Translations.Add(newTranslation, NotImplementedTranslation);
    }

    public static void DeleteTranslation(this TranslationCatalog translationCatalog, string targetTranslation)
    {
        if (translationCatalog.Translations.ContainsKey(targetTranslation))
        {
            translationCatalog.Translations.Remove(targetTranslation);
        }
    }
}