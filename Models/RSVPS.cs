using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace CBelt.Models
{
    public class RSVP
    {
        [Key]
        [Required]
        public int rsvpid{get;set;}
        [Required]
        public int userid{get;set;}
        [Required]
        public User user{get;set;}
        [Required]
        public int activityid{get;set;}
        [Required]
        public Activity activity{get;set;}
    }
}