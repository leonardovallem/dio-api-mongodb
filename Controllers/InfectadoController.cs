using ApiMongoDB.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ApiMongoDB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfectadoController : ControllerBase
    {
        private Data.MongoDB _mongoDb;
        private IMongoCollection<Data.Collections.Infectado> _infectadosCollection;

        public InfectadoController(Data.MongoDB mongoDb)
        {
            _mongoDb = mongoDb;
            _infectadosCollection = _mongoDb.DB.GetCollection<Data.Collections.Infectado>(typeof(Data.Collections.Infectado).Name.ToLower());
        }

        [HttpPost]
        public ActionResult SalvarInfectado([FromBody] Infectado dto)
        {
            var infectado = new Data.Collections.Infectado(dto.DataNascimento, dto.Sexo, dto.Latitude, dto.Longitude);
            _infectadosCollection.InsertOne(infectado);
            return StatusCode(201, "Infectado cadastrado com sucesso");
        }

        [HttpGet]
        public ActionResult ObterInfectado()
        {
            var infectados = _infectadosCollection.Find(new BsonDocument()).ToList();
            return Ok(infectados);
        }
    }
}