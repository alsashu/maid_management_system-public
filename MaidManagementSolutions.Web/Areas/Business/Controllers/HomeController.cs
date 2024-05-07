using Humanizer.Bytes;
using MaidManagementSolutions.DataAccessLayer;
using MaidManagementSolutions.ViewModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using System.Data;
using System.Net;
using System.Reflection;
using System.Text;

namespace MaidManagementSolutions.Web.Areas.Business.Controllers
{
    [Area("business")]
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public HomeController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        // GET: HomeController
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        [ActionName("findmaid")]
        public ActionResult FindMaid()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FindMaid(CompositeViewModel model)
        {
            return View();
        }

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        [ActionName("maiddetails")]
        public ActionResult MaidDetails(string id)
        {
            var param = new
            {
                @id = id
            };
            var dt = DataAccess.GetDataTable(SqlStatement.getMaidById, param);

            if (dt.Rows.Count > 0)
            {
                string imageBase64 = Convert.ToBase64String(dt.Rows[0]["filedata"] as byte[]);
                ViewBag.ImageBase64 = imageBase64;

                string[] arr_language = JsonConvert.DeserializeObject<string[]>(dt.Rows[0]["languages"].ToString());
                ViewBag.LanguageAvailable = string.Join(", ", arr_language);

                string[] arr_services = JsonConvert.DeserializeObject<string[]>(dt.Rows[0]["services"].ToString());
                ViewBag.ServicesAvailable = string.Join(", ", arr_services);

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
                model.ExperienceInYears = Convert.ToInt32(dt.Rows[0]["experienceinyears"]);
                model.ExperienceInMonths = Convert.ToInt32(dt.Rows[0]["experienceinmonths"]);
                model.PreviousEmployerName = dt.Rows[0]["previousemployername"].ToString();
                model.PreviousEmployerContactNumber = dt.Rows[0]["previousemployercontactnumber"].ToString();
                model.PreferredLocation = JsonConvert.DeserializeObject<string[]>(dt.Rows[0]["location"].ToString());
                model.Availability = Convert.ToDateTime(dt.Rows[0]["availablefrom"]).ToString("MM/dd/yyyy");
                //model.Language= 
                return View(model);
            }

            return View();
        }
        [ActionName("result")]
        public ActionResult Results()
        {
            return View();
        }
        [ActionName("userprofile")]
        public ActionResult UserProfile()
        {
            return View();
        }
        [ActionName("maidregistration")]
        public ActionResult MaidRegistration()
        {
            var compositeViewModel = new CompositeViewModel
            {
                MaidViewModel = new MaidViewModel(),
                Document1 = new Documents(),
                Document2 = new Documents()
            };
            compositeViewModel.MaidViewModel = new MaidViewModel
            {
                LanguageList = new SelectList(Enum.GetValues(typeof(LanguageEnum))),
                GenderList = new SelectList(Enum.GetValues(typeof(GenderEnum))),
                WorkingHoursList = new SelectList(Enum.GetValues(typeof(WorkingHoursEnum))),
                MaritalStatusList = new SelectList(Enum.GetValues(typeof(MaritalStatusEnum))),
                ServicesList = new SelectList(Enum.GetValues(typeof(ServicesEnum))),
            };

            return View(compositeViewModel);
        }
        // POST: Maid/MaidRegistration
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MaidRegistration(CompositeViewModel model)
        {
            string gender = (string)model.MaidViewModel.SelectedGender.ToString();
            string workingHours = (string)model.MaidViewModel.SelectedWorkingHours.ToString();
            string maritalStatus = (string)model.MaidViewModel.SelectedMaritalStatus.ToString();

            List<string> languages = new List<string>();
            foreach (var item in model.MaidViewModel.SelectedLanguage)
            {
                languages.Add(item.ToString());
            }

            List<string> services = new List<string>();
            foreach (var item in model.MaidViewModel.SelectedServices)
            {
                services.Add(item.ToString());
            }
            try
            {
                if (ModelState.IsValid)
                {
                    if (model != null)
                    {
                        var paramDoc = new
                        {
                            @filename = model.MaidViewModel.Id.ToString()
                        };
                        var docDt = DataAccess.GetDataTable(SqlStatement.getDocumentByFileName, paramDoc);
                        var param = new
                        {
                            @id = model.MaidViewModel.Id,
                            @firstname = model.MaidViewModel.FirstName,
                            @lastname = model.MaidViewModel.LastName,
                            @contactnumber = model.MaidViewModel.ContactNumber,
                            @email = string.Empty,
                            @address = model.MaidViewModel.Address,
                            @countryid = model.MaidViewModel.CountryId,
                            @stateid = model.MaidViewModel.StateId,
                            @state = model.MaidViewModel.State,
                            @city = model.MaidViewModel.City,
                            @community = model.MaidViewModel.Community,
                            @location = model.MaidViewModel.Location,
                            @about = model.MaidViewModel.About,
                            @pincode = model.MaidViewModel.Pincode,
                            @isverified = model.MaidViewModel.IsVerified,
                            @isblocklisted = false,
                            @createdby = "Ashu",
                            @createddate = DateTime.Now,
                            @modifiedby = "Ashu",
                            @modifieddate = DateTime.Now,
                            @eduction = model.MaidViewModel.Eduction,
                            @age = model.MaidViewModel.Age,
                            @availablefrom = model.MaidViewModel.AvailableFrom,
                            @experienceinyears = model.MaidViewModel.ExperienceInYears,
                            @experienceinmonths = model.MaidViewModel.ExperienceInMonths,
                            @previousemployername = model.MaidViewModel.PreviousEmployerName,
                            @previousemployercontactnumber = model.MaidViewModel.PreviousEmployerContactNumber,
                            @gender = gender,
                            @workinghours = workingHours,
                            @maritalstatus = maritalStatus,
                            @languages = JsonConvert.SerializeObject(languages),
                            @services = JsonConvert.SerializeObject(services),
                        };

                        var res = DataAccess.ExecuteScalar(SqlStatement.addMaid, param);
                        TempData.Clear();
                        //lstMaidViewModels.Add(model);
                        return RedirectToAction(nameof(Index));
                    }
                }
                return RedirectToAction(nameof(MaidRegistration));
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        // POST: HomeController/Create
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
        [HttpPost]
        public IActionResult UploadPDF(string sid)
        {
            var file = Request.Form.Files[0];

            if (file != null)
            {
                if (IsValidFile(file.FileName))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        file.CopyTo(memoryStream);
                        byte[] byteData = memoryStream.ToArray();
                        var doc_id = Guid.NewGuid();
                        var param = new
                        {
                            @id = doc_id,
                            @filename = sid,
                            @filedata = byteData,
                            @filetype = "identity",
                        };

                        var res = DataAccess.ExecuteScalar(SqlStatement.addDocument, param);
                        return Json(new { success = true, id = doc_id, src = byteData });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Invalid file format" });
                }
            }

            return Json(new { success = false, message = "No file received" });
        }
        [HttpPost]
        public IActionResult UploadImage(string sid)
        {
            var file = Request.Form.Files[0];

            if (file != null)
            {
                if (IsValidImage(file.FileName))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        file.CopyTo(memoryStream);
                        byte[] byteData = memoryStream.ToArray();
                        var doc_id = Guid.NewGuid();
                        var param = new
                        {
                            @id = doc_id,
                            @filename = sid,
                            @filedata = byteData,
                            @filetype = "photo",
                        };
                        var res = DataAccess.ExecuteScalar(SqlStatement.addDocument, param);
                        return Json(new { success = true, id = doc_id, src = byteData });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Invalid file format" });
                }
            }
            return Json(new { success = false, message = "No file received" });
        }
        private bool IsValidFile(string fileName)
        {
            string[] allowedExtensions = { ".pdf", ".jpg", ".png" };
            string fileExtension = Path.GetExtension(fileName).ToLower();
            return allowedExtensions.Contains(fileExtension);
        }
        private bool IsValidImage(string fileName)
        {
            string[] allowedExtensions = { ".jpg", ".png" };
            string fileExtension = Path.GetExtension(fileName).ToLower();
            return allowedExtensions.Contains(fileExtension);
        }

        private List<MaidViewModel> MaidViewConversion(DataTable dataTable)
        {
            List<MaidViewModel> myList = new List<MaidViewModel>();
            try
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    MaidViewModel item = new MaidViewModel();
                    item.FirstName = row["firstname"].ToString();
                    item.LastName = row["lastname"].ToString();
                    item.ContactNumber = row["contactnumber"].ToString();
                    item.About = row["about"].ToString();
                    item.Address = row["address"].ToString();
                    item.Age = Convert.ToInt32(row["Age"]);
                    item.AvailableFrom = Convert.ToDateTime(row["availablefrom"]);
                    item.City = row["city"].ToString();
                    item.Community = row["community"].ToString();
                    item.State = row["state"].ToString();
                    item.City = row["city"].ToString();
                    item.Location = row["location"].ToString();
                    item.Pincode = row["pincode"].ToString();
                    item.Eduction = row["eduction"].ToString();
                    item.AvailableFrom = Convert.ToDateTime(row["availablefrom"]);
                    item.ExperienceInYears = Convert.ToInt32(row["experienceinyears"]);
                    item.ExperienceInMonths = Convert.ToInt32(row["experienceinmonths"]);
                    item.PreviousEmployerName = row["previousemployername"].ToString();
                    item.PreviousEmployerContactNumber = row["previousemployercontactnumber"].ToString();


                    myList.Add(item);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return myList;
        }

        [HttpPost]
        [ActionName("search")]
        public string Search(FindMaid findMaid)
        {
            var dt = new DataTable();
            if (!string.IsNullOrEmpty(findMaid.Community) && !string.IsNullOrEmpty(findMaid.Location))
            {
                var param = new
                {
                    @community = findMaid.Community,
                    @location = findMaid.Location,
                };
                dt = DataAccess.GetDataTable(SqlStatement.filterMaidCommunity.Replace("$COMMUNITY$", findMaid.Community).Replace("$LOCATION$", findMaid.Location));

            }
            else if (!string.IsNullOrEmpty(findMaid.Community))
            {
                var param = new
                {
                    @community = findMaid.Community,
                };
                dt = DataAccess.GetDataTable(SqlStatement.filterMaidCommunity.Replace("$COMMUNITY$", findMaid.Community));
            }
            else if (!string.IsNullOrEmpty(findMaid.Location))
            {
                var param = new
                {
                    @location = findMaid.Location,
                };
                dt = DataAccess.GetDataTable(SqlStatement.filterMaidLocation.Replace("$LOCATION$", findMaid.Location));
            }
            else
            {
                dt = DataAccess.GetDataTable(SqlStatement.getAllMaid);
            }

            if (dt.Rows.Count > 0)
            {
                ResponseObject response = new ResponseObject
                {
                    Count = dt.Rows.Count,
                    Records = dt
                };

                return JsonConvert.SerializeObject(response);
            }

            return null;
        }

        [HttpPost]
        [ActionName("addreview")]
        public string AddReview(ReviewViewModel model)
        {
            if (!string.IsNullOrEmpty(model.Title))
            {
                var param = new
                {
                    @id = model.Id,
                    @maidid = model.MaidId,
                    @clientid = model.ClientId,
                    @rating = model.Rating,
                    @review = model.Review,
                    @reviewdate = DateTime.Now,
                    @reviewedby = model.ReviewedBy,
                    @modifieddate = DateTime.Now,
                    @modifiedby = model.ReviewedBy,
                    @title = model.Title
                };

                DataAccess.ExecuteScalar(SqlStatement.addReview, param);

            }
            else
            {
                var param = new
                {
                    @id = model.Id,
                    @maidid = model.MaidId,
                    @clientid = model.ClientId,
                    @rating = model.Rating,
                    @review = model.Review,
                    @reviewdate = DateTime.Now,
                    @reviewedby = model.ReviewedBy,
                    @modifieddate = DateTime.Now,
                    @modifiedby = model.ReviewedBy,
                    @title = string.Empty
                };

                DataAccess.ExecuteScalar(SqlStatement.addReview, param);
            }

            var param_get = new
            {
                @maidid = model.MaidId
            };
            var dt = DataAccess.GetDataTable(SqlStatement.getAllReview, param_get);

            if (dt.Rows.Count > 0)
            {
                ResponseObject response = new ResponseObject
                {
                    Count = dt.Rows.Count,
                    Records = dt
                };

                return JsonConvert.SerializeObject(response);
            }

            return null;
        }

        [HttpPost]
        [ActionName("reportprofile")]
        public string ReportProfile(ReportProfileViewModel model)
        {
            var param = new
            {
                @id = model.Id,
                @maidid = model.MaidId,
                @clientid = model.ClientId,
                @reportingcomment = model.ReportingComment,
                @reportedon = DateTime.Now
            };

            DataAccess.ExecuteScalar(SqlStatement.addReportProfile, param);

            ResponseObject response = new ResponseObject
            {
                Count = 1,
                Records = "Reported."
            };

            return JsonConvert.SerializeObject(response);
        }



        [HttpGet]
        [ActionName("getreview")]
        public string GetReview(string id)
        {
            var param_get = new
            {
                @maidid = Guid.Parse(id.ToString())
            };
            var dt = DataAccess.GetDataTable(SqlStatement.getAllReview, param_get);

            if (dt.Rows.Count > 0)
            {
                ResponseObject response = new ResponseObject
                {
                    Count = dt.Rows.Count,
                    Records = dt
                };

                return JsonConvert.SerializeObject(response);
            }

            return null;
        }

        [HttpGet]
        [ActionName("getaveragereview")]
        public string GetAverageReview(string id)
        {
            var param = new
            {
                @maidid = Guid.Parse(id.ToString())
            };
            var dt = DataAccess.GetDataTable(SqlStatement.getGroupPercentage, param);

            if (dt.Rows.Count > 0)
            {
                ResponseObject response = new ResponseObject
                {
                    Count = dt.Rows.Count,
                    Records = dt
                };

                return JsonConvert.SerializeObject(response);
            }

            return null;
        }

        [HttpGet]
        [ActionName("getgroupcount")]
        public string GetGroupCount(string id)
        {
            var param_get = new
            {
                @maidid = Guid.Parse(id.ToString())
            };
            var dt = DataAccess.GetDataTable(SqlStatement.getAverageReview, param_get);

            if (dt.Rows.Count > 0)
            {
                ResponseObject response = new ResponseObject
                {
                    Count = dt.Rows.Count,
                    Records = dt
                };

                return JsonConvert.SerializeObject(response);
            }

            return null;
        }

        [ActionName("rereg")]
        public ActionResult ReReg()
        {
            var compositeViewModel = new CompositeViewModel
            {
                MaidViewModel = new MaidViewModel(),
                Document1 = new Documents(),
                Document2 = new Documents()
            };
            compositeViewModel.MaidViewModel = new MaidViewModel
            {
                LanguageList = new SelectList(Enum.GetValues(typeof(LanguageEnum))),
                GenderList = new SelectList(Enum.GetValues(typeof(GenderEnum))),
                WorkingHoursList = new SelectList(Enum.GetValues(typeof(WorkingHoursEnum))),
                MaritalStatusList = new SelectList(Enum.GetValues(typeof(MaritalStatusEnum))),
                ServicesList = new SelectList(Enum.GetValues(typeof(ServicesEnum))),
            };

            return View(compositeViewModel);
        }


        //[HttpGet]
        //[ActionName("sendotp")]
        //public string SendOTP(string contact,string otp)
        //{

        //}

        [HttpPost]
        [ActionName("initialreg")]
        public string InitialReg(MaidViewModel model)
        {
            try
            {
                var checkParam = new
                {
                    @contactnumber = model.ContactNumber,
                };

                var dt = DataAccess.GetDataTable(SqlStatement.isContactAvailable, checkParam);
                if (dt.Rows.Count == 0)
                {
                    Guid id = Guid.NewGuid();
                    var param = new
                    {
                        @id = id,
                        @firstname = model.FirstName,
                        @lastname = model.LastName,
                        @contactnumber = model.ContactNumber,
                        @email = string.Empty,
                        @isverified = false,
                        @isblocklisted = false,
                        @createdby = string.Empty,
                        @createddate = DateTime.Now,
                        @modifiedby = string.Empty,
                        @modifieddate = DateTime.Now,
                        @address = string.Empty,
                        @countryid = 0,
                        @stateid = 0,
                        @state = string.Empty,
                        @city = string.Empty,
                        @community = string.Empty,
                        @location = string.Empty,
                        @about = string.Empty,
                        @pincode = string.Empty,
                        @eduction = string.Empty,
                        @age = 0,
                        @availablefrom = DateTime.Now,
                        @experienceinyears = 0,
                        @experienceinmonths = 0,
                        @previousemployername = string.Empty,
                        @previousemployercontactnumber = string.Empty,
                        @gender = string.Empty,
                        @workinghours = JsonConvert.SerializeObject(string.Empty),
                        @maritalstatus = string.Empty,
                        @languages = JsonConvert.SerializeObject(string.Empty),
                        @services = JsonConvert.SerializeObject(string.Empty),
                    };
                    var res = DataAccess.ExecuteScalar(SqlStatement.addMaid, param);

                    ResponseObject response = new ResponseObject
                    {
                        Count = 1,
                        Records = id.ToString(),
                    };

                    return JsonConvert.SerializeObject(response);
                }
                else
                {
                    ResponseObject response = new ResponseObject
                    {
                        Count = 0,
                        Records = "This contact number is already register, to know more please contact admin. \n +91-9008092627",
                    };
                    return JsonConvert.SerializeObject(response);
                }
            }
            catch (Exception ex)
            {
                ResponseObject response = new ResponseObject
                {
                    Count = -1,
                    Records = ex.Message,
                };
                return JsonConvert.SerializeObject(response);
            }
        }

        [HttpPost]
        [ActionName("regoptionone")]
        public string RegOptionOne(MaidOptionOneViewModel model)
        {
            try
            {
                ResponseObject response = new ResponseObject();

                string[] tempArr = new string[1];

                if (model.Gender == null)
                {
                    model.Gender = string.Empty;
                }

                if (model.Language == null)
                {
                    model.Language = tempArr;
                }

                if (model.WorkingHours == null)
                {
                    model.WorkingHours = tempArr;
                }

                if (model.Services == null)
                {
                    model.Services = tempArr;
                }


                var param = new
                {
                    @modifiedby = string.Empty,
                    @modifieddate = DateTime.Now,
                    @age = model.Age,
                    @experienceinyears = model.Experience,
                    @experienceinmonths = 0,
                    @gender = model.Gender,
                    @workinghours = JsonConvert.SerializeObject(model.WorkingHours),
                    @languages = JsonConvert.SerializeObject(model.Language),
                    @services = JsonConvert.SerializeObject(model.Services),
                    @id = Guid.Parse(model.Id),
                };
                var res = DataAccess.ExecuteScalar(SqlStatement.updateMaidOne, param);


                response.Count = 1;
                response.Records = model.Id;

                return JsonConvert.SerializeObject(response);
            }
            catch (Exception ex)
            {
                ResponseObject response = new ResponseObject
                {
                    Count = -1,
                    Records = ex.Message,
                };
                return JsonConvert.SerializeObject(response);
            }
        }

        [HttpPost]
        [ActionName("regoptiontwo")]
        public string RegOptionTwo(MaidOptionTwoViewModel model)
        {
            try
            {
                CheckPhoto(model.Id);

                string[] tempArr = new string[1];

                if (model.About == null)
                {
                    model.About = string.Empty;
                }

                if (model.City == null)
                {
                    model.City = string.Empty;
                }

                if (model.Community == null)
                {
                    model.Community = string.Empty;
                }

                if (model.Location == null)
                {
                    model.Location = string.Empty;
                }

                if (model.PinCode == null)
                {
                    model.PinCode = string.Empty;
                }

                if (model.PreferredLocation == null)
                {
                    model.PreferredLocation = tempArr;
                }

                if (model.State == null)
                {
                    model.State = string.Empty;
                }

                var param = new
                {
                    @state = model.State,
                    @city = model.City,
                    @community = model.Community,
                    @location = JsonConvert.SerializeObject(model.PreferredLocation),
                    @pincode = model.PinCode,
                    @modifiedby = "",
                    @modifieddate = DateTime.Now,
                    @availablefrom = model.IsAvailable ? DateTime.Now : model.AvailableFrom,
                    @id = Guid.Parse(model.Id),
                    @about = model.About,
                    @address = model.Location
                };
                var res = DataAccess.ExecuteScalar(SqlStatement.updateMaidTwo, param);

                ResponseObject response = new ResponseObject
                {
                    Count = 1,
                    Records = model.Id,
                };
                return JsonConvert.SerializeObject(response);
            }
            catch (Exception ex)
            {
                ResponseObject response = new ResponseObject
                {
                    Count = -1,
                    Records = ex.Message,
                };
                return JsonConvert.SerializeObject(response);
            }
        }

        [HttpPost]
        [ActionName("addemployer")]
        public string AddEmployer(EmployerViewModel model)
        {
            List<EmployerViewModel> listEmployer = new List<EmployerViewModel>();
            try
            {
                var paramget = new
                {
                    @id = Guid.Parse(model.Id),
                };

                var dt = DataAccess.GetDataTable(SqlStatement.getOnlyMaidById, paramget);

                if (!string.IsNullOrEmpty(dt.Rows[0]["previousemployer"].ToString()))
                {
                    List<EmployerViewModel> employerViewModels = JsonConvert.DeserializeObject<List<EmployerViewModel>>(dt.Rows[0]["previousemployer"].ToString());
                    foreach (var item in employerViewModels)
                    {
                        listEmployer.Add(item);
                    }
                }

                listEmployer.Add(model);

                var param = new
                {
                    @previousemployer = JsonConvert.SerializeObject(listEmployer),
                    @id = Guid.Parse(model.Id),
                };
                var res = DataAccess.ExecuteScalar(SqlStatement.addEmployer, param);

                ResponseObject response = new ResponseObject
                {
                    Count = 1,
                    Records = listEmployer,
                };
                return JsonConvert.SerializeObject(response);

            }
            catch (Exception ex)
            {
                ResponseObject response = new ResponseObject
                {
                    Count = -1,
                    Records = ex.Message,
                };
                return JsonConvert.SerializeObject(response);
            }
        }

        [HttpGet]
        [ActionName("addsuggestion")]
        public string AddSuggestion(string sugg)
        {
            try
            {
                if (!string.IsNullOrEmpty(sugg))
                {
                    var param = new
                    {
                        @id = Guid.NewGuid(),
                        @custsuggestion = sugg
                    };
                    var res = DataAccess.ExecuteScalar(SqlStatement.addcustsuggestion, param);

                    ResponseObject response = new ResponseObject
                    {
                        Count = 1,
                        Records = "Thanks for your valuable suggestion.",
                    };
                    return JsonConvert.SerializeObject(response);
                }

                return JsonConvert.SerializeObject(new ResponseObject
                {
                    Count = 0,
                    Records = "no value added.",
                });
            }
            catch (Exception ex)
            {
                ResponseObject response = new ResponseObject
                {
                    Count = -1,
                    Records = ex.Message,
                };
                return JsonConvert.SerializeObject(response);
            }
        }

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        [ActionName("searchmaid")]
        public ActionResult SearchMaid()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchMaid(CompositeViewModel model)
        {
            return View();
        }

        [HttpPost]
        [ActionName("searchworker")]
        public string SearchWorker(FindMaid findMaid)
        {
            string query = "SELECT * FROM public.maid as md inner join public.documents as doc on md.id=CAST(doc.filename AS UUID) where doc.filetype='photo'";

            //string query = "SELECT * FROM public.maid as md inner join public.documents as doc on md.id=CAST(doc.filename AS UUID) inner join public.review as rv on md.id = rv.maidid where doc.filetype='photo'";


            if (!string.IsNullOrEmpty(findMaid.Location))
            {
                query = query + " and location ilike '%" + findMaid.Location + "%'";
            }

            if (findMaid.Services != null)
            {
                foreach (var item in findMaid.Services)
                {
                    query = query + " and services  ilike '%" + item + "%'";
                }
            }

            if (findMaid.Language != null)
            {
                foreach (var item in findMaid.Language)
                {
                    query = query + " and languages ilike '%" + item + "%'";
                }
            }

            if (findMaid.Working != null)
            {
                foreach (var item in findMaid.Working)
                {
                    query = query + " and workinghours ilike '%" + item + "%'";
                }
            }

            query = query + ";";

            var dt = new DataTable();

            dt = DataAccess.GetDataTable(query);


            if (dt.Rows.Count > 0)
            {
                ResponseObject response = new ResponseObject
                {
                    Count = dt.Rows.Count,
                    Records = dt
                };

                return JsonConvert.SerializeObject(response);
            }

            return null;
        }

        private void CheckPhoto(string id)
        {
            var param = new
            {
                @filename = id
            };
            var dt = DataAccess.GetDataTable(SqlStatement.getDocumentByFileName, param);

            if (dt.Rows.Count == 0)
            {
                UploadDefaultImage(id);
            }
        }

        private void UploadDefaultImage(string sid)
        {
            var dt = DataAccess.GetDataTable(SqlStatement.getDefaultFile);
            var doc_id = Guid.NewGuid();
            var param = new
            {
                @id = doc_id,
                @filename = sid,
                @filedata = dt.Rows[0]["filedata"],
                @filetype = "photo",
            };
            DataAccess.ExecuteScalar(SqlStatement.addDocument, param);

        }
    }
}
