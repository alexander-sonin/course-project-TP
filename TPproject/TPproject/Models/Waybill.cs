using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TPproject.Models
{
    public class Waybill
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int BusId { get; set; }
        public Bus Bus { get; set; }
        public DateTime time_start { get; set; }
        public DateTime time_end { get; set; }
        public int WaybillStatusId { get; set; }
        public WaybillStatus WaybillStatus { get; set; }
    }
    public class Bus
    {
        public int Id { get; set; }
        public string name_bus { get; set; }
        public string number_bus { get; set; }
        public string color_bus { get; set; }
        public string vin_bus { get; set; }
    }
    public class WaybillStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    

}