using System.Text.RegularExpressions;

//TODO: Convert into string extension
public class VerifyUsernameValidityStrategy
{
    private const string _validUsernamePattern = @"[a-zA-Z0-9]";

    public bool IsValidUsername(string potentialUsername)
    {
        return !string.IsNullOrEmpty(potentialUsername)
            && Regex.IsMatch(potentialUsername, _validUsernamePattern)
            && potentialUsername.Length >= Constants.MinUsernameLength
            && potentialUsername.Length <= Constants.MaxUsernameLength;
    }
}