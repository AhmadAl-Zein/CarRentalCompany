using System.ComponentModel.DataAnnotations;

namespace CarRentalCompany.Models
{
    public class Rent
    {
        [Key]
        public int Id { get; set; }

        public Guid CustomerId { get; set; }

        public Guid CarId { get; set; }

        public DateTime StartRentDate { get; set; }
        
        public DateTime EndRentDate { get; set; } 
    }
}
