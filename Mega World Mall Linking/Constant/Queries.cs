using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mega_World_Mall_Linking.Constant
{
    public class Queries
    {
        public const string SELECT_TABLE = "SELECT * FROM {0}";
        public const string SELECT_TABLE_Top1 = "SELECT * FROM {0} WHERE {1} ORDER BY Time_Now ASC LIMIT 1";
        public const string SELECT_TABLE_WHERE = "SELECT * FROM {0} WHERE {1}";
        public const string SELECT_TABLE_ASC = "SELECT * FROM {0} ORDER BY {1} ASC";
        public const string SELECT_TABLE_DESC = "SELECT * FROM {0} ORDER BY {1} DESC";
        public const string SELECT_TABLE_DESC_LIMIT1 = "SELECT * FROM {0} ORDER BY {1} DESC LIMIT 1";
        public const string SELECT_TABLE_WHERE_LIMIT_ASC = "SELECT * FROM {0} WHERE {1} ORDER BY Time_Now ASC LIMIT 1";
        public const string SELECT_TABLE_WHERE_LIMIT_DESC = "SELECT * FROM {0} WHERE {1} ORDER BY Time_Now DESC LIMIT 1";
        //public const string SELECT_TABLE_VOID = "SELECT "
        public const string SELECT_EXCEL = "Select * from [{0}$]";
        public const string SELECT_MIN_WHERE = "SELECT MIN({0}) AS STARTOR FROM {1} WHERE {2}";
        public const string SELECT_MAX_WHERE = "SELECT MAX({0}) AS ENDOR FROM {1} WHERE {2}";
        public const string GET_STARTHOUR = "SELECT * FROM {0} WHERE AccDate = '{1}' ORDER BY Time_Now ASC LIMIT 1";
        public const string GET_ENDHOUR = "SELECT Time FROM {0} WHERE AccDate = '{1}' ORDER BY Time_Now DESC LIMIT 1";
        //public const string GET_ENDHOUR = "SELECT TOP1 STRFTIME('%H', Time) as EndHour from {0} where AccDate ='{1}' ORDER BY AccDate DESC";

        public const string SELECT_TABLE_WHERE_DESC_LIMIT1 = "SELECT * FROM {0} WHERE {1} ORDER BY {2} DESC LIMIT 1";

        //wbox paradox queries
        //public const string OrderList = @"Select *
        //                              From {0} Where AcDate = #{1:MM/dd/yyyy}#";

        public const string OrderList = @"Select *
                                      From {0} Where TableNo NOT IN ('CASH IN', 'CASH OUT') AND AcDate = #{1:MM/dd/yyyy}#";

        public const string GET_PAYMENTAPPLIED = @"Select CAmount,VAmount,AAmount,MAmount,QAmount,IAmount,DAmount,UAmount,YAmount,OAmount From {0} Where {0}.OrderNo = '{1}'";
        public const string GET_PAYMENTNAME = @"Select Name2 From Defpay Where Remark = '{0}'";
        public const string GET_SALESDISCOUNT = @"Select Number3, DiscType From {0} Where {0}.OrderNo = '{1}' And Posted = -1 And Void = 0 And AcDate = #{2:MM/dd/yyyy}#";
        public const string GET_ORDERITEMS = @"Select * From {0} Where {0}.OrderNo = '{1}'";
        public const string GET_PAYMENT = @"Select Name2 From Defpay";
        public const string GET_TABLES = @"Select TableNo From Tables";
        public const string GET_OTHERPAYMENT = @"Select Name2 From Defpay2";
        public const string GET_DISCOUNT = @"Select Name1 From DefDisc";
        public const string GET_TRANTYPE = @"Select {0}.TableNo,{0}.AreaID,{1}.AreaName From {0} INNER JOIN {1} ON {0}.AreaID = {1}.AreaID Where {0}.TableNo = '{2}'";
    }
}

