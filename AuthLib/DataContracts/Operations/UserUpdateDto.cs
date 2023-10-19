namespace AuthLib.DataContracts.Operations
{
    public class UserUpdateDto
    {

        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string PrimaryLanguageCode { get; set; }
        public UserUpdateDto(string firstName, string? lastName, string primaryLanguageCode)
        {
            FirstName = firstName;
            LastName = lastName;
            PrimaryLanguageCode = primaryLanguageCode;
        }
    }
}
