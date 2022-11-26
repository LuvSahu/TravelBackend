using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IAddStateRL
    {
        public AddStateModel AddState(AddStateModel addstate, string userid);

        public IEnumerable<AddStateModel> GetAllStates();

        public string UploadImage(string id, IFormFile img, string userid);


    }
}
