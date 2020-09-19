namespace Push.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig00 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Detalle",
                c => new
                    {
                        DetalleId = c.Int(nullable: false, identity: true),
                        EncabezadoID = c.Int(nullable: false),
                        TipoRegistro = c.String(),
                        TipoIdEmpleado = c.String(),
                        EmpleadoId = c.String(),
                        Sueldo = c.String(),
                        SueldoNeto = c.String(),
                        NoSeguridadSocial = c.String(),
                    })
                .PrimaryKey(t => t.DetalleId)
                .ForeignKey("dbo.Encabezado", t => t.EncabezadoID, cascadeDelete: true)
                .Index(t => t.EncabezadoID);
            
            CreateTable(
                "dbo.Encabezado",
                c => new
                    {
                        EncabezadoId = c.Int(nullable: false, identity: true),
                        TipoRegistro = c.String(),
                        TipoArchivo = c.String(),
                        Identificacion = c.String(),
                        Periodo = c.String(),
                    })
                .PrimaryKey(t => t.EncabezadoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Detalle", "EncabezadoID", "dbo.Encabezado");
            DropIndex("dbo.Detalle", new[] { "EncabezadoID" });
            DropTable("dbo.Encabezado");
            DropTable("dbo.Detalle");
        }
    }
}
