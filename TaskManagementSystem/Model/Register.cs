using System.ComponentModel.DataAnnotations;
using System.Runtime.Intrinsics.X86;
using System;

namespace TaskManagementSystem.Model
{
    public class Register
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }


    }


    }

