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
        public Collection<ActivityEventModel> Get(int homeid)
        {
            Collection<ActivityEventModel> l_ActivityEvents = new Collection<ActivityEventModel>();
            l_ActivityEvents = HomeDAL.GetActivityEvents(homeid);


            return l_ActivityEvents;
        }

        public Collection<ActivityEventModel> Get_C2(int homeid)
        {
            Collection<ActivityEventModel> l_ActivityEvents = new Collection<ActivityEventModel>();
            l_ActivityEvents = HomeDAL.GetActivityEvents_C2(homeid);
            return l_ActivityEvents;
        }

        public Collection<ActivityEventModel> Get_C3(int homeid)
        {
            Collection<ActivityEventModel> l_ActivityEvents = new Collection<ActivityEventModel>();
            l_ActivityEvents = HomeDAL.GetActivityEvents_C3(homeid);
            return l_ActivityEvents;
        }

        public Collection<ActivityEventModel> Get_C4(int homeid)
        {
            Collection<ActivityEventModel> l_ActivityEvents = new Collection<ActivityEventModel>();
            l_ActivityEvents = HomeDAL.GetActivityEvents_C4(homeid);
            return l_ActivityEvents;
        }
        // GET: api/ActivityCalendar/5
        public string Get()
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
