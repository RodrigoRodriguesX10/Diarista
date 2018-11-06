using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Diarista.Models
{
    public class DatabaseConnection : DbContext
    {
        public DatabaseConnection() :
           base("name=DatabaseConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<User> Users { get; set; }

    }
}