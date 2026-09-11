using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarsier.Database.Enums;

namespace Mega_World_Mall_Linking.LocalStorage.Attributes
{
    public class DbColumnAttribute : Attribute
    {
        public bool Convert { get; set; }
        /// <summary>
        /// Set true if ID is auto increment
        /// </summary>
        public bool AutoIncrement { get; set; }
        /// <summary>
        /// Set true if the property is primary key in the table
        /// </summary>
        public bool IsPrimary { get; set; }
        /// <summary>
        /// Denotes if the field is an identity type or not.
        /// </summary>
        public bool IsIdentity { get; set; }
        /// <summary>
        /// Set column data type
        /// </summary>
        public ColType ColumnType { get; set; }
        /// <summary>
        /// Indicate column not null
        /// </summary>
        public bool NotNull { get; set; }
        /// <summary>
        /// Set default value
        /// </summary>
        public object DefaultValue { get; set; } = string.Empty;
    }
}
