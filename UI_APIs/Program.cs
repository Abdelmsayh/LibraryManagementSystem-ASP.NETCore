
using BLL.Interfaces.ICustamRepository;
using BLL.Interfaces.IService;
using BLL.Mapper;
using BLL.Repository;
using BLL.Service;
using DAL.DataBase;
using DAL.Extend;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

namespace UI_APIs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            #region Configure 



            #region JWT Configuration

            var jwtSettings = builder.Configuration.GetSection("Jwt");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

            // add authentication schema
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    ClockSkew = TimeSpan.Zero,
                    RoleClaimType = ClaimTypes.Role
                };
            });

            #endregion

            // Add services to the container.

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IMembersRepository, MemberRepository>();
            builder.Services.AddScoped<IUserRepository, UsersRepository>();
            builder.Services.AddScoped<IRolesRepository, RolesRepository>();
            builder.Services.AddScoped<IBorrowingTransactionRepository, BorrowingTransactionRepository>();
            builder.Services.AddScoped<IReservationTransactionRepository, ReservationTransactionRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IBooksService, BooksService>();
            builder.Services.AddScoped<IBorrowingTransactionService, BorrowingTransactionService>();
            builder.Services.AddScoped<IMembersService, MembersService>();
            builder.Services.AddScoped<IUsersService, UsersService>();
            builder.Services.AddScoped<IRolesService, RolesService>();
            builder.Services.AddScoped<IReservationTransactionService, ReservationTransactionService>();
            builder.Services.AddScoped<IUserManagementService, UserManagementService>();

            #region Swagger Service

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new OpenApiInfo { Title = "APIs - LibareryMS", Version = "v1" });

                // Include 'SecurityScheme' to use JWT Authentication
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put *ONLY* your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });

            });

            #endregion

            #region Connection String Service
            var connectionString = builder.Configuration.GetConnectionString("ApplicationConnection");

            builder.Services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(connectionString));

            #endregion


            #region Auto Mapper Service

            builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));

            #endregion


            #region Microsoft Identity Service

            builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);



            // Password Configuration
            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
            }).AddEntityFrameworkStores<ApplicationContext>();



            #endregion


            #region CORS Policy

            builder.Services.AddCors();

            #endregion


            #endregion


            builder.Services.AddControllers()
                .AddJsonOptions(x =>
                    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
                );

            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.UseCors(options => options
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());

            app.Run();
        }
    }
}
