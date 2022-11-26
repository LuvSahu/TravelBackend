using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class AddStateRL : IAddStateRL
    {
        private readonly IMongoCollection<AddStateModel> State;
        private readonly IConfiguration configuration;
        private readonly IConfiguration _AppSetting;
        private readonly IConfiguration cloudinaryEntity;


        public AddStateRL(IConfiguration configuration, IConfiguration _AppSetting, IDBSetting db, IConfiguration cloudinaryEntity)
        {
            this.configuration = configuration;
            this._AppSetting = _AppSetting;
            var userlimit = new MongoClient(db.ConnectionString);
            var database = userlimit.GetDatabase(db.DatabaseName);
            State = database.GetCollection<AddStateModel>("States");
            this.cloudinaryEntity = cloudinaryEntity;
        }

        public AddStateModel AddState(AddStateModel addstate, string userid)
        {
            try
            {
                var ifExists = this.State.Find(x => x.StateID == addstate.StateID && x.UserID == userid).SingleOrDefault();
                if (ifExists == null)
                {
                    this.State.InsertOne(addstate);
                    return addstate;
                }
                return null;

            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<AddStateModel> GetAllStates()
        {
            return State.Find(FilterDefinition<AddStateModel>.Empty).ToList();
        }

        public string UploadImage(string id, IFormFile img, string userid)
        {
            try
            {
                var result = this.State.Find(x => x.StateID == id && x.UserID == userid).SingleOrDefault();
                if (result != null)
                {
                    Account cloudaccount = new Account(
                         cloudinaryEntity["CloudinarySettings:cloudName"],
                         cloudinaryEntity["CloudinarySettings:apiKey"],
                         cloudinaryEntity["CloudinarySettings:apiSecret"]
                         );

                    Cloudinary cloudinary = new Cloudinary(cloudaccount);
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(img.FileName, img.OpenReadStream()),
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);
                    string imagePath = uploadResult.Url.ToString();
                    result.StateImage = imagePath;
                    this.State.UpdateOne(x => x.StateID == id, Builders<AddStateModel>.Update.Set(x => x.StateImage, imagePath));
                    return "Image upload SuccessFully";
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
