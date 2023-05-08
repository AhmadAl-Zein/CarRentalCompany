namespace CarRentalCompany.Dto
{
    public class GetCarsDto
    {
        public string CarNumber { get; set; }

        public string Type { get; set; }

        public double FromEngineCapacity { get; set; }
        
        public double ToEngineCapacity { get; set; }

        public string Color { get; set; }

        public double FromDailyFare { get; set; }
        
        public double ToDailyFare { get; set; }

        public bool? WithDriver { get; set; }
    }
}
