using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Business
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL userRL;

        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        public UserRegisterModel Register(UserRegisterModel register)
        {
            try
            {
                return userRL.Register(register);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string Login(UserLoginModel login)
        {
            try
            {
                return userRL.Login(login);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public string FogotPassword(string Email)
        {
            try
            {
                return userRL.FogotPassword(Email);


            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ResetLink(string Email, string password, string confirmPassword)
        {
            try
            {
                return userRL.ResetLink(Email, password, confirmPassword);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
