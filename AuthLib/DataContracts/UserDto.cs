namespace AuthLib.DataContracts
{
    public class UserDto
    {

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public IEnumerable<RoleDto> Roles { get; set; }
        public UserDto(string id, string firstName, string lastName, string emailAddress, IEnumerable<RoleDto> roles)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            Roles = roles;
        }


    }
}
