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
            string where = string.IsNullOrEmpty(Terminal) ? "1=1" : string.Format("TER_NO = '{0}'", Terminal);
            string query = string.Format(Queries.SELECT_TABLE_WHERE_DESC_LIMIT1, _tableName, where, "ID");
            return GetDataTable(query);
        }
        public DataTable LastEOD()
        {
            string query = string.Format("Select * from {0} Order by {1} desc limit 1", _tableName, "ID");
            return GetDataTable(query);
        }
        public DataTable GetInvoiceTransactions(DateTime processDate, string Terminal)
        {
            string where = string.IsNullOrEmpty(Terminal)
                ? string.Format("BUS_DATE = '{0}'", processDate.ToString("yyyy-MM-dd"))
                : string.Format("BUS_DATE = '{0}' AND TER_NO = '{1}'", processDate.ToString("yyyy-MM-dd"), Terminal);
            string query = string.Format(Queries.SELECT_TABLE_WHERE, _tableName, where);
            return GetDataTable(query);
        }
        public DataTable GetInvoiceTransactionsPrint(DateTime processDate)
        {
            string query = string.Format(Queries.SELECT_TABLE_WHERE, _tableName, string.Format("BUS_DATE = '{0}'", processDate.ToString("yyyy-MM-dd")));
            return GetDataTable(query);
        }

        public decimal GetPreviousGrandTotal(string terminal, DateTime businessDate)
        {
            string busDateStr = businessDate.ToString("yyyy-MM-dd");
            string where = string.IsNullOrEmpty(terminal)
                ? $"BUS_DATE < '{busDateStr}'"
                : $"BUS_DATE < '{busDateStr}' AND TER_NO = '{terminal}'";
            string query = $"SELECT NEW_GRNTOT FROM {_tableName} WHERE {where} ORDER BY BUS_DATE DESC, ID DESC LIMIT 1";
            try
            {
                DataTable dt = GetDataTable(query);
                if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["NEW_GRNTOT"] != DBNull.Value)
                {
                    return dt.Rows[0]["NEW_GRNTOT"].ToSafeDecimal();
                }
            }
            catch { }
            return 0.00M;
        }

        public int GetDailyBatchNumber(string terminal, DateTime businessDate)
        {
            string busDateStr = businessDate.ToString("yyyy-MM-dd");
            string where = string.IsNullOrEmpty(terminal)
                ? $"BUS_DATE = '{busDateStr}'"
                : $"BUS_DATE = '{busDateStr}' AND TER_NO = '{terminal}'";
            string query = $"SELECT MAX(EOD_CNT) FROM {_tableName} WHERE {where}";
            try
            {
                DataTable dt = GetDataTable(query);
                if (dt != null && dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
                {
                    int currentBatch = dt.Rows[0][0].ToSafeInteger();
                    return Math.Min(9, Math.Max(1, currentBatch + 1));
                }
            }
            catch { }
            return 1;
        }

    }
}
