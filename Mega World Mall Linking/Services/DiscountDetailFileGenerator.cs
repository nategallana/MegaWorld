using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mega_World_Mall_Linking.Models;

namespace Mega_World_Mall_Linking.Services
{
    public static class DiscountDetailFileGenerator
    {
        public static void GenerateFile(DiscountDetailFile data, string outputDirectory)
        {
            var lines = new List<string>();

            foreach (var entry in data.Entries)
            {
                string discCode = (entry.DiscountCode ?? string.Empty).PadRight(6, ' ').Substring(0, 6);
                string discDesc = (entry.DiscountDescription ?? string.Empty).PadRight(25, ' ').Substring(0, 25);
                string discAmt = ((int)(entry.DiscountAmount * 100)).ToString().PadLeft(8, '0');

                string line = discCode + discDesc + discAmt;
                lines.Add(line);
            }

            string fileName = GenerateFileName(data.TenantCode, data.POSTerminalNumber, data.BatchNumber, data.BusinessDate);
            string fullPath = Path.Combine(outputDirectory, fileName); // No .txt

            File.WriteAllLines(fullPath, lines, Encoding.UTF8);
            Console.WriteLine($"✅ Discount file created: {fullPath}");
        }

        private static string GenerateFileName(string tenantCode, int terminalNumber, int batchNumber, DateTime date)
        {
            string safeTenantCode = (tenantCode ?? "").PadRight(4, '0').Substring(0, 4).ToUpper();
            string monthCode = date.Month <= 9
                ? date.Month.ToString()
                : ((char)('A' + (date.Month - 10))).ToString(); // A–C for Oct–Dec

            string day = date.Day.ToString("D2");
            return $"D{safeTenantCode}{terminalNumber.ToString("D2")}{batchNumber}.{monthCode}{day}";
        }
    }
}
