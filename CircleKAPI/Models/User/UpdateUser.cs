namespace CircleKAPI.Models.User
{
    public class UpdateUser
    {
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public long Phone { get; set; }
        public int Permission { get; set; } = 1;
        public string Address { get; set; } = "";
        public string Avatar { get; set; } = "";
    }
}
