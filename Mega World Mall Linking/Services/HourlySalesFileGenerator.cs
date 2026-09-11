using Mega_World_Mall_Linking.Helpers;
using Mega_World_Mall_Linking.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mega_World_Mall_Linking.Services
{
    public class HourlySalesFileGenerator
    {
        public static void GenerateFile(HourlySalesFile data, string outputDirectory)
        {
            var lines = new List<string>();

            // Fields 01–03
            lines.Add("01" + data.TenantCode.PadRight(8, ' '));
            lines.Add("02" + data.POSTerminalNumber.ToString());
            lines.Add("03" + data.BusinessDate);

            // Fields 04–07: Hourly Sales (auto-detect hour code)
            foreach (var entry in data.HourlyEntries)
            {
                string hourCode = HourCodeHelper.GetHourCode(entry.Timestamp);
                lines.Add("04" + hourCode);

                int cents = (int)(entry.NetSalesAmount * 100);
                lines.Add("05" + cents.ToString());
                lines.Add("06" + entry.TransactionCount.ToString());
                lines.Add("07" + entry.CustomerCount.ToString());
            }

            // Fields 08–10: Daily totals
            int totalCents = (int)(data.TotalNetSalesAmount * 100);
            lines.Add("08" + totalCents.ToString());
            lines.Add("09" + data.TotalTransactionCount.ToString());
            lines.Add("10" + data.TotalCustomerCount.ToString());

            //DateTime date = DateTime.Parse(data.BusinessDate
            //DateTime date = Convert.ToDateTime(data.BusinessDate);
            string fileName = GenerateFileName(data.TenantCode, data.POSTerminalNumber,data.DateNoFormat);
            string fullPath = Path.Combine(outputDirectory, fileName);

            File.WriteAllLines(fullPath, lines, Encoding.UTF8);
            Console.WriteLine("✅ File generated: " + fullPath);
        }
        //public static void GenerateFile(HourlySalesFile data, string outputDirectory)
        //{
        //    List<string> lines = new List<string>();

        //    // Add headers
        //    lines.Add("01" + data.TenantCode.PadLeft(4, '0'));
        //    lines.Add("02" + data.POSTerminalNumber.ToString("D2"));
        //    lines.Add("03" + data.BusinessDate.ToString("yyyyMMdd"));

        //    // Determine dynamic time range from the entries
        //    if (data.HourlyEntries == null || !data.HourlyEntries.Any())
        //        return; // Exit if no entries exist

        //    DateTime earliestHour = data.HourlyEntries.Min(e => e.Timestamp).Date.AddHours(data.HourlyEntries.Min(e => e.Timestamp).Hour);
        //    DateTime latestHour = data.HourlyEntries.Max(e => e.Timestamp).Date.AddHours(data.HourlyEntries.Max(e => e.Timestamp).Hour + 1); // Include the final hour

        //    for (DateTime currentHour = earliestHour; currentHour < latestHour; currentHour = currentHour.AddHours(1))
        //    {
        //        var entry = data.HourlyEntries.FirstOrDefault(e =>
        //            e.Timestamp >= currentHour && e.Timestamp < currentHour.AddHours(1));

        //        // Skip if no entry for this hour
        //        if (entry == null)
        //            continue;

        //        string hourCode = HourCodeHelper.GetHourCode(currentHour);

        //        lines.Add("04" + hourCode);
        //        lines.Add("05" + ((int)(entry.NetSalesAmount * 100)).ToString().PadLeft(8, '0'));
        //        lines.Add("06" + entry.TransactionCount.ToString("D2"));
        //        lines.Add("07" + entry.CustomerCount.ToString("D2"));
        //    }

        //    // File path using your custom file naming logic
        //    string fileName = GenerateFileName(data.TenantCode, data.POSTerminalNumber, data.BusinessDate);
        //    string filePath = Path.Combine(outputDirectory, fileName);
        //    File.WriteAllLines(filePath, lines);
        //}

        private static string GenerateFileName(string tenantCode, int terminal, DateTime date)
        {
            string safeTenantCode = (tenantCode ?? "").PadRight(4, '0').Substring(0, 4).ToUpper();
            string monthCode = date.Month <= 9 ? date.Month.ToString() : ((char)('A' + (date.Month - 10))).ToString();
            string dayCode = date.Day.ToString("D2");
            return $"H{safeTenantCode}{terminal.ToString("D2")}1.{monthCode}{dayCode}";
        }

    }
}
