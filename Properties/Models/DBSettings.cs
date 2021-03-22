using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAPI.Models
{
    public record DBSettings   //using record because the values do not change,  they are loaded from the appsettings file
    {
        public string DBCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

  

}
