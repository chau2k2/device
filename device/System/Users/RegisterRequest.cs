﻿using System.ComponentModel.DataAnnotations;

namespace device.System.Users
{
    public class RegisterRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
