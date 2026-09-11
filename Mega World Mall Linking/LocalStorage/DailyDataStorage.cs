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
    public class DailyDataStorage : BaseStorage<Daily_SLS>
    {
        private string _tableName = new Daily_SLS().GetTableName();
        public DailyDataStorage() : base(Program.DatabaseFile)
        {
            CreateTable(new Daily_SLS());
        }
        //public DataTable GetlastOR(DateTime date, string terminal)
        //{
        //    //string querries = string.Format(Queries.Select_LastOR, "ETRANS", _tableName, string.Format("(BUS_DATE) = date('{0}')", date.ToString("yyyy-MM-dd")), terminal);
        //    //return GetDataTable(querries);
        //}
        public DataTable GetLastEOD(string Terminal)
        {
            // string query = string.Format(Queries.SELECT_TABLE_DESC_LIMIT1, _tableName, "TRN_DATE");
            string query = string.Format(Queries.SELECT_TABLE_DESC_LIMIT1, _tableName, "ID", string.Format("(TER_NO = '{0}')", Terminal));
            // string query = string.Format(Queries.SELECT_TABLE_DESC_LIMIT1, _tableName, "ID",string.Format("(TER_NO = '{0}')",Terminal));
            return GetDataTable(query);
        }
        public DataTable LastEOD()
        {
            //string query = string.Format(Queries.SELECT_TABLE_DESC_LIMIT1, _tableName, "ID");
            string query = string.Format("Select * from {0} Order by {1} desc limit 1", _tableName, "ID");
            return GetDataTable(query);
        }
        public DataTable GetInvoiceTransactions(DateTime processDate, string Terminal)
        {
            string query = string.Format(Queries.SELECT_TABLE_WHERE, _tableName, string.Format("(TRN_DATE) = date('{0}')", processDate.ToString("yyyy-dd-MM")), string.Format("(TER_NO) = '{0}'", Terminal));
            return GetDataTable(query);
        }
        public DataTable GetInvoiceTransactionsPrint(DateTime processDate)
        {
            string query = string.Format(Queries.SELECT_TABLE_WHERE, _tableName, string.Format("(TRN_DATE) = date('{0}')", processDate.ToString("yyyy-dd-MM")));
            return GetDataTable(query);
        }

    }
}
