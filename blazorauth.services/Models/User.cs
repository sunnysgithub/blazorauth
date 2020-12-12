using System;
using System.Collections.Generic;

namespace blazorauth.services.Models
{
    public class User
    {
        public Guid Guid { get; private set; } = Guid.NewGuid();
        
        public string Username {get; set;}

        public List<string> Roles { get; private set; } = new List<string>();

    }
}
