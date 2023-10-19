namespace AuthLib.DataContracts.Operations
{
    public class PermissionUpdateDto
    {
        public PermissionUpdateDto(string system, string controller, string action, string description)
        {
            System = system;
            Controller = controller;
            Action = action;
            Description = description;
        }

        public string System { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
    }
}