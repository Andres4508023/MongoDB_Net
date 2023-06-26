using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Models
{
    public class Poliza
    {
        public ObjectId Id { get; set; }
        public int NPoliza { get; set; }
        public string IdentificacionCliente { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaCulminacion { get; set; }
        public decimal CoberturaValMax { get; set; }
        public string NombrePoliza { get; set; }
        public string Ciudad { get; set; }
        public string Direccion { get; set; }
        public string Placa { get; set; }
        public int Modelo { get; set; }
        public bool Inspeccion { get; set; }

    }
}
