using device.Data;
using device.Entity;
using device.IRepository;
using device.IServices;
using device.Repository;
using device.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// CreateProducer services to the container.

//add dbcontext 
builder.Services.AddDbContext<LaptopDbContext>(opt => opt.UseNpgsql("DeviceDB"));

// Dependency Injection
builder.Services.AddScoped(typeof(IAllRepository<>), typeof(AllRepository<>));

builder.Services.AddScoped(typeof(IVgaService), typeof(VgaService));
builder.Services.AddScoped(typeof(IStorageService), typeof(StorageService));
builder.Services.AddScoped(typeof(IRamService),typeof( RamService));
builder.Services.AddScoped(typeof(IProducerService), typeof(ProducerService));
builder.Services.AddScoped(typeof(IMonitorService), typeof(MonitorService));  
builder.Services.AddScoped(typeof(ILaptopService), typeof(LaptopService));
builder.Services.AddScoped(typeof(ILaptopDetailService), typeof(LaptopDetailService));
builder.Services.AddScoped(typeof(IInvoiceService), typeof(InvoiceService));
builder.Services.AddScoped(typeof(IInvoiceDetailService), typeof(InvoiceDetailService));
builder.Services.AddScoped(typeof(IPcService), typeof(PcService));
builder.Services.AddScoped(typeof(IUserService), typeof(UserService));


builder.Services.AddTransient<UserManager<User>, UserManager<User>>();
builder.Services.AddTransient<SignInManager<User>, SignInManager<User>>();


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
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
