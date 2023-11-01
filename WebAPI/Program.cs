using DataLayer;
using DataLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(options =>
			{
				options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Type = SecuritySchemeType.Http,
					Scheme = "bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
					Description = "Enter the JWT token obtained from the login endpoint",
					Name = "Authorization"
				});
				options.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						Array.Empty<string>()
					}
				});
			});

			builder.Services.AddAuthentication(option =>
			{
				option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(option =>
			{
				option.RequireHttpsMetadata = false;
				option.SaveToken = true;
				option.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidIssuer = "YourIssuer",
					ValidateAudience = true,
					ValidAudience = "YourAudience",
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("c2VydmVwZXJmZWN0bHljaGVlc2VxdWlja2NvYWNoY29sbGVjdHNsb3Bld2lzZWNhbWU=")),
					ValidateLifetime = true,
					ClockSkew = TimeSpan.Zero // Optional, allows for some time difference between the server and client.
				};
			});
			// Add services to the container.
			builder.Services.AddDbContext<SswdatabaseContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("Local"));
			});
			builder.Services.AddScoped(typeof(GenericRepository<>));
			builder.Services.AddScoped<AccountRepository>();
			builder.Services.AddScoped<ProductRepository>();
			builder.Services.AddScoped<ServiceRepository>();
			builder.Services.AddScoped<OrderRepository>();
			builder.Services.AddScoped<ImageRepository>();
			builder.Services.AddScoped<AccountDetailRepository>();
			builder.Services.AddScoped<PaymentRepository>();
			builder.Services.AddScoped<OrderServiceRepository>();
			builder.Services.AddScoped<OrderProductRepository>();

			//CORS handler
			var CORS_CONFIG = "_CORS_CONFIG";
			builder.Services.AddCors(options =>
			{
				options.AddPolicy(name: CORS_CONFIG,
					builder => builder.AllowAnyOrigin()
						.AllowAnyMethod()
						.AllowAnyHeader());
			});

			var app = builder.Build();



			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseCors(CORS_CONFIG);
			app.UseHttpsRedirection();

			app.UseAuthentication();
			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}