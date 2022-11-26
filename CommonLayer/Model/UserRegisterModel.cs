using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class UserRegisterModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserID { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
