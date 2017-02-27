namespace MyAmbu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ambulancias",
                c => new
                    {
                        IdAmbulancia = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.IdAmbulancia);
            
            CreateTable(
                "dbo.Paramedicos",
                c => new
                    {
                        Cedula = c.String(nullable: false, maxLength: 128),
                        Nombres = c.String(),
                        Apellidos = c.String(),
                        Correo = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Cedula);
            
            CreateTable(
                "dbo.PedidoAmbulancias",
                c => new
                    {
                        IdPedidoAmbulancia = c.Int(nullable: false, identity: true),
                        IdPaciente = c.Int(nullable: false),
                        NumeroPacientes = c.Int(nullable: false),
                        TipoEmergencia = c.String(),
                        Latitud = c.Double(nullable: false),
                        Longitud = c.Double(nullable: false),
                        Direccion = c.String(),
                        Cedula = c.String(),
                        Calificacion = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        Estado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdPedidoAmbulancia);
            
            CreateTable(
                "dbo.UbicacionAmbulancias",
                c => new
                    {
                        UbicacionAmbulancia = c.Int(nullable: false, identity: true),
                        Cedula = c.String(maxLength: 128),
                        Fecha = c.DateTime(nullable: false),
                        Latitud = c.Double(nullable: false),
                        Longitud = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.UbicacionAmbulancia)
                .ForeignKey("dbo.Paramedicos", t => t.Cedula)
                .Index(t => t.Cedula);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UbicacionAmbulancias", "Cedula", "dbo.Paramedicos");
            DropIndex("dbo.UbicacionAmbulancias", new[] { "Cedula" });
            DropTable("dbo.UbicacionAmbulancias");
            DropTable("dbo.PedidoAmbulancias");
            DropTable("dbo.Paramedicos");
            DropTable("dbo.Ambulancias");
        }
    }
}
