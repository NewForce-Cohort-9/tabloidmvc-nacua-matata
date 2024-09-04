using System.ComponentModel;

namespace TabloidMVC.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DisplayName("Username")]
        public string DisplayName { get; set; }
        public string Email { get; set; }

        [DisplayName("Date Joined")]
        public DateTime CreateDateTime { get; set; }

        [DisplayName("Profile Picture")]
        public string ImageLocation { get; set; }
        public int UserTypeId { get; set; }
        public UserType UserType { get; set; }
        public Boolean IsActive { get; set; }
        [DisplayName("Full Name")]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}
