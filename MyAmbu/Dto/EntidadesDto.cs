using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyAmbu.Dto
{
    public class AmbulanciasDto
    {
        public int IdAmbulancia { get; set; }
        public string Nombre { get; set; }
    }

    public class UbicacionAmbulanciasDto
    {
        public int UbicacionAmbulancia { get; set; }
        public String Cedula { get; set; }
        public DateTime Fecha { get; set; }
        public Double Latitud { get; set; }
        public Double Longitud { get; set; }
        
    }
    public class UbicacionPacienteDto
    {
        public int IdPaciente { get; set; }
        public int NumeroPacientes { get; set; }
        public String TipoEmergencia { get; set; }
        public Double Latitud { get; set; }
        public Double Longitud { get; set; }
        public String Direccion { get; set; }

    }

    public class DistanciaAmbulancia
    {
        public DistanciaAmbulancia(String id, long D, Double lt, Double lg)
        {
            Cedula = id;
            Distancia = D;
            Latitud = lt;
            Longitud = lg;
        }
        public String Cedula { get; set; }
        public long Distancia { get; set; }
        public Double Latitud { get; set; }
        public Double Longitud { get; set; }

    }
    public class LoginParamedicoDto
    {
        public String Cedula { get; set; }
        public String Password { get; set; }
    }
    public class CancelarPedidoDto {
        public String idAmbulancia { get; set; }
        public String RazonCancelar { get; set; }
    }

    public class CalificarPedidoDto
    {
        public String IdPaciente { get; set; }
        public String IdAmbulancia { get; set; }
        public int CalificacionServicio { get; set; }
    }
}