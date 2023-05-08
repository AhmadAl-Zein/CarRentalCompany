using CarRentalCompany.Dto;
using CarRentalCompany.Models;

namespace CarRentalCompany.Services.Interfaces
{
    public interface ICarService
    {
        public Task<Car> addCar(CarDto body);
        public Task updateCar(Guid id, CarDto body);
        public Task<List<Car>> getCars(GetCarsDto filter, string? sorting, int page, int size);
        public Task<Car> getCarById(Guid id);
        public Task deleteCar(Guid id);
    }
}
