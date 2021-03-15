using System.Linq;
using GlupiApp.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace GlupiApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            InitializeDb();
            CreateHostBuilder(args).Build().Run();
        }

        private static void InitializeDb()
        {
            var opt = new DbContextOptionsBuilder<AppDbContext>();
            opt.UseSqlite("Data Source=app.db");

            var db = new AppDbContext(opt.Options);
            db.Database.EnsureCreated();

            if (db.Users.Any())
                return;

            var admin = new User
            {
                UserName = "Admin",
                Password = "PustiMeUnutra"
            };

            db.Users.Add(admin);

            var person = new Person
            {
                FirstName = "Ivan",
                LastName = "Gundulić",
                OIB = "hehehehehe",
                Gender = 1,
            };
            db.Persons.Add(person);
            db.SaveChanges();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
