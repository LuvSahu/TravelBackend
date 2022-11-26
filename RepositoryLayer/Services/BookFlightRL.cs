using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class BookFlightRL : IBookFlightRL
    {
        private readonly IMongoCollection<BookFlightModel> Flight;
        private readonly IConfiguration configuration;
        private readonly IConfiguration _AppSetting;


        public BookFlightRL(IConfiguration configuration, IConfiguration _AppSetting, IDBSetting db)
        {
            this.configuration = configuration;
            this._AppSetting = _AppSetting;
            var userlimit = new MongoClient(db.ConnectionString);
            var database = userlimit.GetDatabase(db.DatabaseName);
            Flight = database.GetCollection<BookFlightModel>("Flight");
        }

        public BookFlightModel BookFlight(BookFlightModel bookflight, string userid)
        {
            try
            {
                var ifExists = this.Flight.Find(x => x.FlightID == bookflight.FlightID && x.UserID == userid).SingleOrDefault();
                if (ifExists == null)
                {
                    this.Flight.InsertOne(bookflight);
                    return bookflight;
                }
                return null;

            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool CancelFlight(string id)
        {
            try
            {
                var ifExists = this.Flight.FindOneAndDelete(x => x.FlightID == id);
                return true;

            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

    }

}