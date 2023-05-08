using AutoMapper;
using CarRentalCompany.Data;
using CarRentalCompany.Dto;
using CarRentalCompany.Models;
using CarRentalCompany.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CarRentalCompany.Services
{
    public class CarService : ICarService
    {
        private readonly RentCompanyDbContext _dbContext;
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;

        public CarService(IMapper mapper, RentCompanyDbContext dbContext, IMemoryCache cache)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _cache = cache;
        }

        public async Task<Car> addCar(CarDto body)
        {
            var car = _mapper.Map<Car>(body);
            await _dbContext.Cars.AddAsync(car);
            await _dbContext.SaveChangesAsync();

            return car;
        }
        public async Task updateCar(Guid id, CarDto body)
        {
            var car = _mapper.Map<Car>(body);
            car.Id = id;
            _dbContext.Cars.Update(car);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<Car>> getCars(GetCarsDto filter, string? sorting, int page,int size)
        {
            var cars = _dbContext.Cars.Where(x => x.CarNumber.Contains(filter.CarNumber) &&
                                                  x.Color.Contains(filter.Color) &&
                                                  x.Type.Contains(filter.Type) &&
                                                  x.DailyFare >= filter.FromDailyFare &&
                                                  x.DailyFare <= filter.ToDailyFare &&
                                                  x.EngineCapacity >= filter.FromEngineCapacity &&
                                                  x.EngineCapacity <= filter.ToEngineCapacity &&
                                                  filter.WithDriver == null ? true : x.WithDriver == filter.WithDriver).AsQueryable();
            var sortedCars = sorting == null ? cars.ToList() : sortCars(cars, sorting);
            var searchCars = sortedCars.Skip((page - 1) * size).Take(size).ToList();

            //Caching Cars
            _cache.Set("cars", searchCars);

            return searchCars;
        }
        public async Task<List<Car>> getCarsFromCache()
        {
            var cars = _cache.Get<List<Car>>("cars");

            return cars;
        }
        public async Task<Car> getCarById(Guid id)
        {
            return await _dbContext.Cars.FindAsync(id);
        }
        public async Task deleteCar(Guid id)
        {
            var car = await _dbContext.Cars.FindAsync(id);
            _dbContext.Cars.Remove(car);
            await _dbContext.SaveChangesAsync();
        }
        
        private List<Car> sortCars(IQueryable<Car> cars, string sorting)
        {
            sorting = sorting.ToUpper();
            if (sorting.StartsWith("CARNUMBER")) {
                return sorting.ToUpper().EndsWith("DESC") ? cars.OrderByDescending(x => x.CarNumber).ToList() : cars.OrderBy(x => x.CarNumber).ToList();
            } else if (sorting.StartsWith("COLOR"))
            {
                return sorting.ToUpper().EndsWith("DESC") ? cars.OrderByDescending(x => x.Color).ToList() : cars.OrderBy(x => x.Color).ToList();
            } else if (sorting.StartsWith("TYPE"))
            {
                return sorting.ToUpper().EndsWith("DESC") ? cars.OrderByDescending(x => x.Type).ToList() : cars.OrderBy(x => x.Type).ToList();
            } else if (sorting.StartsWith("ENGINECAPACITY"))
            {
                return sorting.ToUpper().EndsWith("DESC") ? cars.OrderByDescending(x => x.EngineCapacity).ToList() : cars.OrderBy(x => x.EngineCapacity).ToList();
            } else if (sorting.StartsWith("DAILYFARE"))
            {
                return sorting.ToUpper().EndsWith("DESC") ? cars.OrderByDescending(x => x.DailyFare).ToList() : cars.OrderBy(x => x.DailyFare).ToList();
            } else
            {
                return cars.ToList();
            }
        }
    }
}
