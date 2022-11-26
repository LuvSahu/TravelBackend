using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IAddStateBL
    {
        public AddStateModel AddState(AddStateModel addstate,string userid);

        public IEnumerable<AddStateModel> GetallStates();

        public string UploadImage(string id, IFormFile img, string userid);

    }
}
