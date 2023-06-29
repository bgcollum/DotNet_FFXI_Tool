namespace MVCPresentation.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MVCPresentation.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<MVCPresentation.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "MVCPresentation.Models.ApplicationDbContext";
        }

        protected override void Seed(MVCPresentation.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            const string admin = "admin@company.com";
            const string adminPassword = "P@ssw0rd";

            // Roles
            // Note: This is INTENTIONALLY omitting the using LogicLayer.UserManager statement, since there is a naming conflict from the ASP.NET User Manager

            LogicLayer.UserManager localUserManager = new LogicLayer.UserManager();
            var roles = localUserManager.RetrieveUserRoles();

            foreach (var role in roles)
            {
                context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = role });
            }
            if (!roles.Contains("Administrator"))
            {
                // Note: Even though Administrator should be in the list of roles, this check is to remove any risk of it being missing due to deletion from the internal database
                context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "Administrator" });
            }
            if (!context.Users.Any(u => u.UserName == admin))
            {
                var user = new ApplicationUser()
                {
                    UserName = admin,
                    Email = admin,
                    DisplayName = "Admin"
                };
                IdentityResult result = userManager.Create(user, adminPassword);
                context.SaveChanges();  // Updates the database

                // Add the Administrator role to admin
                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Administrator");
                    context.SaveChanges();
                }
            }
        }
    }
}
