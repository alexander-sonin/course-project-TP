using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPproject.Models;

namespace TPproject.Controllers
{
    public class ManageController : Controller
    {
        
        UserContext userContext = new UserContext();
        WaybillContext waybillContext = new WaybillContext();

        [Authorize]
        public ActionResult Index()
        {
            return View(userContext.GetUser(User.Identity.Name));
        }
        [Authorize]
        public ActionResult EditUser(int id)
        {
                User user = userContext.User.Find(id);
                if (user != null)
                {
                    return PartialView("EditUser", user);
                }
                return View("Index");
        }
        [HttpPost]
        public ActionResult EditUser(User user)
        {
            userContext.Entry(user).State = EntityState.Modified;
            userContext.SaveChanges();
            return RedirectToAction("AdminView");
        }
        [Authorize]
        public ActionResult DeleteUser(int id)
        {
            User user = userContext.User.Find(id);
            if (user != null)
            {
                return PartialView("DeleteUser", user);
            }
            return View("AdminView");
        }

        [HttpPost]
        public ActionResult DeleteUser(int id, FormCollection collection)
        {
            User user = userContext.User.Find(id);
            if (user != null)
            {
                userContext.User.Remove(user);
                userContext.SaveChanges();
            }
            return RedirectToAction("AdminView");
        }



        [Authorize(Roles = "admin")]
        public ActionResult AdminView()
        {
            var roles = userContext.User.Include(p => p.Role);
            return View(roles.ToList());
        }


        [Authorize(Roles = "manager")]
        public ActionResult ManagerAddBus()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ManagerAddBus(Bus bus)
        {
            waybillContext.Bus.Add(bus);
            waybillContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult WaybillList()
        {

            var waybils = waybillContext.Waybill.Include(p => p.Bus).Include(x => x.User).Include(y => y.WaybillStatus);

            return View(waybils.ToList());
            
        }
        public ActionResult CreateWaybill()
        {
            var users = userContext.User.Where(g=>g.RoleID==4).ToList();
            var buses = waybillContext.Bus.ToList();
            ViewBag.bus = buses;
            ViewBag.user = users;
            return View();
        }
        [HttpPost]
        public ActionResult CreateWaybill(Waybill waybill)
        {
         
            waybill.Bus = waybillContext.Bus.FirstOrDefault(r => r.number_bus.Equals(waybill.Bus.number_bus));
            waybill.User = userContext.User.FirstOrDefault(r => r.LastName.Equals(waybill.User.LastName));
            waybill.time_start = DateTime.Now;
            waybill.time_end = DateTime.MaxValue;
            waybill.WaybillStatusId = 1;
            waybillContext.Waybill.Add(waybill);
            waybillContext.SaveChanges();
            
            return RedirectToAction("Index");
        }
        [Authorize]
        public ActionResult BusDelete(int id)
        {
            Bus bus = waybillContext.Bus.Find(id);
            if (bus != null)
            {
                return PartialView("BusDelete", bus);
            }
            return View("Services");
        }

        [HttpPost]
        public ActionResult BusDelete(int id, FormCollection collection)
        {
            Bus bus = waybillContext.Bus.Find(id);
            if (bus != null)
            {
                waybillContext.Bus.Remove(bus);
                waybillContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [Authorize]
        public ActionResult BusEdit(int id)
        {
            Bus bus = waybillContext.Bus.Find(id);
            if (bus != null)
            {
                return PartialView("BusEdit", bus);
            }
            return View("Index");
        }
        [HttpPost]
        public ActionResult BusEdit(Bus bus)
        {
            waybillContext.Entry(bus).State = EntityState.Modified;
            waybillContext.SaveChanges();
            return RedirectToAction("Index");
        }


        [Authorize]
        public ActionResult WaybillEdit(int id)
        {
            Waybill waybill = waybillContext.Waybill.Find(id);
            if (waybill != null)
            {
                return PartialView("WaybillEdit", waybill);
            }
            return View("WaybillList");
        }
        [HttpPost]
        public ActionResult WaybillEdit(Waybill waybill)
        {
            waybillContext.Entry(waybill).State = EntityState.Modified;

            waybill.Bus = waybillContext.Bus.Where(n => n.number_bus == waybill.Bus.number_bus).FirstOrDefault();
            waybill.BusId = waybill.Bus.Id;
            waybill.User = userContext.User.Where(u => u.LastName == waybill.User.LastName).Where(g => g.RoleID == 4).FirstOrDefault();
            waybill.UserId = waybill.User.Id;
            waybillContext.SaveChanges();
            return RedirectToAction("WaybillList");
        }

        

        [Authorize]
        public ActionResult WaybillDetails(int id)
        {
            
            Waybill waybill = waybillContext.Waybill.Find(id);
            if (waybill != null)
            {
                return View(waybill);
            }
            return View("Index");
        }
        [HttpPost]
        public ActionResult WaybillDetails(int id,string command)
        {
            Waybill waybill = waybillContext.Waybill.Find(id);
            if (waybill != null)
            {
                if (command.Equals("Выполнен"))
                {
                    waybill.WaybillStatusId = 2;
                    waybillContext.Entry(waybill).State = EntityState.Modified;
                    waybillContext.SaveChanges();
                    return RedirectToAction("WaybillList");
                }
                if (command.Equals("Отклонить"))
                {
                    waybill.WaybillStatusId = 3;
                    waybillContext.Entry(waybill).State = EntityState.Modified;
                    waybillContext.SaveChanges();
                    return RedirectToAction("WaybillList");
                }
            }
            return RedirectToAction("Index");
        }
        [Authorize]
        public ActionResult BusList()
        {
            return View(waybillContext.Bus);
        }
        [Authorize]
        public ActionResult DeleteWaybill(int id)
        {
            Waybill waybill = waybillContext.Waybill.Find(id);
            if (waybill != null)
            {
                return PartialView("DeleteWaybill", waybill);
            }
            return View("WaybillList");
        }

        [HttpPost]
        public ActionResult DeleteWaybill(int id, FormCollection collection)
        {
            Waybill waybill = waybillContext.Waybill.Find(id);
            
            if (waybill != null)
            {
                waybillContext.Waybill.Remove(waybill);
                waybillContext.SaveChanges();
            }
            return RedirectToAction("WaybillList");
        }
       
    }
}
