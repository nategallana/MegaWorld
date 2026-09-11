using Mega_World_Mall_Linking.LocalStorage.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarsier.Database.Enums;

namespace Mega_World_Mall_Linking.Models
{
    public class DiscountData
    {
        [DbColumn(IsIdentity = true, IsPrimary = true, AutoIncrement = true)]
        public int ID { get; set; }
        [DbColumn(ColumnType = ColType.Text, NotNull = true)]
        public string ORDER_ID { get; set; }

        [DbColumn(ColumnType = ColType.Text, NotNull = true)]
        public string DISCOUNT_CODE { get; set; }

        [DbColumn(ColumnType = ColType.Text, NotNull = true)]
        public string DISCOUNT_DESC { get; set; }

        [DbColumn(ColumnType = ColType.Decimal, NotNull = true)]
        public decimal DISCOUNT_AMT { get; set; }
    }
}
