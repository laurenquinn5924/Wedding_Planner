using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models
{
    public class User : BaseEntity
    {
        [Key]
        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<RSVP> RSVPS {get; set;}

        public User()
        {
            RSVPS = new List<RSVP>();
        }
    }
}