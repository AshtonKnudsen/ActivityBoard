using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace CBelt.Models
{
    public class Activity
    {
        [Key]
        public int activityid{get;set;}
        [Required]
        public DateTime date{get;set;}
        [Required]
        public int duration{get;set;}
        [Required]
        public string title{get;set;}
        [Required]
        public TimeSpan time{get;set;}
        [Required]
        public string description{get;set;}
        public User myuser{get;set;}
        public List<RSVP> RSVPS {get;set;}
    }
}