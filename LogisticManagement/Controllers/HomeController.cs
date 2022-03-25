using LogisticManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LogisticManagement.Controllers
{
    public class HomeController : Controller
    {
        private LogisticManagementEntities db = new LogisticManagementEntities();
        public ActionResult Index()
        {
            return View();
        }
      /*  public ActionResult ContactDetails()
        {
            //var contact = db.Contacts.ToList();

            return View(db.Contacts.ToList());
        }*/

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            var data = new ConatactDetail();

            return View(data);
        }

        
        public ActionResult Contact_Details(ConatactDetail ctd)
        {
            //var contact = db.Contacts.ToList();
            ConatactDetail cd = db.ConatactDetails.FirstOrDefault(x => x.City == ctd.City);

            return View(cd);
        }
    }
}