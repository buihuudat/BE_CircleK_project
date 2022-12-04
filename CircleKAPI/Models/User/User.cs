namespace CircleKAPI.Models.User
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public long Phone { get; set; }
        public string Username { get; set; } = string.Empty;
        public byte[] Password { get; set; } = new byte[32];
        public byte[] Salt { get; set; } = new byte[32];
        public string Avatar { get; set; } = "";
        public int Permission { get; set; } = 1;
        public string Address { get; set; } = string.Empty;
    }
}
