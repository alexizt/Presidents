using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Threading;
using System.ComponentModel;
using Presidents.Models;
using Presidents.Models.Repositories;

namespace Presidents.Controllers
{
    public class PresidentsController : ApiController
    {

        private IPresidentsRepository presidentsRepository;

        static string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        //static string ApplicationName = "Presidents of US";

        public PresidentsController(IPresidentsRepository presidentsRepository) {
            this.presidentsRepository = presidentsRepository;
        }


        /// <summary>
        ///  GET api/presidents/
        /// </summary>
        /// <param name="sort">Sort field: birthday,birthday_desc, deathday, deathday_desc</param>
        /// <returns>List of presidents order by <paramref name="sort"/></returns>
        public IHttpActionResult Get(string sort = "birthday")
        {

            var presidents= presidentsRepository.getPresidents().Where(x => true);
            

            switch (sort.ToLower())
            {
                case "birthday":
                    presidents = presidents.OrderBy(x => x.BirthDay).ToList();
                    break;
                case "birthday_desc":
                    presidents = presidents.OrderByDescending(x => x.BirthDay).ToList();
                    break;
                case "deathday":
                    presidents = presidents.OrderBy(x => x.DeathDay).ToList();
                    break;
                case "deathday_desc":
                    presidents = presidents.OrderByDescending(x => x.DeathDay).ToList();
                    break;
            }
            return Ok(presidents);
        }


        /// <summary>
        ///  GET api/presidents/name
        /// </summary>
        /// <param name="name">Name of the US President</param>
        /// <returns>List US Presidents filtered by <paramref name="name"/></returns>
        [Route("api/presidents/{name}")]
        public IHttpActionResult GetByName(string name)
        {
            List<President> presidents = presidentsRepository.getPresidents().Where(x => x.PresidentName == name || name == "").OrderBy(x => x.PresidentName).ToList();
            return Ok(presidents);
        }


 

            
            
    }
}
