using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPoco;

namespace JsonEditor_Backend.API.NPoco
{
    public class ExtendedDatabase : Database, IExtendedDatabase
    {
        private string defaultPrimaryKeyColumn = "Id";

        public ExtendedDatabase(string connectionStringName)
            : base(connectionStringName)
        {
        }

        public string DefaultPrimaryKeyColumn
        {
            get { return defaultPrimaryKeyColumn; }
            set { defaultPrimaryKeyColumn = value; }
        }

        public object Insert<T>(string tableName, T poco)
        {
            return Insert(tableName, defaultPrimaryKeyColumn, poco);
        }

        public object Insert<T>(string tableName, T poco, bool autoIncrement)
        {
            return Insert(tableName, defaultPrimaryKeyColumn, autoIncrement, poco);
        }
    }
}