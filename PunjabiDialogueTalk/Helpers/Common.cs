using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;

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


       public static System.Drawing.Image ScaleImage(System.Drawing.Image image, int maxImageHeight)
       {

           /* we will resize image based on the height/width ratio by passing 
            * expected height as parameter. Based on Expected height and current image 
            * height, new ratio will be arrived and using the same we will do the 
            * resizing of image width. */

           var ratio = (double)maxImageHeight / image.Height;
           var newWidth = (int)(image.Width * ratio);
           var newHeight = (int)(image.Height * ratio);
           var newImage = new Bitmap(newWidth, newHeight);
           using (var g = Graphics.FromImage(newImage))
           {
               g.DrawImage(image, 0, 0, newWidth, newHeight);
           }
           return newImage;
       }
    }

}

