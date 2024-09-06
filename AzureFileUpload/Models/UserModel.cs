using System.ComponentModel.DataAnnotations;

namespace AzureFileUpload.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = "Admin";

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "12345";
    }
}
