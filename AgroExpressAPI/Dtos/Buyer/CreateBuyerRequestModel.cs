using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AgroExpressAPI.CustomValidation;

namespace AgroExpressAPI.Dtos.Buyer;
    public class CreateBuyerRequestModel
    {
        [Required]
        [MaxLength(10)]
        [DisplayName("User Name")]
        public string UserName{get; set;}
        [DisplayName("Profile Picture")]
        public string ProfilePicture {get; set;}
        [Required]
        [DisplayName("First Name")]
        public string FirstName{get; set;}
        [Required]
        [DisplayName("Last Name")]
        public string LastName{get; set;}
       [Required]
       [MaxLength(14)]
       [MinLength(10)]
       [DisplayName("Display Name")]
        public string PhoneNumber{get; set;}
        [Required]
        [DisplayName("Full Address")]
        public string FullAddress{get; set;}
        [Required]
        [DisplayName("Local Governmnet")]
        public string LocalGovernment {get; set;}
        [Required]
        public string State {get; set;}
        [Required]
        public string Gender{get; set;}
        [Required]
        [EmailAddress]
        [EmailValidation]
        public string Email {get;set;}
        [Required]
        [Compare("Email")]
        [DisplayName("Confirm Email")]
        [EmailValidation]
        public string ConfirmEmail {get;set;}
        [Required]
        [PasswordValidation]
        public string Password{get; set;}
        [Required]
        [DisplayName("Confirm Password")]
        [Compare("Password")]
        [PasswordValidation]
        public string ConfirmPassword{get; set;}
    }
