using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presidents.Models
{
    /// <summary>
    /// Presidents Repository Interface
    /// </summary>
    
    public interface IPresidentsRepository
    {
        /// <summary>
        /// Get List of Presidents
        /// </summary>
        List<President> GetPresidents();

        /// <summary>
        /// Get List of Presidents Ordered
        /// </summary>
        List<President> GetPresidentsOrdered(string sort = "birthday");

        /// <summary>
        ///  Get US President filtered by Name
        /// </summary>
        List<President> GetPresidentsByName(string name);
    }
}