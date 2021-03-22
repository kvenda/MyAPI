using MongoDB.Bson.Serialization.Attributes;
using System;


namespace MyAPI.Models
{
    public class DataFields
    {
         [BsonId]
        public Guid Id { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public string FullName { get; set; }

        public string MyText { get; set; }

        public int CalculationExample => DateTime.DaysInMonth(LastModifiedDate.Year, LastModifiedDate.Month) + (int)(100);

        public string Summary { get; set; }
    }
}
