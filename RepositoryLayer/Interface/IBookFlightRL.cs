using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IBookFlightRL
    {
        public BookFlightModel BookFlight(BookFlightModel bookflight, string userid);

        public bool CancelFlight(string id);


    }
}
