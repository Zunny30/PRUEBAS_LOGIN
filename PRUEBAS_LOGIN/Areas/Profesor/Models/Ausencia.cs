using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRUEBAS_LOGIN.Areas.Profesor.Models
{
    public class Ausencia
    {

       
            public int IdAusencia { get; set; }

            public string Tipo { get; set; }   // Ausencia o Incapacidad

            public string Asunto { get; set; }

            public DateTime FechaInicio { get; set; }

            public DateTime FechaFin { get; set; }
        public int IdUsuario { get; set; }
    }
}
