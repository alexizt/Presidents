using System;
using System.Collections.Generic;
using System.Linq;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;

namespace Presidents.Models
{

    /// <summary>
    /// The Presidents repository
    /// </summary>
    public class PresidentsRepository : IPresidentsRepository
    {

        private SheetsService _SheetsService;
        private const string spreadsheetId = "1i2qbKeasPptIrY1PkFVjbHSrLtKEPIIwES6m2l2Mdd8";
        private const string range = "A2:E";

        public PresidentsRepository()
        {
            /// Instance the SheetService 
            var service = new SheetsService(new BaseClientService.Initializer
            {
                ApplicationName = System.Configuration.ConfigurationManager.AppSettings["SheetsService.ApplicationName"],
                ApiKey = System.Configuration.ConfigurationManager.AppSettings["SheetsService.ApiKey"]
            });
            _SheetsService = service;
        }

        /// <summary>
        /// Gets presidents from spreadsheet and maps it to a List of Presidents
        /// </summary>
        private List<President> mapPresidents()
        {
            // Define request parameters.
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    _SheetsService.Spreadsheets.Values.Get(spreadsheetId, range);

            // Execute spreadsheet requet
            ValueRange response = request.Execute();
            IList<IList<Object>> values = response.Values;


            // Map to list of Presidents
            List<President> presidents = values.Select(
            x => new President()
            {
                PresidentName = x[0].ToString(),
                BirthDay = (DateTime?)Convert.ToDateTime(x[1]),
                BirthPlace = x[2].ToString(),
                DeathDay = x.Count > 3 ? (DateTime?)Convert.ToDateTime(x[3]) : null,
                DeathPlace = x.Count > 4 ? x[4].ToString() : null,
            }).ToList();
            return presidents;
        }

        /// <summary>
        /// Get List of Presidents
        /// </summary>
        /// <returns></returns>
        public List<President> GetPresidents()
        {
            List<President> presidents = mapPresidents();

            return presidents;
        }

        /// <summary>
        /// Get List of Presidents Ordered
        /// </summary>
        /// <param name="sort">Sort parameter: birthday,birthday_desc, deathday, deathday_desc</param>
        /// <returns>List of presidents order by sort</returns>
        /// <remarks>If they have not died yet, they are displayed at the bottom regardless of sort order</remarks>
        public List<President> GetPresidentsOrdered(string sort = "birthday")
        {
            List<President> presidents = this.mapPresidents();


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
            return presidents;
        }

        /// <summary>
        ///  Get US President filtered by Name
        /// </summary>
        /// <param name="name">Name of the US President</param>
        /// <returns>List US Presidents filtered by name</returns>
        public List<President> GetPresidentsByName(string name)
        {
            return this.mapPresidents().Where(x => x.PresidentName == name || name == "").OrderBy(x => x.PresidentName).ToList();
        }
    }

}