using Mega_World_Mall_Linking.LocalStorage.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarsier.Database.Enums;

namespace Mega_World_Mall_Linking.Models
{
    public class DailyDataDetails
    {
        [DbColumn(IsIdentity = true, IsPrimary = true, AutoIncrement = true)]
        public int ID { get; set; }

        [DbColumn(ColumnType = ColType.Text, NotNull = true)]
        public string TRAN_ID { get; set; }

        [DbColumn(ColumnType = ColType.Text, NotNull = true)]
        public string SLS_TYPE { get; set; }

        [DbColumn(ColumnType = ColType.Decimal, NotNull = true)]
        public Decimal NET_SLS { get; set; }
    }
}
