using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Business
{
    public class BookFlightBL : IBookFlightBL
    {

        private readonly IBookFlightRL bookflightRL;

        public BookFlightBL(IBookFlightRL bookflightRL)
        {
            this.bookflightRL = bookflightRL;
        }

        public BookFlightModel BookFlight(BookFlightModel bookflight, string userid)
        {
            try
            {
                return bookflightRL.BookFlight(bookflight, userid);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool CancelFlight(string id)
        {
            try
            {
                return this.bookflightRL.CancelFlight(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
