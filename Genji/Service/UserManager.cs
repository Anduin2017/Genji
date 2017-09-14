using System;
using System.Collections.Generic;
using System.Text;

namespace Genji.Service
{
    public class UserManager<Db>
    {
        private Db _db;
        public UserManager(Db database)
        {
            _db = database;
        }
    }
}
