﻿using System.Collections.Generic;

namespace OnlineCookbook.ViewModels
{
    public class ManageUsersViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty; 
        public string Email { get; set; } = string.Empty;
        public IList<string> Roles { get; set; } = new List<string>();
    }

}
