using System;
using System.Collections.Generic;
using System.Linq;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;


namespace Presidents.Models
{

    namespace Repositories
    {
        /// <summary>
        /// Presidents Repository Interface
        /// </summary>
        public interface IPresidentsRepository
        {
            List<President> getPresidents();
        }

        /// <summary>
        /// The Presidents repository
        /// </summary>
        public class PresidentsRepository : IPresidentsRepository {

            /// <summary>
            /// Get List of Presidents
            /// </summary>
            /// <returns></returns>
            public List<President> getPresidents()
            {

                var service = new SheetsService(new BaseClientService.Initializer
                {
                    ApplicationName = System.Configuration.ConfigurationManager.AppSettings["SheetsService.ApplicationName"],
                    ApiKey = System.Configuration.ConfigurationManager.AppSettings["SheetsService.ApiKey"]
                });

                // Define request parameters.
                String spreadsheetId = "1i2qbKeasPptIrY1PkFVjbHSrLtKEPIIwES6m2l2Mdd8";
                String range = "A2:E";
                SpreadsheetsResource.ValuesResource.GetRequest request =
                        service.Spreadsheets.Values.Get(spreadsheetId, range);

                // Prints the names and majors of students in a sample spreadsheet:
                // https://docs.google.com/spreadsheets/d/1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms/edit
                ValueRange response = request.Execute();
                IList<IList<Object>> values = response.Values;

                List<President> presidents = new List<President>();
                if (values != null && values.Count > 0)
                {
                    Console.WriteLine("Name, Major");
                    foreach (var row in values)
                    {
                        switch (row.Count)
                        {
                            case 5:
                                presidents.Add(new President() { PresidentName = row[0].ToString(), BirthDay = row[1].ToString(), BirthPlace = row[2].ToString(), DeathDay = row[3].ToString(), DeathPlace = row[4].ToString() });
                                break;
                            case 3:
                                presidents.Add(new President() { PresidentName = row[0].ToString(), BirthDay = row[1].ToString(), BirthPlace = row[2].ToString() });
                                break;
                            default:
                                break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No data found.");
                }
                return presidents.ToList();
            }

        }

    }
}