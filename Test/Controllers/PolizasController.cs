using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using Test.Models;
using MongoDB.Bson;
using Test.Library;
using Microsoft.AspNetCore.Authorization;
using Test.Repository;

namespace Test.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PolizasController : ControllerBase
    {
        private readonly IMongoDatabase _database;
        private readonly IJWTManagerRepository jWTManagerRepository;
        public PolizasController(IJWTManagerRepository jWTManagerRepository, IMongoDatabase database)
        {
            this.jWTManagerRepository = jWTManagerRepository;
            _database = database;
            
        }

        [HttpGet]
        [Route("GetPoliza")]
        public IActionResult GetPoliza()
        {
            var collection = _database.GetCollection<Poliza>("Polizas");
            var polizas = collection.Find(p => true).ToList();
            return Ok(polizas);
        }

        [HttpGet]
        public IActionResult GetPolizaPlaca(string placa, int NPoliza)
        {
            var collection = _database.GetCollection<Poliza>("Polizas");

            if (placa != null)
            {
                var filterPlaca = collection.Find(p => p.Placa == placa).FirstOrDefault();
                return Ok(filterPlaca);
            }

            if (NPoliza != 0)
            {
                var filterPoliza = collection.Find(p => p.NPoliza == NPoliza).FirstOrDefault();
                return Ok(filterPoliza);
            }
            else
            {
                return BadRequest("No es posible traer la informacion ya que no hay placa y Npoliza");
            }

        }

        [HttpPost]
        public IActionResult PostPoliza(Poliza poliza)
        {
            
            var Fecha = new Library.Fecha();
            poliza.FechaCulminacion = Fecha.GetDateOneYearLater(poliza.FechaIngreso);
            if (poliza.FechaCulminacion > DateTime.Now)
            {
                var collection = _database.GetCollection<Poliza>("Polizas");
                collection.InsertOne(poliza);
                return Ok(poliza);
            }
            else
            {
                return BadRequest("No es posible crear la poliza, por que no cumple con las condiciones");
            }
                        
        }

        [HttpPut("{Placa}")]
        public IActionResult PutPoliza(string Placa, Poliza updatedPoliza)
        {
            var collection = _database.GetCollection<Poliza>("Polizas");
            var filter = Builders<Poliza>.Filter.Eq(p => p.Placa, Placa);
            var result = collection.ReplaceOne(filter, updatedPoliza);

            if (result.ModifiedCount == 0)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{Placa}")]
        public IActionResult DeletePoliza(string Placa)
        {
            var collection = _database.GetCollection<Poliza>("Polizas");
            var filter = Builders<Poliza>.Filter.Eq(p => p.Placa, Placa);
            var result = collection.DeleteOne(filter);

            if (result.DeletedCount == 0)
                return NotFound();

            return NoContent();
        }

    }
}
