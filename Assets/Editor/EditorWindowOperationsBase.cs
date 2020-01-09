namespace LocalizationEditor
{
    internal abstract class EditorWindowOperationsBase
    {
        public static string FormatStringForButton(string unformattedString)
        {
            return unformattedString.Replace('_', ' ');
        }

        public static string FormatStringForTranslationKey(string unformattedString)
        {
            return unformattedString.Replace(' ', '_');
        }

        public static string FormatStringForFieldName(string unformattedString)
        {
            var formattedString = "";

            foreach (var c in unformattedString)
            {
                if (char.IsLetterOrDigit(c))
                {
                    formattedString += c;
                }
            }

            formattedString += LocalizationEditorData.FieldNameSuffix;

            return formattedString;
        }
    }
}