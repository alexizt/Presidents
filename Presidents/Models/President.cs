using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presidents.Models
{

    /// <summary>
    /// A President Class
    /// </summary>
    public class President
    {
        public string PresidentName { get; set; }
        public string BirthDay { get; set; }
        public string BirthPlace { get; set; }
        public string DeathDay { get; set; }
        public string DeathPlace { get; set; }
    }
}