using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IBookFlightBL
    {
        public BookFlightModel BookFlight(BookFlightModel bookflight, string userid);

        public bool CancelFlight(string id);


    }
}
