namespace APIMyAmbu.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<APIMyAmbu.Models.MyAmbu>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(APIMyAmbu.Models.MyAmbu context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.Ambulacias.AddOrUpdate(
              p => p.IdAmbulancia,
              new Ambulacias{ IdAmbulancia = 1, Nombre = "Clinica Santa Isabel" },
              new Ambulacias{ IdAmbulancia = 2, Nombre = "Clinica Laura Daniela" },
              new Ambulacias{ IdAmbulancia = 3, Nombre = "Clinica Valledupar" }
            );
            //
        }
    }
}
