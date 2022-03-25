using LogisticManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using Rotativa;


namespace LogisticManagement.Controllers
{
    public class ConsignmentController : Controller
    {
        private LogisticManagementEntities db = new LogisticManagementEntities();
        [HttpGet]
        // GET: Consignment
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Consignment_booking()
        {
            //ViewBag.name = "Ashish";
            return View();
        }

        public ActionResult UserContactDetails()
        {
            var emailid = TempData["email"];
           User em = db.Users.FirstOrDefault(x => x.Email_ID == emailid);
            if(em == null)
            {
                ViewBag.name = TempData["name"];
                ViewBag.email = TempData["email"];
            }
            else
            {
                ViewBag.name = em.Name;
                ViewBag.email = em.Email_ID;
                ViewBag.mobile = em.MobileNO;
                ViewBag.address = em.Address;

            }
            return View();
        }

        public ActionResult Consignment_Details()
        {
            var id = TempData["id"];
            ViewBag.id = id;
            return View();
        }

        [HttpPost]
        public ActionResult Consignment_Details(Consignment cn)
        {
            var pickup = cn.PickupType;
            TempData["Pkic"] = pickup;
            var destination = cn.Reciver_Address;
            TempData["destination"] = destination;
            db.Consignments.Add(cn);
            db.SaveChanges();

            var cid = cn.CId;
            TempData["cid"] = cid;
            TempData["bid"] = cid;

            var time = DateTime.Now;

            Tracking tr = new Tracking() { Bid = cid, Consignment_Procurement = time };
            db.Trackings.Add(tr);
            db.SaveChanges();
            return RedirectToAction("ConfirmationPage");
        }

        [HttpPost]
        public ActionResult Consignment_booking(User us)
        {
            var name = us.Name;
           TempData["name"] = name;
            var email = us.Email_ID;
           TempData["email"] = email;

            User em = db.Users.FirstOrDefault(x => x.Email_ID == us.Email_ID);
            if (em == null)
            {
                return RedirectToAction("UserContactDetails");
            }
            else
            {
                return RedirectToAction("UserContactDetails");
            }
            
        }

        [HttpPost]
        public ActionResult UserContactDetails(User us)
        {
            var source = us.Address;
            TempData["source"] = source;
            User em = db.Users.FirstOrDefault(x => x.Email_ID == us.Email_ID);
            if (em == null)
            {
                db.Users.Add(us);
                db.SaveChanges();
                var id = us.UId;
                TempData["id"] = id;
            }
            else
            {
                var id = em.UId;
                TempData["id"] = id;
            }

            return RedirectToAction("Consignment_Details",us);
        }
        public ActionResult ConfirmationPage()
        {
            
            string pickup = (string)TempData["Pkic"];
            if(pickup == "slf drop")
            {
                var source = TempData["source"];
                ViewBag.source = source;
                var destination = TempData["destination"];
                ViewBag.destination = destination;
                var cid = (int)TempData["cid"];
                TempData["c"] = cid;
                ViewBag.cid = cid;
                Consignment em = db.Consignments.Find(cid);
                return View(em);
            }
            else
            {
                return RedirectToAction("ConformationPage2");
            }
           
        }
        public ActionResult ConformationPage2()
        {
            var source = TempData["source"];
            ViewBag.source = source;
            var destination = TempData["destination"];
            ViewBag.destination = destination;
            int cid = (int)TempData["cid"];
            ViewBag.cid = cid;
            var slot = DateTime.Now.AddDays(1);
            ViewBag.slot = slot;
            TempData["c"] = cid;
           
            Consignment em = db.Consignments.Find(cid);

            return View(em);
        }

        public ActionResult selfDownload()
        {
            
            int c = (int)TempData["c"];
            Consignment consignment = db.Consignments.Find(c);
            var pdfResulst = new ViewAsPdf("ConfirmationPage", consignment);
            
            return pdfResulst;
        }
        public ActionResult reciverDownload()
        {

            int c = (int)TempData["c"];
            Consignment consignment = db.Consignments.Find(c);
            var pdfResulst = new ViewAsPdf("ConfirmationPage", consignment);

            return pdfResulst;
        }

        public ActionResult Tracking()
        {

            return View();
        }

        [HttpPost]
        
        public ActionResult Tracking_Details(int bid)
        {
        int id = bid;
        Tracking t = db.Trackings.Find(id);
        return View(t);
        }


    }
}
