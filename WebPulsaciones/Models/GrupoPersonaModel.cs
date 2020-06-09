using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPulsaciones.Models
{
    public class GrupoPersonaInputModel
    {
        public string Codigo { get; set; }
        public List<PersonaInputModel> Personas { get; set; }
    }
}
