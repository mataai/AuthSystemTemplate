namespace AuthLib.DataContracts.Operations

{
    public class UserCreateDto
    {
        public string Id { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PrimaryLanguageCode { get; set; }
    }
}
