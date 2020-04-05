﻿namespace SimpulBlog.Core.Settings
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public int ExpiryTime { get; set; }
    }
}
