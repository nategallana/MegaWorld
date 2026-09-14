using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mega_World_Mall_Linking.Models
{
    public class HourlySalesFile
    {
        // Field 01 - Tenant Code (8-character code)
        public string TenantCode { get; set; }

        // Field 02 - POS Terminal Number (1 to 4-digit number)
        public int POSTerminalNumber { get; set; }

        // Field 03 - Transaction Date
        public string BusinessDate { get; set; }

        public DateTime DateNoFormat { get; set; }

        public int BatchNumber { get; set; } = 1;

        // Fields 04–07 (repeating) - Hourly Sales Entries
        public List<HourlySalesEntry> HourlyEntries { get; set; } = new List<HourlySalesEntry>();

        // Field 08 - Total Net Sales Amount for the Day
        public decimal TotalNetSalesAmount { get; set; }

        // Field 09 - Total Number of Sales Transactions for the Day
        public int TotalTransactionCount { get; set; }

        // Field 10 - Total Customer Count for the Day
        public int TotalCustomerCount { get; set; }
    }
}
