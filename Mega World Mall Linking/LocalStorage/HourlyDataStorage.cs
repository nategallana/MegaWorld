using Mega_World_Mall_Linking.Constant;
using Mega_World_Mall_Linking.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarsier.Extensions;

namespace Mega_World_Mall_Linking.LocalStorage
{
    public class HourlyDataStorage : BaseStorage<HourlyData>
    {
        private string _tableName = new HourlyData().GetTableName();

        public HourlyDataStorage() : base(Program.DatabaseFile)
        {
            CreateTable(new HourlyData());
        }
        public DataTable GetInvoiceTransactions(DateTime processDate)
        {
            string query = string.Format(Queries.SELECT_TABLE_WHERE, _tableName, string.Format("(TRN_DATE) = date('{0}')", processDate.ToString("yyyy-MM-dd")));
            return GetDataTable(query);
        }
    }
}
