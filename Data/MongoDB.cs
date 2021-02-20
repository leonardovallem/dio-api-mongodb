using System;
using ApiMongoDB.Data.Collections;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace ApiMongoDB.Data
{
    public class MongoDB
    {
        public IMongoDatabase DB { get; }

        public MongoDB(IConfiguration configuration)
        {
            try
            {
                var settings = MongoClientSettings.FromConnectionString(configuration["ConnectionString"]);
                var client = new MongoClient(settings);
                DB = client.GetDatabase(configuration["NomeBD"]);
                MapClasses();
            }
            catch (Exception e)
            {
                throw new MongoException("It was not possible to connect to MongoDB", e);
            }
        }

        private void MapClasses()
        {
            var conventionPack = new ConventionPack() { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("camelCase", conventionPack, t => true);

            if (!BsonClassMap.IsClassMapRegistered(typeof(Infectado)))
            {
                BsonClassMap.RegisterClassMap<Infectado>(i =>
                {
                    i.AutoMap();
                    i.SetIgnoreExtraElements(true);
                });
            }
        }
    }
}