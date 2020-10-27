using Blog.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

//
// BUGS TODO 
// Fix the ability to go to page numbers by changing the URL
// style rest of website and optomize mobile 
// When sign in is pressed without any entries, the site throws an error page
// 
// FEATURES TODO 
// Asign usernames to comment section
// Continute with styling for comment section 
// Add email services
// More user features: saving posts, contributing, account pages, 
// Possibly impliment a search bar
// 
// STYLING TODO
// change categories to drop downs
// Make the comment section look better


namespace Blog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            try
            {

                var scope = host.Services.CreateScope();

                var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                // Handles the user account. 
                var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                // Handles roles that can be assigned to user.
                var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                // Apply latest migrations. 
                ctx.Database.EnsureCreated();

                var adminRole = new IdentityRole("Admin");
                if (!ctx.Roles.Any())
                {
                    // GetAwaiter is used in lieu of await since the rest of func is not async. 
                    // create roles 
                    roleMgr.CreateAsync(adminRole).GetAwaiter().GetResult();
                }

                if (!ctx.Users.Any(u => u.UserName == "admin"))
                {
                    // create an admin if one doesn't exist.
                    var adminUser = new IdentityUser
                    {
                        UserName = "admin",
                        Email = "admin@test.com"
                    };
                    userMgr.CreateAsync(adminUser, "password").GetAwaiter().GetResult();
                    // Add role to user. 
                    userMgr.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message); 
            }
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
