using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YammerDailyPosterBLL;
using YammerDailyPosterBLL.Model;

namespace YammerDailyPosterWeb.Controllers
{
    public class DailyMessageController : Controller
    {

        // GET: DailyMessage
        public ActionResult Index()
        {
            PostMessager pm = new PostMessager();
            List<MessageInfo> data = pm.GetData().Items;
            if (data.Count > 0)
                return View(data);
            else
                return RedirectToAction("Create");
        }

        // GET: DailyMessage/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DailyMessage/Create
        public ActionResult Create()
        {
            MessageInfo item = new MessageInfo() { Schedule = DateTime.Now };
            return View(item);
        }

        // POST: DailyMessage/Create
        [HttpPost]
        public ActionResult Create(MessageInfo item)
        {
            try
            {
                PostMessager pm = new PostMessager();
                pm.AddNew(item.Schedule, item.Subject, item.Context);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DailyMessage/Edit/5
        public ActionResult Edit(string id)
        {
            PostMessager pm = new PostMessager();
            MessageInfo data = pm.GetItem( Guid.Parse(id));
            return View(data);
        }

        // POST: DailyMessage/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, MessageInfo item)
        {
            try
            {
                PostMessager pm = new PostMessager();
                pm.UpdateItem(Guid.Parse(id), item.Schedule, item.Subject, item.Context);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DailyMessage/Delete/5
        public ActionResult Delete(string id)
        {
            PostMessager pm = new PostMessager();
            pm.DeleteItem(Guid.Parse(id));
            return RedirectToAction("Index");
        }

        // POST: DailyMessage/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
