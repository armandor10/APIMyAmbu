namespace MyAmbu.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;

    public class MyAmbuEntity : DbContext
    {
        // El contexto se ha configurado para usar una cadena de conexión 'MyAmbuEntity' del archivo 
        // de configuración de la aplicación (App.config o Web.config). De forma predeterminada, 
        // esta cadena de conexión tiene como destino la base de datos 'MyAmbu.Models.MyAmbuEntity' de la instancia LocalDb. 
        // 
        // Si desea tener como destino una base de datos y/o un proveedor de base de datos diferente, 
        // modifique la cadena de conexión 'MyAmbuEntity'  en el archivo de configuración de la aplicación.
        public MyAmbuEntity()
            : base("name=MyAmbuEntity")
        {
        }

        public System.Data.Entity.DbSet<MyAmbu.Models.Ambulancias> Ambulacias { get; set; }

        public System.Data.Entity.DbSet<MyAmbu.Models.UbicacionAmbulancias> UbicacionAmbulancias { get; set; }

        public System.Data.Entity.DbSet<MyAmbu.Models.Paramedicos> Paramedicos { get; set; }

        public System.Data.Entity.DbSet<MyAmbu.Models.PedidoAmbulancias> PedidoAmbulancias { get; set; }
        // Agregue un DbSet para cada tipo de entidad que desee incluir en el modelo. Para obtener más información 
        // sobre cómo configurar y usar un modelo Code First, vea http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    public class PedidoAmbulancias
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPedidoAmbulancia { get; set; }
        public int IdPaciente { get; set; }
        public int NumeroPacientes { get; set; }
        public String TipoEmergencia { get; set; }
        public Double Latitud { get; set; }
        public Double Longitud { get; set; }
        public String Direccion { get; set; }
        public String Cedula { get; set; }
        public int Calificacion { get; set; }
        public DateTime Fecha { get; set; }
        public Boolean Estado { get; set; }


    }

    public class Ambulancias
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAmbulancia { get; set; }
        public string Nombre { get; set; }
        
    }

    public class UbicacionAmbulancias
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UbicacionAmbulancia { get; set; }
        public String Cedula { get; set; }
        public DateTime Fecha { get; set; }
        public Double Latitud { get; set; }
        public Double Longitud { get; set; }
        public Paramedicos paramedicos { get; set; }
    }
    public class Paramedicos {

        [Key]
        public String Cedula { get; set; }
        public String Nombres { get; set; }
        public String Apellidos { get; set; }
        public String Correo { get; set; }
        public String Password { get; set; }
        

    }
}