using System.Collections.Generic;
using System.Web.Http;
using Google.Apis.Sheets.v4;
using Presidents.Models;

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
        /// <returns>JSON list of presidents ordered by sort parameter (default by birthday)</returns>
        /// <remarks>If they have not died yet, they are displayed at the bottom regardless of sort order</remarks>
        [HttpGet]
        public IHttpActionResult Get(string sort = "birthday")
        {
            var presidents= presidentsRepository.GetPresidentsOrdered(sort);
            return Ok(presidents);
        }


        /// <summary>
        ///  Get US President filtered by Name
        /// </summary>
        /// <param name="name">Name of the US President</param>
        /// <returns>JSON List US Presidents filtered by name</returns>
        [HttpGet]
        [Route("api/presidents/{name}")]
        public IHttpActionResult GetByName(string name)
        {
            List<President> presidents = presidentsRepository.GetPresidentsByName(name);
            return Ok(presidents);
        }
    }
}
