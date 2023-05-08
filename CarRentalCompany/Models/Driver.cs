using System.ComponentModel.DataAnnotations;

namespace CarRentalCompany.Models
{
    public class Driver
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string MobileNumber { get; set; }
    }
}
