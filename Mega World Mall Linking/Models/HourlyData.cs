using Mega_World_Mall_Linking.LocalStorage.Attributes;
using Tarsier.Database.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mega_World_Mall_Linking.Models
{
    public class HourlyData
    {
        [DbColumn(IsIdentity = true, IsPrimary = true, AutoIncrement = true)]
        public int ID { get; set; }

        [DbColumn(ColumnType = ColType.Text, NotNull = true)]
        public string TENANTCODE { get; set; }

        [DbColumn(ColumnType = ColType.Text, NotNull = true)]
        public string TRAN_ID { get; set; }

        [DbColumn(ColumnType = ColType.Text, NotNull = true)]
        public string TER_NO { get; set; }

        [DbColumn(ColumnType = ColType.DateTime, NotNull = true)]
        public string BUS_DATE { get; set; }

        [DbColumn(ColumnType = ColType.Decimal, NotNull = true)]
        public decimal TOT_NET { get; set; }

        [DbColumn(ColumnType = ColType.Decimal, NotNull = true)]
        public int TOT_TRN { get; set; }

        [DbColumn(ColumnType = ColType.Decimal, NotNull = true)]
        public int TOT_CUS { get; set; }
    }
}
