using System;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using Xunit;
using Test.Models;
using Test.Controllers;
using Mongo2Go;
using System.Collections.Generic;
using System.Linq;
using Moq;

namespace TestUnit
{
  public class PolizasTestController
    {
        
            private PolizasController _controller;
            private IMongoDatabase _database;

     
        public  PolizasTestController()
            {
                // Crear un objeto Mock de IMongoDatabase
                var databaseMock = new Mock<IMongoDatabase>();
                _database = databaseMock.Object;

                _controller = new PolizasController(_database);
            }

        [Fact]
        public void GetPoliza_ReturnsOkResultWithPolizas()
        {
            var collection = _database.GetCollection<Poliza>("Polizas");
            collection.InsertMany(new[]
            {
                    new Poliza { Id = ObjectId.GenerateNewId(),
                                 NPoliza = 13,
                                 IdentificacionCliente = "1234",
                                 FechaIngreso = Convert.ToDateTime("2023-06-23"),
                                 FechaCulminacion = Convert.ToDateTime("2024-06-23"),
                                 CoberturaValMax = 1000000,
                                 NombrePoliza = "Seguro Vehiculo",
                                 Ciudad = "Bogota",
                                 Direccion = "Calle 1f #73a 39",
                                 Placa = "VFB-661",
                                 Modelo = 2019,
                                 Inspeccion = true
                    },
                });
            var controller = new PolizasController(_database);
            // Act
            var result = _controller.GetPoliza();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var Polizas = Assert.IsAssignableFrom<IEnumerable<Poliza>>(okResult.Value);
            Assert.Equal(2, Polizas.Count());
        }
        // Resto de los casos de prueba...


        //*************************************************************

        //private PolizasController _controller;
        //private readonly IMongoDatabase _database;

        //private IMongoDatabase CreateInMemoryDatabase()
        //{
        //    var runner = MongoDbRunner.Start();
        //    var client = new MongoClient(runner.ConnectionString);
        //    var database = client.GetDatabase("Test");
        //    return database;
        //}
        //public PolizasTestController(IMongoDatabase database)
        //{
        //    _database = CreateInMemoryDatabase();
        //    _controller = new PolizasController(_database);
        //}

        //[Fact]
        //public void GetPoliza_ReturnsOkResultWithPolizas()
        //{

        //    // Arrange
        //    var collection = _database.GetCollection<Poliza>("Polizas");
        //    collection.InsertMany(new[]
        //    { 
        //        new Poliza { Id = ObjectId.GenerateNewId(),
        //                     NPoliza = 13,
        //                     IdentificacionCliente = "1234",
        //                     FechaIngreso = Convert.ToDateTime("2023-06-23"),
        //                     FechaCulminacion = Convert.ToDateTime("2024-06-23"),
        //                     CoberturaValMax = 1000000,
        //                     NombrePoliza = "Seguro Vehiculo",
        //                     Ciudad = "Bogota",
        //                     Direccion = "Calle 1f #73a 39",
        //                     Placa = "VFB-661",
        //                     Modelo = 2019,
        //                     Inspeccion = true
        //        },
        //    });
        //    var controller = new PolizasController(_database);
        //    // Act
        //    var result = _controller.GetPoliza();

        //    // Assert
        //    var okResult = Assert.IsType<OkObjectResult>(result);
        //    var Polizas = Assert.IsAssignableFrom<IEnumerable<Poliza>>(okResult.Value);
        //    Assert.Equal(2, Polizas.Count());
        //}
        // ***********************************************************************
        //var collection = _database.GetCollection<Poliza>("Polizas");
        //var expectedPolizas = new[] { new Poliza(), new Poliza() };
        //collection.InsertMany(expectedPolizas);

        //    // Act
        //    var result = _controller.GetPoliza();

        //// Assert
        //Assert.IsType<OkObjectResult>(result);
        //    var okResult = (OkObjectResult)result;
        //Assert.Equal(expectedPolizas, okResult.Value);

    }




}
