using BLL.Interfaces.ICustamRepository;
using BLL.Interfaces.IService;
using BLL.Mapper;
using BLL.Repository;
using BLL.Service;
using DAL.DataBase;
using DAL.Extend;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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


            #region Connection String Service
            var connectionString = builder.Configuration.GetConnectionString("ApplicationConnection");

            builder.Services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(connectionString));

            #endregion


            #region Auto Mapper Service

            builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));

            #endregion


            #region Microsoft Identity Service

            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;

                options.User.RequireUniqueEmail = true;

                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
            })
            .AddEntityFrameworkStores<ApplicationContext>()
            .AddDefaultTokenProviders(); 

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
                        options =>
                        {
                            options.LoginPath = new PathString("/Account/Login");
                            options.AccessDeniedPath = new PathString("/Account/Login"); 
                        });

            #endregion


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
