﻿namespace Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Ip { get; set; }
        public int Lives { get; set; }
    }
}