using System.ComponentModel.DataAnnotations;

namespace CarRentalCompany.Models
{
    public class Car
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string CarNumber { get; set; } = "";

        [Required]
        public string Type { get; set; } = "";

        [Required]
        public double EngineCapacity { get; set; }

        [Required]
        public string Color { get; set; } = "";

        [Required]
        public double DailyFare { get; set; }

        [Required]
        public bool WithDriver { get; set; }

        public Guid? DriverId { get; set; }
    }
}
