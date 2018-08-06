using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using WebApplication1.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.PlatformAbstractions;
using System.Runtime.Serialization.Json;
using System.Text;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public LoginController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login LSM)
        {
            if (LSM.UserName == "admin")
            {
                if (LSM.Password == "admin")
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    ModelState.AddModelError("Password", "Password does not Matched");
                }
            }
            else { ModelState.AddModelError("UserName", "Username does not Matched"); }
            return View();
        }
        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(LoginSignupModel LSM)
        {
            try
            {
                string webRootPath = _hostingEnvironment.WebRootPath;
                string contentRootPath = _hostingEnvironment.ContentRootPath;
                string Filename = (contentRootPath) + "\\App_Data\\Database.json";
                dynamic jsondata = String.Empty;
                using (StreamReader r = new StreamReader(Filename, Encoding.Default))
                {
                    dynamic json = r.ReadToEnd();
                    r.Close();
                    List<LoginSignupModel> UserList = JsonConvert.DeserializeObject<List<LoginSignupModel>>(json);
                    //LSM.Id=UserList.Count + 1;
                    int IndexNumber = UserList.Max(t => t.Id);
                 //   int IndexNumber = UserList.FindLastIndex(x => x.Id ==UserList.FindIndex.[id]);

                    LSM.Id =IndexNumber + 1;

                    UserList.Add(LSM);
                    jsondata = JsonConvert.SerializeObject(UserList, Formatting.None);
                    System.IO.File.WriteAllText(Filename, jsondata);
                    return RedirectToAction("Index");
                }
            }catch(Exception e) { e.ToString(); }
            return View();
        }

        [HttpGet]
        public IActionResult UserEdit(int id)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            string Filename = (contentRootPath) + "\\App_Data\\Database.json";
            using (StreamReader r = new StreamReader(Filename))
            {
                dynamic json = r.ReadToEnd();
                List<LoginSignupModel> UserList = JsonConvert.DeserializeObject<List<LoginSignupModel>>(json);
                if (UserList.Count != 0)
                {
                    int IndexNumber = UserList.FindIndex(x => x.Id == id);
                    return View(UserList[IndexNumber]);
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult UserEdit(LoginSignupModel LSM)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            string Filename = (contentRootPath) + "\\App_Data\\Database.json";
            dynamic jsondata = String.Empty;
            StreamReader r = new StreamReader(Filename);
            dynamic json = r.ReadToEnd();
            r.Close();
            List<LoginSignupModel> UserList = JsonConvert.DeserializeObject<List<LoginSignupModel>>(json);
            dynamic _des = JArray.Parse(json);
            foreach (var _obj in _des)
            {
                if (_obj.Id == LSM.Id)
                {
                    //UserList.ElementAt(LSM.Id - 1);
                    int IndexNumber = UserList.FindIndex(x => x.Id == LSM.Id);
                    LoginSignupModel UpdateData = new LoginSignupModel();
                    UpdateData.UserName = LSM.UserName;
                    UpdateData.Id = LSM.Id;
                    UpdateData.Password = LSM.Password;
                    UpdateData.FirstName = LSM.FirstName;
                    UpdateData.SureName = LSM.SureName;
                    UpdateData.MiddleName = LSM.MiddleName;
                    UpdateData.Qualification = LSM.Qualification;
                    UpdateData.Address = LSM.Address;
                    UpdateData.Contact = LSM.Contact;
                    UpdateData.Department = LSM.Department;
                    UpdateData.Email = LSM.Email;

                    UserList[IndexNumber]=UpdateData;
                }

            }
            jsondata = JsonConvert.SerializeObject(UserList, Formatting.None);
            System.IO.File.WriteAllText(Filename, jsondata);
            return RedirectToAction("Index");
            //System.IO.File.WriteAllText((contentRootPath) + "\\App_Data\\Database.json", json);

            return View();
        }

        [HttpGet]
        public ActionResult UserDetails(int id)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            string Filename = (contentRootPath) + "\\App_Data\\Database.json";
            using (StreamReader r = new StreamReader(Filename))
            {
                var json = r.ReadToEnd();
                r.Close();
                List<LoginSignupModel> UserList = JsonConvert.DeserializeObject<List<LoginSignupModel>>(json);
                
                    LoginSignupModel LSM = new LoginSignupModel();
                    int IndexNumber = UserList.FindIndex(x => x.Id == id);
                    return View(UserList[IndexNumber]);
                
            }
            return View();
        }
        [HttpGet]
        public IActionResult Index()
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            string Filename = (contentRootPath) + "\\App_Data\\Database.json";
            using (StreamReader r = new StreamReader(Filename))
            {
                var json = r.ReadToEnd();
                List<LoginSignupModel> UserList = JsonConvert.DeserializeObject<List<LoginSignupModel>>(json);
                if (UserList.Count != 0)
                { return View(UserList); }
            }
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginSignupModel LSM)
        {
            return UserEdit(LSM.Id);
        }
        [HttpGet]
        public ActionResult DeleteUser(int id)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            string Filename = (contentRootPath) + "\\App_Data\\Database.json";
            using (StreamReader r = new StreamReader(Filename))
            {
                var json = r.ReadToEnd();
                List<LoginSignupModel> UserList = JsonConvert.DeserializeObject<List<LoginSignupModel>>(json);
                int IndexNumber = UserList.FindIndex(x => x.Id == id);
                return View(UserList[IndexNumber]);
            }
        }

        [HttpPost]
        
        public ActionResult DeleteUser(LoginSignupModel LSM)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            string Filename = (contentRootPath) + "\\App_Data\\Database.json";
            dynamic jsondata = String.Empty;
            using (StreamReader r = new StreamReader(Filename))
            {
                var json = r.ReadToEnd();
                r.Close();
                List<LoginSignupModel> UserList = JsonConvert.DeserializeObject<List<LoginSignupModel>>(json);
                dynamic _des = JArray.Parse(json);

                foreach (var _obj in _des)
                {
                    if (_obj.Id == LSM.Id)
                    {
                        int IndexNumber = UserList.FindIndex(x => x.Id == LSM.Id);
                        UserList.RemoveAt(IndexNumber);
                    }  
                }
                jsondata = JsonConvert.SerializeObject(UserList, Formatting.None);
                System.IO.File.WriteAllText(Filename, jsondata);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddUser(LoginSignupModel LSM)
        {
            return View();
        }

    }
}