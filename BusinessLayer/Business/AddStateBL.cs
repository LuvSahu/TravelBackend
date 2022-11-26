using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Business
{
    public class AddStateBL : IAddStateBL

    {
        private readonly IAddStateRL stateRL;
        public AddStateBL(IAddStateRL stateRL)
        {
            this.stateRL = stateRL;
        }

        public AddStateModel AddState(AddStateModel addstate, string userid)
        {
            try
            {
                return stateRL.AddState(addstate,userid);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<AddStateModel> GetallStates()
        {
            try
            {
                return stateRL.GetAllStates();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string UploadImage(string id, IFormFile img, string userid)
        {
            try
            {
                return this.stateRL.UploadImage(id, img, userid);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }

}
