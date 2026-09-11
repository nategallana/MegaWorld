using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mega_World_Mall_Linking.Models
{
    public class HourlySalesEntry
    {
        public DateTime Timestamp { get; set; } 
        public decimal NetSalesAmount { get; set; }
        public int TransactionCount { get; set; }
        public int CustomerCount { get; set; }
    }

}
