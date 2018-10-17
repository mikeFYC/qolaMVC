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
    public class BirthdayCalendarController : ApiController
    {
        // GET api/<controller>
        public Collection<ActivityEventModel> Get(int HomeId)
        {
            Collection<ActivityEventModel> l_ActivityEvents = new Collection<ActivityEventModel>();
            l_ActivityEvents = HomeDAL.GetBirthdayCalendar(HomeId);
            return l_ActivityEvents;
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}