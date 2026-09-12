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
                string discCode = (entry.DiscountCode ?? string.Empty).Trim();
                if (discCode.Length > 6)
                    discCode = discCode.Substring(0, 6);

                string discDesc = (entry.DiscountDescription ?? string.Empty).Trim();
                if (discDesc.Length > 25)
                    discDesc = discDesc.Substring(0, 25);

                string discAmt = entry.DiscountAmount.ToString("0.00");

                string line = $"{discCode}, {discDesc}, {discAmt}";
                lines.Add(line);
            }

            int safeBatch = data.BatchNumber <= 0 ? 1 : data.BatchNumber;
            string fileName = GenerateFileName(data.TenantCode, data.POSTerminalNumber, safeBatch, data.BusinessDate);
            string fullPath = Path.Combine(outputDirectory, fileName);

            File.WriteAllLines(fullPath, lines, Encoding.UTF8);
            Console.WriteLine($"✅ Discount file created: {fullPath}");
        }

        private static string GenerateFileName(string tenantCode, int terminalNumber, int batchNumber, DateTime date)
        {
            string safeTenantCode = (tenantCode ?? "").PadRight(4, '0').Substring(0, 4).ToUpper();
            int safeBatch = batchNumber <= 0 ? 1 : batchNumber;
            string monthCode = date.Month <= 9
                ? date.Month.ToString()
                : ((char)('A' + (date.Month - 10))).ToString(); // A–C for Oct–Dec

            string dayCode = date.Day.ToString("D2");
            return $"D{safeTenantCode}{terminalNumber.ToString("D2")}{safeBatch}.{monthCode}{dayCode}";
        }
    }
}
