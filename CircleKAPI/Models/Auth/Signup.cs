﻿namespace CircleKAPI.Models.Auth
{
    public class Signup
    {
        public long Phone { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
