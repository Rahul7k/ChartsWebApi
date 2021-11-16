using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace charts.web.api.Models
{
    public class Medals
    {
        public int Rank{get; set;}
        public string Nation{get; set;}
        public int Gold{get; set;}
        public int Silver{get; set;}
        public int Bronze{get; set;}
        public int Total{get; set;}
        public int RankByTotal{get; set;}
    }
}