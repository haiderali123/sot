using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using aghaApi.Models;
namespace aghaApi.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        finalEntities ctx = new finalEntities();
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
            {
                return View();
            }
            else
                return RedirectToAction("/Main");
        }
        public void Logout()
        {
            Session["Admin"] = null;
            Response.Redirect("/Admin/Main");
        }

        public ActionResult addAdmin()
        {
            if (Session["Admin"] != null)
            {
            return View();
            }
            else
                return RedirectToAction("/Index");
        }
        [HttpPost]
        public void saveadmin(admin a)
        {
            
            ctx.admins.Add(a);
            try
            {
                ctx.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];
                file.SaveAs(Server.MapPath(@"~\admin_" + a.Id + ".jpeg"));
                break;
            }
            Response.Redirect("/Admin/Main");
        }

        [HttpPost]
        public void saveworker(worker_Portfolio a)
        {
            ctx.worker_Portfolio.Add(a);
            try
            {
                ctx.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

           

            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];
                file.SaveAs(Server.MapPath(@"~\worker_" + a.Id + ".jpeg"));
                break;
            }
            
            Availability_Slots slot = new Availability_Slots();
            slot.Availability_Slots1 = "10am - 11am";
            slot.wid = a.Id;
            slot.IsAvailable = 0;
            ctx.Availability_Slots.Add(slot);
            Availability_Slots slot1 = new Availability_Slots();
            slot1.Availability_Slots1 = "11am - 12pm";
            slot1.wid = a.Id;
            slot1.IsAvailable = 0;
            ctx.Availability_Slots.Add(slot1);

            Availability_Slots slot2 = new Availability_Slots();
            slot2.Availability_Slots1 = "12pm - 1pm";
            slot2.wid = a.Id;
            slot2.IsAvailable = 0;
            ctx.Availability_Slots.Add(slot2);

            Availability_Slots slot3 = new Availability_Slots();
            slot3.Availability_Slots1 = "1pm - 2pm";
            slot3.wid = a.Id;
            slot3.IsAvailable = 0;
            ctx.Availability_Slots.Add(slot3);

            Availability_Slots slot4 = new Availability_Slots();
            slot4.Availability_Slots1 = "2pm - 3pm";
            slot4.wid = a.Id;
            slot4.IsAvailable = 0;
            ctx.Availability_Slots.Add(slot4);

            Availability_Slots slot5 = new Availability_Slots();
            slot5.Availability_Slots1 = "3pm - 4pm";
            slot5.wid = a.Id;
            slot5.IsAvailable = 0;
            ctx.Availability_Slots.Add(slot5);

            Availability_Slots slot6 = new Availability_Slots();
            slot6.Availability_Slots1 = "4pm - 5pm";
            slot6.wid = a.Id;
            slot6.IsAvailable = 0;
            ctx.Availability_Slots.Add(slot6);

            Availability_Slots slot7 = new Availability_Slots();
            slot7.Availability_Slots1 = "5pm - 6pm";
            slot7.wid = a.Id;
            slot7.IsAvailable = 0;
            ctx.Availability_Slots.Add(slot7);

            Availability_Slots slot8 = new Availability_Slots();
            slot8.Availability_Slots1 = "6pm - 7pm";
            slot8.wid = a.Id;
            slot8.IsAvailable = 0;
            ctx.Availability_Slots.Add(slot8);

            Availability_Slots slot9 = new Availability_Slots();
            slot9.Availability_Slots1 = "7pm - 8pm";
            slot9.wid = a.Id;
            slot9.IsAvailable = 0;
            ctx.Availability_Slots.Add(slot9);

            
            try
            {
                ctx.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Response.Redirect("/Admin/Main");
        }
        [HttpPost]
        public void SaveService(service a)
        {
            
            ctx.services.Add(a);
            try
            {
                ctx.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
           // service service = ctx.services.Where(x => x.service_type == a.service_type).ToList()[0];
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];

                file.SaveAs(Server.MapPath(@"~\service_" + a.Id + ".jpeg"));
                break;
            }
            Response.Redirect("/Admin/Main");
        
        }
        public ActionResult SearchUser()
        {
            String search = ViewBag.search = Request["search"];
            List<user> lst = ctx.users.Where(x => x.username.Contains(search)).ToList();
            return View(lst);
        }

        [HttpPost]
        public ActionResult SavePromotion(promotion a)
        {
            
            ctx.promotions.Add(a);
            try
            {
                ctx.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];
                file.SaveAs(Server.MapPath(@"~\promotion_" + a.Id + ".jpg"));
                break;
            }
            return RedirectToAction("/Index");
        }
        public ActionResult createWorker()
        {
            if (Session["Admin"] != null)
            {
                ViewData["services"] = ctx.services;
            return View();
            }
            else
                return RedirectToAction("/Index");
        }

        [HttpPost]
        public ActionResult addworker(worker_Portfolio worker)
        {
            if (Session["Admin"] != null)
            {
            if (ctx.worker_Portfolio.Any(x => x.cnic == worker.cnic))
            {
                ViewBag.message = "CNIC number already exist";
                ViewBag.s = "failed";
                return View();
            }
            else
            {

                ctx.worker_Portfolio.Add(worker);
                ctx.SaveChanges();
                ViewBag.message2 = "You are Successfully Registered";
                ViewBag.s = "Success";
                return View();
            }
            }
            else
                return RedirectToAction("/Index");
        }
        public ActionResult viewWorker()
        {
            if (Session["Admin"] != null)
            {
            List<worker_Portfolio> list = ctx.worker_Portfolio.ToList();
            
            return View(list);
            }
            else
                return RedirectToAction("/Index");
        }
        public ActionResult addService()
        {
            if (Session["Admin"] != null)
            {
                ViewData["categories"] = ctx.categories;
            return View();
                 }
            else
                return RedirectToAction("/Index");
        }
        public ActionResult viewService()
        {
            if (Session["Admin"] != null)
            {
            List<service> list = ctx.services.ToList();
            return View(list);
            }
            else
                return RedirectToAction("/Index");
        }
        public ActionResult addPromotion()
        {        
            if (Session["Admin"] != null)
            {
            return View();
             }
            else
                return RedirectToAction("/Index");
        }
        public ActionResult viewPromotion()
        {
if (Session["Admin"] != null)
            {
            List<promotion> list = ctx.promotions.ToList();
            return View(list);
     }
            else
                return RedirectToAction("/Index");
        }

        [HttpPost]
        public void addUser(user_request request)
        {
           

            ctx.user_request.Add(request);
            ctx.SaveChanges();
            ViewBag.message2 = "You are Successfully Registered";
            ViewBag.s = "Success";
            Response.Redirect("/Home/Main");
        }

        [HttpPost]
        public void UserRegister(user_request req)
        {

            ctx.user_request.Add(req);
            ctx.SaveChanges();
            List<user_request> list = ctx.user_request.ToList();
            Response.Redirect("/Home/Main");
        }
        public ActionResult UserRegistration()
        {
            if (Session["Admin"] != null)
            {
            IEnumerable<user_request> list = ctx.user_request.ToList();
            return View(list);
                }
            else
                return RedirectToAction("/Index");
        }
        [HttpPost]
        public void validateSignIn(admin admin)
        {
            if (ctx.admins.Any(x => x.username.Equals(admin.username) && x.passwod.Equals(admin.passwod)))
            {
                Session["Admin"] = "Admin";
                Response.Redirect("/Admin/Main");
               
            }
            else
            {
                
                
                Response.Redirect("/Admin/Index");
              
            }
        }
    
    public ActionResult Main()
        {
        if (Session["Admin"] != null)
            {
            return View();
            }
            else
                return RedirectToAction("/Index");
        }

    public ActionResult DeleteUser(int id)
        {
        if (Session["Admin"] != null)
        {
            user user = ctx.users.First(x => x.Id.Equals(id));
            ctx.users.Remove(user);
            ctx.SaveChanges();
            return RedirectToAction("userview");
        }
        else
            return RedirectToAction("Index");
        }
    public ActionResult DeleteService(int id)
    {
        if (Session["Admin"] != null)
        {
            service ser = ctx.services.First(x => x.Id.Equals(id));
            ctx.services.Remove(ser);
            ctx.SaveChanges();
            return RedirectToAction("viewService");
        }
        else
            return RedirectToAction("Index");
    }
    public ActionResult DeletePromotion(int id)
    {
        if (Session["Admin"] != null)
        {
            promotion pro = ctx.promotions.First(x => x.Id.Equals(id));
            ctx.promotions.Remove(pro);
            ctx.SaveChanges();
            return RedirectToAction("viewPromotion");
        }
        else
            return RedirectToAction("Index");
    }
    public ActionResult DeleteWorker(int id)
    {
        if (Session["Admin"] != null)
        {
            worker_Portfolio worker = ctx.worker_Portfolio.First(x => x.Id.Equals(id));
            ctx.worker_Portfolio.Remove(worker);
            ctx.SaveChanges();
            return RedirectToAction("viewWorker");
        }
        else
            return RedirectToAction("Index");
    }
    public ActionResult EditUser(int id)
    {
        if (Session["Admin"] != null)
        {
            user u = ctx.users.First(x => x.Id.Equals(id));
            return View(u);
        }
        else
            return RedirectToAction("/Index");

    }
    public ActionResult EditService(int id)
    {
        if (Session["Admin"] != null)
        {
            service ser = ctx.services.First(x => x.Id.Equals(id));
            return View(ser);
        }
        else
            return RedirectToAction("/Index");

    }
    public ActionResult EditPromotion(int id)
    {
        if (Session["Admin"] != null)
        {
             promotion pro = ctx.promotions.First(x => x.Id.Equals(id));
            return View(pro);
        }
        else
            return RedirectToAction("/Index");

    }
    public ActionResult EditWorker(int id)
    {
        if (Session["Admin"] != null)
        {
            worker_Portfolio worker = ctx.worker_Portfolio.First(x => x.Id.Equals(id));
            return View(worker);
        }
        else
            return RedirectToAction("/Index");

    }
    public ActionResult EditUserinDb(user User)
    {
        if (Session["Admin"] != null)
        {
            user u = ctx.users.FirstOrDefault(x => x.Id == User.Id);
            u.cnic = User.cnic;
            u.contact = User.contact;
            u.address = User.address;
            ctx.SaveChanges();
            return RedirectToAction("userview");
        }
        else
            return RedirectToAction("Index");
    }
    public ActionResult EditServiceinDb(service Service)
    {
        if (Session["Admin"] != null)
        {
            service ser = ctx.services.FirstOrDefault(x => x.Id == Service.Id);
            ser.service_type = Service.service_type;
            ser.no_of_workers = Service.no_of_workers;
            ser.description = Service.description;
            ctx.SaveChanges();
            return RedirectToAction("viewService");
        }
        else
            return RedirectToAction("Index");
    }
    public ActionResult EditPromotioninDb(promotion Promotion)
    {
        if (Session["Admin"] != null)
        {
            promotion pro = ctx.promotions.FirstOrDefault(x => x.Id == Promotion.Id);
            pro.subject = Promotion.subject;
            pro.description = Promotion.description;
            ctx.SaveChanges();
            return RedirectToAction("viewPromotion");
        }
        else
            return RedirectToAction("Index");
    }
    public ActionResult EditWorkerinDb(worker_Portfolio worker)
    {
        if (Session["Admin"] != null)
        {
            worker_Portfolio w = ctx.worker_Portfolio.FirstOrDefault(x => x.Id == worker.Id);
            w.name = worker.name;
            w.cnic = worker.cnic;
            w.contact1 = worker.contact1;
            w.contact2 = worker.contact2;
            w.address = worker.address;
            w.experience_ = worker.experience_;
            //ctx.Entry(worker) = System.Data.Entity.EntityState.Modified;
            ctx.SaveChanges();
            return RedirectToAction("viewWorker");
        }
        else 
            return RedirectToAction("Index");

    }
        public ActionResult ServiceRequests()
    {
            if(Session["Admin"]!=null)
            {
                List<request> req = ctx.requests.ToList();
                
            }
        return View();
    }
    public ActionResult userview(string users="")
    {
        if (Session["Admin"] != null)
        {
            if (users == "" || users == " ")
            {
                List<user> list = ctx.users.ToList();
                return View(list);
            }
            else
            {
                List<user> list = ctx.users.Where(x => x.username.Contains(users.ToLower()) || x.cnic == users || x.contact == users || x.address == users).ToList();
                return View(list);
            }
        }
        else
            return RedirectToAction("/Index");
    }

        public ActionResult changeSlot(int id , int wid)
        {
          Availability_Slots slot=  ctx.Availability_Slots.FirstOrDefault(obj => obj.Id == id);
            
            if(slot.IsAvailable == 1)
                slot.IsAvailable = 0;
            else if(slot.IsAvailable == 0)
                slot.IsAvailable = 1;
            ctx.SaveChanges();

            return RedirectToAction("EditWorker/"+wid);
        }

        public ActionResult addCategory()
        {
            if (Session["Admin"] != null)
            {
                return View();
            }
            else
                return RedirectToAction("/Index");
        }

        public void SaveCategory(category c)
        {
            ctx.categories.Add(c);
            ctx.SaveChanges();
            try
            {
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Response.Redirect("/Admin/Main");
        }

        public ActionResult viewCategory()
        {
            if (Session["Admin"] != null)
            {
                List<category> list = ctx.categories.ToList();
                return View(list);
            }
            else
                return RedirectToAction("/Index");
        }


        public ActionResult EditCategory(int id)
        {
            if (Session["Admin"] != null)
            {
                category cate = ctx.categories.First(x => x.Id.Equals(id));
                return View(cate);
            }
            else
                return RedirectToAction("/Index");

        }

        public ActionResult EditCategoryinDb(category cate)
        {
            if (Session["Admin"] != null)
            {
                category c = ctx.categories.FirstOrDefault(x => x.Id == cate.Id);
                c.name = cate.name;
                ctx.SaveChanges();
                return RedirectToAction("viewcategory");
            }
            else
                return RedirectToAction("Index");
        }

        public ActionResult DeleteCategory(int id)
        {
            if (Session["Admin"] != null)
            {
                category c = ctx.categories.First(x => x.Id.Equals(id));
                ctx.categories.Remove(c);
                ctx.SaveChanges();
                return RedirectToAction("viewCategory");
            }
            else
                return RedirectToAction("Index");
        }

        public ActionResult ViewRequest()
        {

            return View(ctx.requests);
        }

        public ActionResult DeleteRequest(int Id)
        {
            request r = ctx.requests.FirstOrDefault(n => n.Id == Id);
            ctx.requests.Remove(r);
            ctx.SaveChanges();
            return RedirectToAction("ViewRequest");
        }


    }

}