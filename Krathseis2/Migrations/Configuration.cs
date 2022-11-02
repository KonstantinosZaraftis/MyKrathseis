namespace Krathseis2.Migrations
{
    using Krathseis2.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Krathseis2.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Krathseis2.Models.ApplicationDbContext context)
        {
            Reservation reservation1 = new Reservation() { Id = 1, Name = "Ankit", StartLocation = "New York", EndLocation = "Beijing" };
            Reservation reservation2 = new Reservation() { Id = 2, Name = "Bobby", StartLocation = "New Jersey", EndLocation = "Boston" };
            Reservation reservation3 = new Reservation() { Id = 3, Name = "Jacky", StartLocation = "London", EndLocation = "Paris" };

            Customer customer1 = new Customer() { Id = 1, Firstname = "Konstantinos", LastName = "Zaraftis", PhoneNumber = "6946337409", EmailAddress = "konstantinos.Zar@gmail.com" };
            Customer customer2 = new Customer() { Id = 2, Firstname = "Kobe", LastName = "Bryant", PhoneNumber = "6946337409", EmailAddress = "konstantinos.Zar@gmail.com" };
            Customer customer3 = new Customer() { Id = 3, Firstname = "Tom", LastName = "Cruise", PhoneNumber = "6946337409", EmailAddress = "konstantinos.Zar@gmail.com" };
            context.Reservations.AddOrUpdate(r => r.Name, reservation1, reservation2, reservation3);
            context.Customers.AddOrUpdate(c => c.Firstname, customer1, customer2, customer3);
         
        }
    }
}
