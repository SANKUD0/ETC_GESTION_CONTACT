namespace ETC_GESTION_CONTACT.Models
{
    public class UserRegisterModel
    {
        required
        public string FirstName { get; set; }
        required
        public string LastName { get; set; }
        required
        public string PhoneNumber { get; set; }
        required
        public string Email { get; set; }
        required
        public string Password { get; set; }
        required
        public string ConfirmPassword { get; set; }
    }
}
