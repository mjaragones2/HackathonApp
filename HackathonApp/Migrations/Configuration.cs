namespace HackathonApp.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Configuration;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Data.Entity.SqlServer;
    using System.Collections.Generic;
    using System.Data.Entity.Spatial;
    using HackathonApp.Models;
    using Constants = Models.Constants;

    internal sealed class Configuration : DbMigrationsConfiguration<HackathonApp.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "HackathonApp.Models.ApplicationDbContext";
        }

        protected override void Seed(HackathonApp.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "techstack.heand123@gmail.com";
                user.Email = "techstack.heand123@gmail.com";
                user.EmailConfirmed = true;

                string userPWD = "TechStack@123";

                var chkUser = UserManager.Create(user, userPWD);

                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
            }

            // creating Creating Regular User role     
            if (!roleManager.RoleExists(Constants.ConsUser))
            {
                var role = new IdentityRole();
                role.Name = Constants.ConsUser;
                roleManager.Create(role);
            }


        }
    }
}