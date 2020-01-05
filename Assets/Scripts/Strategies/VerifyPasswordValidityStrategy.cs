using System.Text.RegularExpressions;

//TODO: Convert into string extension
public class VerifyPasswordValidityStrategy
{
    private const string _validPasswordPattern = @"[a-zA-Z0-9]";

    public bool IsValidPassword(string potentialPassword)
    {
        return !string.IsNullOrEmpty(potentialPassword)
            && Regex.IsMatch(potentialPassword, _validPasswordPattern)
            && potentialPassword.Length >= Constants.MinPasswordLength
            && potentialPassword.Length <= Constants.MaxPasswordLength;
    }
}