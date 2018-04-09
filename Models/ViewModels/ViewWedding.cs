using System;
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models
{
    public class AddWedding : BaseEntity
    {
     [Display(Name= "Wedder One")]
     [Required]
     [MinLength(2)]
     public string WedderOne { get; set;}

    [Display(Name = "Wedder Two")]
    [Required]
    [MinLength(2)]
    public string WedderTwo { get; set; }

    
    [Required]
    [Display(Name = "Date of Wedding")]
    [MyDate(ErrorMessage = "Date must be in Future")]
    public DateTime DateOfEvent { get; set; }

    [Display(Name = "Address of Event")]
    [Required]
    [MinLength(2)]
    public string Address { get; set; }
    }

    public class MyDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime d = Convert.ToDateTime(value);
            return d >= DateTime.Now;
        }
    }
}