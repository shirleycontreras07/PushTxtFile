using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Push.Models
{
    public class Encabezado
    {
        public Encabezado()
        {
            this.Detalle = new HashSet<Detalle>();
            TipoArchivo = "AM";
            TipoRegistro = "E";
        }

        [Key]
        public int EncabezadoId { get; set; }
        
        public string TipoRegistro { get; set; } 

        public string TipoArchivo { get; set; } 

        public string Identificacion { get; set; }

        public string Periodo { get; set; }

        public virtual ICollection<Detalle> Detalle { get; set; }
    }
}