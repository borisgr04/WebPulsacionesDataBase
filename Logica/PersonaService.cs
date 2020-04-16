using Datos;
using Entity;
using System;
using System.Collections.Generic;

namespace Logica
{
    public class PersonaService
    {
        private readonly ConnectionManager _conexion;
        private readonly PersonaRepository _repositorio;
        public PersonaService(string connectionString)
        {
            _conexion = new ConnectionManager(connectionString);
            _repositorio = new PersonaRepository(_conexion);
        }
        public GuardarPersonaResponse Guardar(Persona persona)
        {
            try
            {
                  _conexion.Open();
                var personaAux = _repositorio.BuscarPorIdentificacion(persona.Identificacion);
                if (personaAux != null)
                {
                    return new GuardarPersonaResponse($"Error de la Aplicacion: La persona ya se encuentra registrada!");
                }        
                persona.CalcularPulsaciones();
              
                _repositorio.Guardar(persona);
                _conexion.Close();
                return new GuardarPersonaResponse(persona);
            }
            catch (Exception e)
            {
                return new GuardarPersonaResponse($"Error de la Aplicacion: {e.Message}");
            }
            finally { _conexion.Close(); }
        }
        public List<Persona> ConsultarTodos()
        {
            _conexion.Open();
            List<Persona> personas = _repositorio.ConsultarTodos();
            _conexion.Close();
            return personas;
        }
        public string Eliminar(string identificacion)
        {
            try
            {
                _conexion.Open();
                var persona = _repositorio.BuscarPorIdentificacion(identificacion);
                if (persona != null)
                {
                    _repositorio.Eliminar(persona);
                    _conexion.Close();
                    return ($"El registro {persona.Nombre} se ha eliminado satisfactoriamente.");
                }
                else
                {
                    return ($"Lo sentimos, {identificacion} no se encuentra registrada.");
                }
            }
            catch (Exception e)
            {

                return $"Error de la Aplicación: {e.Message}";
            }
            finally { _conexion.Close(); }

        }
        public Persona BuscarxIdentificacion(string identificacion)
        {
            _conexion.Open();
            Persona persona = _repositorio.BuscarPorIdentificacion(identificacion);
            _conexion.Close();
            return persona;
        }
        public int Totalizar()
        {
            return _repositorio.Totalizar();
        }
        public int TotalizarMujeres()
        {
            return _repositorio.TotalizarMujeres();
        }
        public int TotalizarHombres()
        {
            return _repositorio.TotalizarHombres();
        }
    }

    public class GuardarPersonaResponse 
    {
        public GuardarPersonaResponse(Persona persona)
        {
            Error = false;
            Persona = persona;
        }
        public GuardarPersonaResponse(string mensaje)
        {
            Error = true;
            Mensaje = mensaje;
        }
        public bool Error { get; set; }
        public string Mensaje { get; set; }
        public Persona Persona { get; set; }
    }
}
