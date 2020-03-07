using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroConDetalle.Entidades
{
    public class TelefonosDetalle
    {
        [Key]
        public int TelId { get; set; }
        public string TipoTelefono { get;  }
        public string Telefono { get; set; }

        public TelefonosDetalle()
        {
            TelId = 0;
            TipoTelefono = string.Empty;
            Telefono = string.Empty;
        }
    }
}
