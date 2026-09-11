using Mega_World_Mall_Linking.LocalStorage.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarsier.Database.Enums;

namespace Mega_World_Mall_Linking.Models
{
    public class HourlyDataDetails
    {
        [DbColumn(IsIdentity = true, IsPrimary = true, AutoIncrement = true)]
        public int ID { get; set; }

        [DbColumn(ColumnType = ColType.Text, NotNull = true)]
        public string TRAN_ID { get; set; }
        [DbColumn(ColumnType = ColType.Text, NotNull = true)]
        public string HOUR_CODE { get; set; }

        [DbColumn(ColumnType = ColType.Decimal, NotNull = true)]
        public decimal HNET_SLS { get; set; }

        [DbColumn(ColumnType = ColType.Integer, NotNull = true)]
        public int TRN_NO { get; set; }

        [DbColumn(ColumnType = ColType.Integer, NotNull = true)]
        public int CUS_NO { get; set; }
    }
}
