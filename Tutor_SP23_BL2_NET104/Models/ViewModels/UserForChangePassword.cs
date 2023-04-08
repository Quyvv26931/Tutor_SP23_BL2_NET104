namespace Tutor_SP23_BL2_NET104.Models.ViewModels
{
    public class UserForChangePassword
    {
        public string Username { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
