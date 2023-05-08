using AutoMapper;
using CarRentalCompany;
using CarRentalCompany.Data;
using CarRentalCompany.Dto;
using CarRentalCompany.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Dependency Injection
builder.Services.AddScoped<CarService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//AutoMapper
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

//DB Context
builder.Services.AddDbContext<RentCompanyDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

//Caching
builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(
    options =>
    {
        options.Run(async context =>
        {
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            context.Response.ContentType = "application/json";
            var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();
            if (null != exceptionObject)
            {
                var error = new ResponseDto()
                {
                    Success = false,
                    Data = exceptionObject.Error.Message
                };
                await context.Response.WriteAsJsonAsync(error).ConfigureAwait(false);
            }
        });
    }
);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
