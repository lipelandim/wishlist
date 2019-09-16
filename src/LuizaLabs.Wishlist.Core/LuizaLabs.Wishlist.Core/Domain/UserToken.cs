using System;
using System.Collections.Generic;
using System.Text;

namespace LuizaLabs.Wishlist.Core.Domain
{
    public class UserToken
    {
        public long UserId { get; set; }
        public List<KeyValuePair<string, bool>> Profiles { get; set; }
        public EditPermissions EditPermissions { get; set; }
    }

    public class EditPermissions
    {
        public bool Event { get; set; }
        public bool EventType { get; set; }
        public bool Compliance { get; set; }
    }
}