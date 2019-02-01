using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TPproject.Models
{
    public class WaybillContext : DbContext
    {
        public WaybillContext() :
        base("DbConnection")
        { }

        public DbSet<Waybill> Waybill { get; set; }
        public DbSet<Bus> Bus { get; set; }
        public DbSet<WaybillStatus> WaybillStatus { get; set; }

        
    }
}