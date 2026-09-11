using Mega_World_Mall_Linking.LocalStorage.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Tarsier.Database.Enums;

namespace Mega_World_Mall_Linking.Models
{
    public class Daily_SLS
    {
        [DbColumn(IsIdentity = true, IsPrimary = true, AutoIncrement = true)]
        public int ID { get; set; }

        [DbColumn(ColumnType = ColType.Text, NotNull = true)]
        public string TENANT_CODE { get; set; }

        [DbColumn(ColumnType = ColType.Text, NotNull = true)]
        public string TER_NO { get; set; }

        [DbColumn(ColumnType = ColType.Text, NotNull = true)]
        public string BUS_DATE { get; set; }

        [DbColumn(ColumnType = ColType.Text, NotNull = true)]
        public string STR_TRN_ID { get; set; }
        [DbColumn(ColumnType = ColType.Text, NotNull = true)]
        public string END_TRN_ID { get; set; }

        [DbColumn(ColumnType = ColType.Decimal, NotNull = true)]
        public decimal OLD_GRNTOT { get; set; }

        [DbColumn(ColumnType = ColType.Decimal, NotNull = true)]
        public decimal NEW_GRNTOT { get; set; }

        [DbColumn(ColumnType = ColType.Decimal, NotNull = true)]
        public decimal GROSS_SLS { get; set; }

        [DbColumn(ColumnType = ColType.Decimal, NotNull = true)]
        public decimal NON_TAXSLS { get; set; }

        [DbColumn(ColumnType = ColType.Decimal, NotNull = true)]
        public decimal SC_DISC { get; set; }

        [DbColumn(ColumnType = ColType.Decimal, NotNull = true)]
        public decimal OTHR_DISC { get; set; }

        [DbColumn(ColumnType = ColType.Decimal, NotNull = true)]
        public decimal REFUND { get; set; }

        [DbColumn(ColumnType = ColType.Decimal, NotNull = true)]
        public decimal VAT_AMT { get; set; }

        [DbColumn(ColumnType = ColType.Decimal, NotNull = true)]
        public decimal SRVC_CHRG { get; set; }

        [DbColumn(ColumnType = ColType.Decimal, NotNull = true)]
        public decimal NET_SLS { get; set; }

        [DbColumn(ColumnType = ColType.Decimal, NotNull = true)]
        public decimal CASH_SLS { get; set; }

        [DbColumn(ColumnType = ColType.Decimal, NotNull = true)]
        public decimal CHRG_SLS { get; set; }

        [DbColumn(ColumnType = ColType.Decimal, NotNull = true)]
        public decimal GC_OTHR_SLS { get; set; }

        [DbColumn(ColumnType = ColType.Decimal, NotNull = true)]
        public decimal VOID_AMT { get; set; }

        [DbColumn(ColumnType = ColType.Integer, NotNull = true)]
        public int CUS_CNT { get; set; }

        [DbColumn(ColumnType = ColType.Integer, NotNull = true)]
        public int CTL_NO { get; set; }

        [DbColumn(ColumnType = ColType.Integer, NotNull = true)]
        public int TRN_CNT { get; set; }
        [DbColumn(ColumnType = ColType.Decimal, NotNull = true)]
        public decimal EOD_CNT { get; set; }
    }
}
