using Mega_World_Mall_Linking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarsier.Extensions;

namespace Mega_World_Mall_Linking.LocalStorage
{
    public class HourlyDataDetailsStorage : BaseStorage<HourlyDataDetails>
    {
        private string _tableName = new HourlyDataDetails().GetTableName();

        public HourlyDataDetailsStorage() : base(Program.DatabaseFile)
        {
            CreateTable(new HourlyDataDetails());
        }
    }
}
