using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.Model
{
    public class BookFlightModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string FlightID { get; set; }
        public string Flyingfrom { get; set; }
        public string Flyingto { get; set; }
        public string Classtype { get; set; }
        public DateTime Date { get; set; }
        public string TotalSeat { get; set; }

        [ForeignKey("State")]
        public string StateID { get; set; }

        [ForeignKey("User")]
        public string UserID { get; set; }

    }
}
