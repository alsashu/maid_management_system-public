using MaidManagementSolutions.DataAccessLayer;
using MaidManagementSolutions.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MaidManagementSolutions.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    public class MaidController : Controller
    {
        // GET: MaidController
        public ActionResult Index()
        {
            List<MaidViewModel> maidViewModels = new List<MaidViewModel>();
            var dt = DataAccess.GetDataTable(SqlStatement.getAllMaid);
            if (dt.Rows.Count > 0)
            {

                MaidViewModel model = new MaidViewModel();
                model.Id = Guid.Parse(dt.Rows[0]["id"].ToString());
                model.FirstName = dt.Rows[0]["firstname"].ToString();
                model.LastName = dt.Rows[0]["lastname"].ToString();
                model.ContactNumber = dt.Rows[0]["contactnumber"].ToString();
                model.About = dt.Rows[0]["about"].ToString();
                model.Address = dt.Rows[0]["address"].ToString();
                model.Age = Convert.ToInt32(dt.Rows[0]["Age"]);
                model.AvailableFrom = Convert.ToDateTime(dt.Rows[0]["availablefrom"]);
                model.City = dt.Rows[0]["city"].ToString();
                model.Community = dt.Rows[0]["community"].ToString();
                model.State = dt.Rows[0]["state"].ToString();
                model.City = dt.Rows[0]["city"].ToString();
                model.Location = dt.Rows[0]["location"].ToString();
                model.Pincode = dt.Rows[0]["pincode"].ToString();
                model.Eduction = dt.Rows[0]["eduction"].ToString();
                model.AvailableFrom = Convert.ToDateTime(dt.Rows[0]["availablefrom"]);
                model.ExperienceInYears = Convert.ToInt32(dt.Rows[0]["experienceinyears"]);
                model.ExperienceInMonths = Convert.ToInt32(dt.Rows[0]["experienceinmonths"]);
                model.PreviousEmployerName = dt.Rows[0]["previousemployername"].ToString();
                model.PreviousEmployerContactNumber = dt.Rows[0]["previousemployercontactnumber"].ToString();

                maidViewModels.Add(model);
            }
            return View(maidViewModels);
        }

        // GET: MaidController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MaidController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MaidController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MaidController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MaidController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MaidController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MaidController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
