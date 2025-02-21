
using Application.Database;
using Application.Interfaces;
using Domain;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {

        

            var builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            
            TestDBConnection(connectionString);

            


            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddScoped< ITransactionsService, TransactionsService>();
            builder.Services.AddScoped<DepositService>();
            builder.Services.AddScoped<ProcessTransactionFactory>();
            builder.Services.AddScoped<TransactionsService>();
            builder.Services.AddScoped<WithdrawalService>();


            builder.Services.AddDbContext<ApplicationDBContext>(options =>
                   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            IConfigurationSection configurationSection = builder.Configuration.GetSection(nameof(TransactionsOptions));
            builder.Services.Configure<TransactionsOptions>(configurationSection);

            builder.Services.AddScoped<IBankingDbContext>(
                provider => provider.GetRequiredService<ApplicationDBContext>());

            //builder.Services.AddHttpClient();

            builder.Services.AddHttpClient();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader();
                    });
            });

            var app = builder.Build();

            app.UseCors("AllowAllOrigins");

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
        }

        private static void TestDBConnection(string connectionString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("✅ Connection to SQL Server successful!");

                    using (SqlCommand command = new SqlCommand("SELECT GETDATE();", connection))
                    {
                        object result = command.ExecuteScalar();
                        Console.WriteLine($"📅 Server Date/Time: {result}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Connection failed:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
