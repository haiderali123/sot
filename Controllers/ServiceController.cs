using aghaApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace aghaApi.Controllers
{
    public class ServiceController : ApiController
    {
        finalEntities db = new finalEntities();

        public object GetServices()
        {
            return db.services;
        }

    }
}
