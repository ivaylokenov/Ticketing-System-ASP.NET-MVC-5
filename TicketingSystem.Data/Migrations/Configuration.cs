namespace TicketingSystem.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Microsoft.AspNet.Identity;

    using TicketingSystem.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using TicketingSystem.Common;
    using System.Collections.Generic;
    using System.Reflection;
    using System.IO;

    public sealed class Configuration : DbMigrationsConfiguration<TicketSystemDbContext>
    {
        private UserManager<User> userManager;
        private IRandomGenerator random;

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.random = new RandomGenerator();
        }

        protected override void Seed(TicketSystemDbContext context)
        {
            this.userManager = new UserManager<User>(new UserStore<User>(context));
            this.SeedRoles(context);
            this.SeedUsers(context);
            this.SeedCategoriesWithTicketsWithComments(context);
        }

        private void SeedRoles(TicketSystemDbContext context)
        {
            context.Roles.AddOrUpdate(x => x.Name, new IdentityRole(GlobalConstants.AdminRole));
            context.SaveChanges();
        }

        private void SeedUsers(TicketSystemDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            for (int i = 0; i < 10; i++)
            {
                var user = new User
                    {
                        Email = string.Format("{0}@{1}.com", this.random.RandomString(6, 16), this.random.RandomString(6, 16)),
                        UserName = this.random.RandomString(6, 16)
                    };

                this.userManager.Create(user, "123456");
            }

            var adminUser = new User
            {
                Email = "admin@mysite.com",
                UserName = "Administrator"
            };

            this.userManager.Create(adminUser, "admin123456");

            this.userManager.AddToRole(adminUser.Id, GlobalConstants.AdminRole);
        }

        private void SeedCategoriesWithTicketsWithComments(TicketSystemDbContext context)
        {
            if (context.Categories.Any())
            {
                return;
            }

            var image = this.GetSampleImage();
            var users = context.Users.Take(10).ToList();
            for (int i = 0; i < 5; i++)
            {
                var category = new Category
                {
                    Name = this.random.RandomString(5, 20)
                };

                for (int j = 0; j < 10; j++)
                {
                    var ticket = new Ticket
                    {
                        Author = users[this.random.RandomNumber(0, users.Count - 1)],
                        Title = this.random.RandomString(5, 40),
                        Description = this.random.RandomString(200, 500),
                        Image = image,
                        Priority = (PriorityType)this.random.RandomNumber(0, 2)
                    };

                    for (int k = 0; k < 5; k++)
                    {
                        var comment = new Comment
                        {
                            Author = users[this.random.RandomNumber(0, users.Count - 1)],
                            Content = this.random.RandomString(100, 200)
                        };

                        ticket.Comments.Add(comment);
                    }

                    category.Tickets.Add(ticket);
                }

                context.Categories.Add(category);
                context.SaveChanges();
            }
        }

        private Image GetSampleImage()
        {
            var directory = AssemblyHelpers.GetDirectoryForAssembyl(Assembly.GetExecutingAssembly());
            var file = File.ReadAllBytes(directory + "/Migrations/Imgs/cat.jpg");
            var image = new Image
            {
                Content = file,
                FileExtension = "jpg"
            };

            return image;
        }
    }
}
