using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroConDetalle.Entidades
{
    public class Personas
    {
        [Key]
        public int PersonaId { get; set; }
        public string Nombre { get; set; }
        public string Cedula { get; set; }
        public string Direcion { get; set; }
        public DateTime FechaNacimineto { get; set; }

        [ForeignKey("PersonasId")]
        public virtual List<TelefonosDetalle> Telefonos { get; set; }

        public Personas()
        {
            PersonaId = 0;
            Nombre = string.Empty;
            Cedula = string.Empty;
            Direcion = string.Empty;
            FechaNacimineto = DateTime.Now;
            Telefonos = new List<TelefonosDetalle>();    
        }
        
    }
}
