namespace APIMyAmbu.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;

    public class MyAmbu : DbContext
    {
        // El contexto se ha configurado para usar una cadena de conexión 'Model' del archivo 
        // de configuración de la aplicación (App.config o Web.config). De forma predeterminada, 
        // esta cadena de conexión tiene como destino la base de datos 'APIMyAmbu.Models.Model' de la instancia LocalDb. 
        // 
        // Si desea tener como destino una base de datos y/o un proveedor de base de datos diferente, 
        // modifique la cadena de conexión 'Model'  en el archivo de configuración de la aplicación.
        public MyAmbu()
            : base("name=MyAmbu")
        {
        }

        public System.Data.Entity.DbSet<APIMyAmbu.Models.Ambulacias> Ambulacias { get; set; }

        // Agregue un DbSet para cada tipo de entidad que desee incluir en el modelo. Para obtener más información 
        // sobre cómo configurar y usar un modelo Code First, vea http://go.microsoft.com/fwlink/?LinkId=390109.

         public virtual DbSet<Ambulacias> MyEntities { get; set; }
    }

    public class Ambulacias
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAmbulancia { get; set; }
        public string Nombre { get; set; }
        public virtual ICollection<UbicacionAmbulacias> UbicacionAmbulancias { get; set; }
    }

    public class UbicacionAmbulacias
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UbicacionAmbulancia { get; set; }
        public int IdAmbulancia { get; set; }
        public DateTime Fecha { get; set; }
        public Double Latitud { get; set; }
        public Double Longitud { get; set; }
        public Ambulacias Ambulacias { get; set; }
    }
}