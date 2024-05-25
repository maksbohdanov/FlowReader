using FlowReader.Application;
using FlowReader.DataAccess;
using FlowReader.DataAccess.Persistence;
using FlowReader.Middleware;

namespace FlowReader
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDataAccess(builder.Configuration);
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddApplication();

            //builder.Services.AddJwt(builder.Configuration);
            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization();

            builder.Services.AddRazorPages();
            builder.Services.AddControllersWithViews();
            
            builder.Services.AddTransient<ExceptionHandlingMiddleware>();

            var app = builder.Build();
            using(var scope = app.Services.CreateScope())
            {
                await AutomatedMigration.MigrateAsync(scope.ServiceProvider);
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseCors(corsPolicyBuilder =>
                corsPolicyBuilder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            );

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            //app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.Run();
        }
    }
}
