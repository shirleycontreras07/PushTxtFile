using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Push.Models
{
    public class Detalle
    {
        public Detalle()
        {
            TipoRegistro = "D";
        }

        [Key]
        public int DetalleId { get; set; }

        public int EncabezadoID { get; set; }


        public string TipoRegistro { get; set; } 

        public string TipoIdEmpleado { get; set; }

        public string EmpleadoId { get; set; }

        public string Sueldo { get; set; }

        public string SueldoNeto { get; set; }

        public string NoSeguridadSocial { get; set; }

        public virtual Encabezado Encabezado { get; set;  }
    }
}