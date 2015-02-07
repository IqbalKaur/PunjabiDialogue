using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PunjabiDialogueTalk.Models
{
    public class Dialogue
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}