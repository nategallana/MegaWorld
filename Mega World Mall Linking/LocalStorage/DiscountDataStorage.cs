using Mega_World_Mall_Linking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarsier.Extensions;

namespace Mega_World_Mall_Linking.LocalStorage
{
    public class DiscountDataStorage : BaseStorage<DiscountData>
    {
        private string _tableName = new DiscountData().GetTableName();
        
        public DiscountDataStorage() : base(Program.DatabaseFile)
        {
            CreateTable(new DiscountData());
        }
    }
}
