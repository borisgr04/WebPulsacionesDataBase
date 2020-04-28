using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebPulsaciones.Models
{
    public class PersonaInputModel
    {
        [Required]
        public string Identificacion { get; set; }
        [Required(ErrorMessage ="El Nombre es requerido")]
        public string Nombre { get; set; }
        [Required]
        [Range(0,99, ErrorMessage ="Especifique una edad entre 0 y 99")]
        public int Edad { get; set; }
        [Required]
        [SexValidation(ErrorMessage = "Especifique un sexo [M ó F]")]
        public string Sexo { get; set; }
    }
    
    public class PersonaViewModel : PersonaInputModel
    {
        public PersonaViewModel()
        {

        }
        public PersonaViewModel(Persona persona)
        {
            Identificacion = persona.Identificacion;
            Nombre = persona.Nombre;
            Edad = persona.Edad;
            Sexo = persona.Sexo;
            Pulsacion = persona.Pulsacion;
        }
        public decimal Pulsacion { get; set; }
    }

    public class SexValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (Convert.ToString(value) == "M" || Convert.ToString(value) == "F")
                return ValidationResult.Success;
            else
                return new ValidationResult(ErrorMessage);
        }
    }
}
