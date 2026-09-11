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
    public class DailyDataDetailsStorage : BaseStorage<DailyDataDetails>
    {
        private string _tableName = new DailyDataDetails().GetTableName();
        public DailyDataDetailsStorage() : base(Program.DatabaseFile)
        {
            CreateTable(new DailyDataDetails());
        }
        //public DataTable GetInvoiceTransactions(string Terminal)
        //{
        //    //string query = string.Format(Queries.SELECT_TRANSACTION_ID, _tableName, Terminal);
        //    //return GetDataTable(query);
        //}
        //public DataTable UpdateInvoiceTransaction(string tranID, string terminal)
        //{
        //    //string query = string.Format(Queries.UPDATE_TRANSACTION_ID, _tableName, tranID, terminal);
        //    //return GetDataTable(query);
        //}
    }
}
