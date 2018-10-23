using QolaMVC.DAL;
using QolaMVC.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QolaMVC.WebAPI
{
    public class ActivityCalendarController : ApiController
    {
        // GET: api/ActivityCalendar
        public Collection<ActivityEventModel> Get()
        {
            Collection<ActivityEventModel> l_ActivityEvents = new Collection<ActivityEventModel>();
            l_ActivityEvents = HomeDAL.GetActivityEvents();


            return l_ActivityEvents;
        }

        public Collection<ActivityEventModel> Get_C2()
        {
            Collection<ActivityEventModel> l_ActivityEvents = new Collection<ActivityEventModel>();
            l_ActivityEvents = HomeDAL.GetActivityEvents_C2();
            return l_ActivityEvents;
        }

        public Collection<ActivityEventModel> Get_C3()
        {
            Collection<ActivityEventModel> l_ActivityEvents = new Collection<ActivityEventModel>();
            l_ActivityEvents = HomeDAL.GetActivityEvents_C3();
            return l_ActivityEvents;
        }

        public Collection<ActivityEventModel> Get_C4()
        {
            Collection<ActivityEventModel> l_ActivityEvents = new Collection<ActivityEventModel>();
            l_ActivityEvents = HomeDAL.GetActivityEvents_C4();
            return l_ActivityEvents;
        }
        // GET: api/ActivityCalendar/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ActivityCalendar
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ActivityCalendar/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ActivityCalendar/5
        public void Delete(int id)
        {
        }
    }
}
