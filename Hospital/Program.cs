using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Hospital.Data;
using DAOControllers.ManagerControllers;
using DAOControllers;
using Models;

namespace Hospital
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var hospitalConnection = builder.Configuration.GetConnectionString("hospitalConnection");
            builder.Services.AddControllers();

            builder.Services.AddDbContext<DBContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("hospitalConnection")));

            builder.Services.AddScoped<IGenericRepository<Address>, DAOAddress>();
            builder.Services.AddScoped<IGenericRepository<Medicament>, DAOMedicament>();
            builder.Services.AddScoped<IGenericRepository<LaboratoryResult>, DAOLaboratoryResult>();
            builder.Services.AddScoped<IGenericRepository<Doctor>, DAODoctor>();
            builder.Services.AddScoped<IGenericRepository<Patient>, DAOPatient>();

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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
