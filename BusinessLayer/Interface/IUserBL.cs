using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        public UserRegisterModel Register(UserRegisterModel register);

        public string Login(UserLoginModel login);

        public string FogotPassword(string Email);

        public bool ResetLink(string Email, string password, string confirmPassword);
    }
}
