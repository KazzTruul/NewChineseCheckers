using System.Text.RegularExpressions;

public class VerifyUsernameValidityStrategy
{
    private string _validUsernamePattern = @"[a-zA-Z0-9]";

    public bool IsValidUsername(string potentialUsername)
    {
        return !string.IsNullOrEmpty(potentialUsername)
            && Regex.IsMatch(potentialUsername, _validUsernamePattern)
            && potentialUsername.Length >= Constants.MinUsernameLength
            && potentialUsername.Length <= Constants.MaxUsernameLength;
    }
}