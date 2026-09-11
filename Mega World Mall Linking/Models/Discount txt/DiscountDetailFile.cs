using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mega_World_Mall_Linking.Models
{
    public class DiscountDetailFile
    {
        public string TenantCode { get; set; }           // First 4 chars only will be used
        public int POSTerminalNumber { get; set; }
        public int BatchNumber { get; set; }
        public DateTime BusinessDate { get; set; }

        public List<DiscountDetailEntry> Entries { get; set; } = new List<DiscountDetailEntry>();
    }
}
