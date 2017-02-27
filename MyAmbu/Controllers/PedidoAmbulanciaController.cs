using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MyAmbu.Dto;
using System.Net.Http.Headers;
using MyAmbu.Models;
using Google.Apis.Drive;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyAmbu.Controllers
{

    [RoutePrefix("api/PedidoAmbulancia")]
    public class PedidoAmbulanciaController : ApiController
    {
        private static String DIR_URl = "https://maps.googleapis.com/maps/api/directions/json?origin={0},{1}&destination={2},{3}&key={4}";
        private static String KeyGoogle = "AIzaSyBwuI_lYrCitgDzuaGDX7v77ZKH_8u8e2o";
        private MyAmbuEntity db = new MyAmbuEntity();
        HubAlarma Hub = new HubAlarma();


        [ResponseType(typeof(UbicacionPacienteDto))]
        [Route("")]
        [HttpPost]
        public async System.Threading.Tasks.Task<DistanciaAmbulancia> PostUbicacionPaciente(UbicacionPacienteDto UbicacionPaciente)
        {
            List<DistanciaAmbulancia> Distancias = new List<DistanciaAmbulancia>();
            
            List<UbicacionAmbulancias> ListaAmbulancias = null;
            int RangoTiempoBusquedad = 1;
            do
            {
                RangoTiempoBusquedad = RangoTiempoBusquedad * 2;
                DateTime FechaInicio = DateTime.Now.AddHours(-RangoTiempoBusquedad);
                ListaAmbulancias = db.UbicacionAmbulancias.Where(t => t.Fecha >= FechaInicio)
                    .ToList().GroupBy(i => i.Cedula).Select(g => g.Last()).ToList();
            } while (ListaAmbulancias.Count==0);

            foreach (UbicacionAmbulancias UbicacionAmbulancia in ListaAmbulancias)
            {
                using (var client = new HttpClient())
                {

                    String Direccion = string.Format(DIR_URl,
                    UbicacionAmbulancia.Latitud.ToString().Replace(',', '.'),
                    UbicacionAmbulancia.Longitud.ToString().Replace(',', '.'),
                    UbicacionPaciente.Latitud.ToString().Replace(',', '.'),
                    UbicacionPaciente.Longitud.ToString().Replace(',', '.'),
                    KeyGoogle
                    );

                    client.BaseAddress = new Uri(Direccion);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                    HttpResponseMessage response = await client.GetAsync("");
                    if (response.IsSuccessStatusCode)
                    {
                        RootObject Resultado = await response.Content.ReadAsAsync<RootObject>();
                        Distancias.Add(new DistanciaAmbulancia(
                            UbicacionAmbulancia.Cedula, 
                            Resultado.routes[0].legs[0].duration.value,
                            UbicacionAmbulancia.Latitud,
                            UbicacionAmbulancia.Longitud));
                    }
                }
            }

            DistanciaAmbulancia AmbulanciaCercana = Distancias.OrderBy(t => t.Distancia).First();
            try
            {
                Hub.Send(JsonConvert.SerializeObject(UbicacionPaciente) , AmbulanciaCercana.Cedula);
            }
            catch
            {

            }
            


            return AmbulanciaCercana;
        }

        // POST: api/PedidoAmbulancia
        
        [ResponseType(typeof(CancelarPedidoDto))]
        
        [HttpPost]
        [Route("Cancelar")]
        public  String CancelarPedido(CancelarPedidoDto value)
        {
            Hub.Send(value.RazonCancelar, value.idAmbulancia);
            return "Ok";
        }

        // POST: api/PedidoAmbulancia

        [ResponseType(typeof(CalificarPedidoDto))]
       
        [HttpPost]
        [Route("Calificar")]
        public String CalificarPedido(CalificarPedidoDto value)
        {
            return "Ok";
        }



        //// PUT: api/PedidoAmbulancia/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/PedidoAmbulancia/5
        //public void Delete(int id)
        //{
        //}
    }
}
