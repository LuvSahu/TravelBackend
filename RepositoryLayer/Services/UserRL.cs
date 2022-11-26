using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        private readonly IMongoCollection<UserRegisterModel> User;

        private readonly IConfiguration configuration;


        //private readonly FundoContext fundoContext;

        public UserRL(IDBSetting db, IConfiguration configuration)
        {
            this.configuration = configuration;
            var userlimit = new MongoClient(db.ConnectionString);
            var database = userlimit.GetDatabase(db.DatabaseName);
            User = database.GetCollection<UserRegisterModel>("User");

        }


        public UserRegisterModel Register(UserRegisterModel register)
        {
            try
            {
                var check = this.User.AsQueryable().Where(x => x.Email == register.Email).SingleOrDefault();
                if (check == null)
                {
                    this.User.InsertOne(register);
                    return register;

                }
                return null;
            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

        public string Login(UserLoginModel userLogin)
        {
            try
            {
                var check = this.User.AsQueryable().Where(x => x.Email == userLogin.Email).SingleOrDefault();
                if (check != null)
                {
                    check = this.User.AsQueryable().Where(x => x.Password == userLogin.Password).SingleOrDefault();
                    if (check != null)
                    {

                        var token = GenerateSecurityToken(check.Email, check.UserID);
                        return token;

                    }
                    return null;
                }

                return null;
            }

            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

        public string GenerateSecurityToken(string email, string UserId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.configuration[("JWT:key")]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("UserId", UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }

        public string FogotPassword(string Email)
        {
            try
            {
                var EmailCheck = this.User.AsQueryable().Where(x => x.Email == Email).SingleOrDefault();
                if (EmailCheck != null)
                {

                    string token = GenerateSecurityToken(EmailCheck.Email, EmailCheck.UserID);
                    MSMQModel mSMQModel = new MSMQModel();
                    mSMQModel.sendData2Queue(token);
                    return "Mail sent";
                }
                else
                {
                    return null;
                }
            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool ResetLink(string Email, string password, string confirmPassword)
        {
            try
            {
                if (password.Equals(confirmPassword))
                {
                    var EmailCheck = this.User.Find(x => x.Email == Email).SingleOrDefault();
                   //return true;
                    if (EmailCheck != null)
                    {
                        EmailCheck.Password = password;
                        this.User.UpdateOne(x => x.Email == Email, Builders<UserRegisterModel>.Update.Set(x => x.Password, password));
                        return true;
                    }
                    return true;

                }
                else
                {
                    return false;
                }

            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }

        }

    }
}
