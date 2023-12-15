using Online_Exam.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Online_Exam.Models
{
    public class Users
    {
        [Key]
        [Required]
        [EmailAddress]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please Enter Valid E-mail")]
        [DisplayName("Email Address")]
        public string U_Email { get; set; }

        [Required]
        [DisplayName("Full Name")]
        public string U_FullName { get; set; }

        [Required]
        [StringLength(11)]
        [RegularExpression(@"01[0-2]\d{8}|015\d{8}", ErrorMessage = "Invalid phone number")]
        [DisplayName("Phone Number")]
        public string U_PhoneNumber { get; set; }

        [Required]
        [DisplayName("Photo")]
        public string U_Photo { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", ErrorMessage = "Password requirements not met")]
        [DisplayName("Password")]
        public string U_Password { get; set; }
        [Compare("U_Password", ErrorMessage = "The password and confirmation password do not match.")]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }

        public virtual ICollection<Answers> Answers { get; set; } = new List<Answers>();

        public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();

    }
}
