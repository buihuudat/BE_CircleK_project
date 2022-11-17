namespace CircleKAPI.Models.User
{
    public class CreateUser
    {
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public long Phone { get; set; }
        public int Permission { get; set; } = 1;
        public string Address { get; set; } = string.Empty;
    }
}
