namespace AuthLib.DataContracts

{
    public class PermissionDto
    {
        public PermissionDto(int id, string system, string controller, string action, string description)
        {
            Id = id;
            System = system;
            Controller = controller;
            Action = action;
            Description = description;
        }

        public int Id { get; set; }
        public string System { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
    }
}
