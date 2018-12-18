using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace CBelt.Models
{
    public class User
    {
        [Key]
        public int userid{get;set;}
        [Required]
        public string firstname{get;set;}
        [Required]
        public string lastname{get;set;}
        [EmailAddress]
        [Required]
        [MinLength(7, ErrorMessage = "Email Minimum Length 7 Characters")]
        public string email{get;set;}
        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage= "Password Must Be 6 Characters Long")]
        public string password{get;set;}
        public List<RSVP> RSVPS {get;set;}
        [NotMapped]
        [Compare("password")]
        [DataType(DataType.Password)]
        public string confirm{get;set;}
        public DateTime CreatedAt{get;set;} = DateTime.Now;
        public DateTime UpdatedAt{get;set;} = DateTime.Now;



    }
    public class LoginUser
    {
        [Required]
        public string email{get;set;}
        [Required(ErrorMessage = "Must Use Your Individual Password")]
        public string password{get;set;}

    }
}