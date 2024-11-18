using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TripsLogApp.Models
{
    public class Trip
    {
        [Key]
        public int TripId { get; set; }

        [Required]
        public string Destination { get; set; }

        [Required]
        public DateOnly StartDate { get; set; }

        [Required]
        public DateOnly EndDate { get; set; }

        [AllowNull]
        public string? Accommodation { get; set; }

        [AllowNull]
        [Phone]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage ="Requires valid phone number")]
        public string? AccommodationPhone { get; set; }

        [AllowNull]
        [EmailAddress(ErrorMessage ="requires valid email address")]
        public string? AccommodationEmail { get; set; }

        [AllowNull]
        public string? ThingToDo1 { get; set; }

        [AllowNull]
        public string? ThingToDo2 { get; set; }

        [AllowNull]
        public string? ThingToDo3 { get; set; }
    }
}
