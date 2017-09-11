using System;
using System.Collections.Generic;
using System.Text;

namespace Mercy.Service
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
