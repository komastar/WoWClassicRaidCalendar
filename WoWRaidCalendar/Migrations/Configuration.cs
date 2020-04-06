namespace WCRC.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.Migrations;
    using WCRC.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "WCRC.Models.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            IdentityResult IdRoleResult;
            IdentityResult IdUserResult;
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            if (!roleManager.RoleExists("Admin"))
            {
                IdRoleResult = roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var admin = new ApplicationUser { UserName = "", Email = "" };
            IdUserResult = userManager.Create(admin, "");
            var adminId = userManager.FindByName(admin.UserName).Id;
            IdUserResult = userManager.AddToRole(adminId, "");
        }
    }
}
