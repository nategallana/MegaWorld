using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mega_World_Mall_Linking.Models
{
    public class DailyDataHeader
    {
        public string TenantCode { get; set; }
        public string POSTerminalNumber { get; set; }
        public DateTime Date { get; set; }
        public string TransactionID { get; set; }
        public decimal OldAccumulatedTotal { get; set; }
        public decimal NewAccumulatedTotal { get; set; }
        public decimal TotalGrossSalesAmount { get; set; }
        public decimal TotalNonTaxableSalesAmount { get; set; }
        public decimal TotalSeniorCitizenDiscount { get; set; }
        public decimal TotalOtherDiscount { get; set; }
        public decimal TotalRefundAmount { get; set; }
        public decimal TotalTaxAmount { get; set; }
        public decimal TotalServiceCharge { get; set; }
        public decimal TotalNetSalesAmount { get; set; }
        public decimal TotalCashSales { get; set; }
        public decimal TotalChargeSales { get; set; }
        public decimal TotalGCOtherSales { get; set; }
        public decimal TotalVoidAmount { get; set; }
        public int TotalCustomerCount { get; set; }
        public int ControlNumber { get; set; }
        public int TotalNumberOfTransactions { get; set; }
    }
   
}
