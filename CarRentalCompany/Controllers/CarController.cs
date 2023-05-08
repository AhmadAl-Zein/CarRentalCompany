using CarRentalCompany.Dto;
using CarRentalCompany.Models;
using CarRentalCompany.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalCompany.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private CarService carService;

        public CarController(CarService _carService)
        {
            carService = _carService;
        }

        [HttpPost("Add")]
        public async Task<ResponseDto> AddCar(CarDto body)
        {
            return new ResponseDto()
            {
                Success = true,
                Data = await carService.addCar(body)
            };
        }

        [HttpPut("Update/{id}")]
        public async Task<ResponseDto> UpdateCar(Guid id, CarDto body)
        {
            await carService.updateCar(id, body);
            return new ResponseDto()
            {
                Success = true,
                Data = "Car Updated Successfully..."
            };
        }

        [HttpPost("Get")]
        public async Task<ResponseDto> GetCars(GetCarsDto filter, string? sorting, int page, int size)
        {
            var cars = await carService.getCars(filter, sorting, page, size);
            return new ResponseDto()
            {
                Success = true,
                Data = cars
            };
        }

        [HttpGet("Get/Cache")]
        public async Task<ResponseDto> GetCarsFromCache()
        {
            return new ResponseDto()
            {
                Success = true,
                Data = await carService.getCarsFromCache()
            };
        }

        [HttpGet("Get/{id}")]
        public async Task<ResponseDto> GetCarById(Guid id)
        {
            return new ResponseDto()
            {
                Success = true,
                Data = await carService.getCarById(id)
            };
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ResponseDto> DeleteCar(Guid id)
        {
            await carService.deleteCar(id);
            return new ResponseDto()
            {
                Success = true,
                Data = "Car Deleted Successfully..."
            };
        }
    }
}
