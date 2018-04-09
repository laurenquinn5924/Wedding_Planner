using System;
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models
{
    public class RSVP : BaseEntity
    {
        [Key]
        public int RSVPSId { get; set; }
        public int GuestWeddingId { get; set; } //Foreign Key
        public int UserId { get; set; } //Foreign Key
        public User User { get; set; }
        public Wedding GuestWedding { get; set; }
    }
}