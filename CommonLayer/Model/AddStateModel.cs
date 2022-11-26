using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.Model
{
    public class AddStateModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string StateID { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string StateImage { get; set; }

        [ForeignKey("User")]
        public string UserID { get; set; }
    }
}
