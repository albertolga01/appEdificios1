using System;
using System.Collections.Generic;
using System.Text;

namespace AppEdificiosP.Models
{
    internal class MToken
    {
        public string id { get; set; } 
        public card card { get; set; }
    }

    public class card
    {
        public string holder_name { get; set; }
        public string card_number { get; set; }
    }
}
