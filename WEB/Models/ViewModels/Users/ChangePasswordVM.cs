﻿namespace WEB.Models.ViewModels.Users
{
    public class ChangePasswordVM
    {
        public Guid Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string CheckNewPassword { get; set; }
    }
}
