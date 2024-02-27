using device.Data;
using device.IRepository;
using device.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// CreateProducer services to the container.

//add dbcontext 
builder.Services.AddDbContext<LaptopDbContext>(opt => opt.UseNpgsql("DeviceDB"));

// Dependency Injection
builder.Services.AddScoped(typeof(IAllRepository<>), typeof(AllRepository<>));

builder.Services.AddControllers(option => option.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
