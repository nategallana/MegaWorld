using Mega_World_Mall_Linking.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mega_World_Mall_Linking.Services
{
    public class DailySalesGenerator
    {
        public static void Generate(
            DailyDataHeader salesReport,
            List<DailyDataDetails> salesPerTypeList,
            string outputDirectory,
            int batchNumber = 1)
        {
            List<string> lines = new List<string>();

            string tenantCode = salesReport.TenantCode;
            int terminalNumber = Convert.ToInt32(salesReport.POSTerminalNumber);
            DateTime businessDate = salesReport.Date;

            // 1. Header lines
            lines.Add("01" + tenantCode);
            lines.Add("02" + terminalNumber.ToString());
            lines.Add("03" + businessDate.ToString("MMddyyyy"));
            lines.Add("04" + FormatAmount(salesReport.OldAccumulatedTotal));
            lines.Add("05" + FormatAmount(salesReport.NewAccumulatedTotal));
            lines.Add("06" + FormatAmount(salesReport.TotalGrossSalesAmount));
            lines.Add("07" + FormatAmount(salesReport.TotalNonTaxableSalesAmount));
            lines.Add("08" + FormatAmount(salesReport.TotalSeniorCitizenDiscount));
            lines.Add("09" + FormatAmount(salesReport.TotalOtherDiscount));
            lines.Add("10" + FormatAmount(salesReport.TotalRefundAmount));
            lines.Add("11" + FormatAmount(salesReport.TotalTaxAmount));
            lines.Add("12" + FormatAmount(salesReport.TotalServiceCharge));
            lines.Add("13" + FormatAmount(salesReport.TotalServiceCharge));
            lines.Add("14" + FormatAmount(salesReport.TotalCashSales));
            lines.Add("15" + FormatAmount(salesReport.TotalChargeSales)) ;
            lines.Add("16" + FormatAmount(salesReport.TotalGCOtherSales));
            lines.Add("17" + FormatAmount(salesReport.TotalVoidAmount));
            lines.Add("18" + salesReport.TotalCustomerCount.ToString().PadLeft(3, '0'));
            lines.Add("19" + salesReport.ControlNumber);
            lines.Add("20" + salesReport.TotalNumberOfTransactions.ToString());

            // 2. Repeating SalesPerType lines
            foreach (var type in salesPerTypeList)
            {
                string salesType = type.SLS_TYPE.PadLeft(2, '0');
                string netSales = FormatAmount(type.NET_SLS);

                lines.Add("21" + salesType);
                lines.Add("22" + netSales);
            }

            // 3. Generate file name
            string fileName = GenerateFileName(tenantCode, terminalNumber, batchNumber, businessDate);
            string fullPath = Path.Combine(outputDirectory, fileName);

            File.WriteAllLines(fullPath, lines);
            
        }

        private static string FormatAmount(object value)
        {
            decimal amount = Convert.ToDecimal(value);
            int cents = (int)(amount * 100);
            return cents.ToString().PadLeft(8, '0');
        }

        private static string GenerateFileName(string tenantID, int terminalNo, int batchNo, DateTime date)
        {
            string tenantCode = tenantID.Substring(0, 4).ToUpper();
            string terminal = terminalNo.ToString("D2");
            string batch = batchNo.ToString();

            string month = date.Month <= 9
                ? date.Month.ToString()
                : ((char)('A' + (date.Month - 10))).ToString();

            string day = date.Day.ToString("D2");

            return $"S{tenantCode}{terminal}{batch}.{month}{day}";
        }

    }

}
