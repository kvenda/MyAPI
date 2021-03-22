using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MyAPI.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;


namespace MyAPI.Services
{
    public class APIService
    {
        private readonly ILogger<APIService> _logger;
        private readonly IConfiguration _config;
        private readonly IMongoCollection<DataFields> _data;

        public APIService(ILogger<APIService> logger, IConfiguration config)
        {

            _logger = logger;
            _config = config;

            DBSettings settings = _config.GetSection("DBSettings").Get<DBSettings>();

            var client = new MongoClient(settings.ConnectionString);
                var database = client.GetDatabase(settings.DatabaseName);
                _data = database.GetCollection<DataFields>(settings.DBCollectionName);
        }

        public List<DataFields> Get()
        {
            _logger.LogInformation("In API Service Get() - to get all records");
            return _data.Find(data => true).ToList();
        }

        public DataFields Get(string id)
        {
            _logger.LogInformation("In API Service Get()- get one by GUID");
            return _data.Find(data => data.Id == new Guid(id)).FirstOrDefault();
        }

        public DataFields GetField(string parameter)
        {
            _logger.LogInformation("In API Service Get()- get one by parameter value");
            return _data.Find(data => data.FullName.Contains(parameter)).FirstOrDefault();
        }


        public void Create(DataFields newdata)
        {
            _data.InsertOne(newdata);
        }

        public void Update(Guid id, DataFields updatedData) =>
                _data.ReplaceOne(data => data.Id == id, updatedData);

        public void Remove(string id) =>
               _data.DeleteOne(data => data.Id == new Guid(id));
        

    }
}

