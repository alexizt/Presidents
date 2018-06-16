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
    /// <summary>
    /// Main Controller
    /// </summary>
    public class PresidentsController : ApiController
    {

        private IPresidentsRepository presidentsRepository;

        static string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };

        /// <summary>
        /// Injected constructor
        /// </summary>
        /// <param name="presidentsRepository">Injected PresidentsRepository</param>
        public PresidentsController(IPresidentsRepository presidentsRepository) {
            this.presidentsRepository = presidentsRepository;
        }

        /// <summary>
        ///  Get US Presidents (allows sorting by birthdate and date of the decease)
        /// </summary>
        /// <param name="sort">Sort parameter: birthday,birthday_desc, deathday, deathday_desc</param>
        /// <returns>List of presidents order by sort</returns>
        /// <remarks>If they have not died yet, they are displayed at the bottom regardless of sort order</remarks>
        [HttpGet]
        public IHttpActionResult Get(string sort = "birthday")
        {

            var presidents= presidentsRepository.getPresidents().Where(x => true);
            

            switch (sort.ToLower())
            {
                case "birthday":
                    presidents = presidents.OrderBy(x => x.DeathDay == null).ThenBy(x => x.BirthDay).ToList();
                    break;
                case "birthday_desc":
                    presidents = presidents.OrderBy(x => x.DeathDay == null).ThenByDescending(x => x.BirthDay).ToList();
                    break;
                case "deathday":
                    presidents = presidents.OrderBy(x => x.DeathDay == null).ThenBy(x => x.DeathDay).ToList();
                    break;
                case "deathday_desc":
                    presidents = presidents.OrderBy(x => x.DeathDay == null).ThenByDescending(x => x.DeathDay).ToList();
                    break;
            }
            return Ok(presidents);
        }


        /// <summary>
        ///  Get US President filtered by Name
        /// </summary>
        /// <param name="name">Name of the US President</param>
        /// <returns>List US Presidents filtered by name</returns>
        [HttpGet]
        [Route("api/presidents/{name}")]
        public IHttpActionResult GetByName(string name)
        {
            List<President> presidents = presidentsRepository.getPresidents().Where(x => x.PresidentName == name || name == "").OrderBy(x => x.PresidentName).ToList();
            return Ok(presidents);
        }
    }
}
