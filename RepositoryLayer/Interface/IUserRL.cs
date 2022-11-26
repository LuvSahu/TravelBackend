using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        public UserRegisterModel Register(UserRegisterModel register);

        public string Login(UserLoginModel userLogin);

        public string FogotPassword(string Email);

        public bool ResetLink(string Email, string password, string confirmPassword);
    }
}
