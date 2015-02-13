using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PunjabiDialogueTalk.Helpers
{
    public class Common
    {
       static public DateTime convertUTCToEST(DateTime timeUtc)
        {
            TimeZoneInfo estZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime estTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, estZone);
            return estTime;
        }
        
    }

}

