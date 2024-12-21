using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using NETCore.Encrypt.Extensions;
using System.Security.Claims;
using OnlineSinavPortali.Models;
using OnlineSinavPortali.ViewModels;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace OnlineSinavPortali.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly INotyfService _notify;
        private readonly IConfiguration _config;
        private readonly IFileProvider _fileProvider;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;


        public HomeController(ILogger<HomeController> logger, IConfiguration config, AppDbContext context, INotyfService notify, IFileProvider fileProvider, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager)
        {

            _context = context;
            _config = config;
            _notify = notify;
            _logger = logger;
            _fileProvider = fileProvider;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
            
        public async Task<IActionResult> UserPage()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null) return RedirectToAction("Login");
           
            var userRoles = await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(userId));

            var currentUser = await _userManager.FindByIdAsync(userId);
           


            var userModel = new UserModel()
            {
               
                UniversityDepartment =currentUser.UniversityDepartment,
                CreatedAt = currentUser.CreatedAt,
                FullName = currentUser.FullName,
                Email = currentUser.Email,
                PhotoUrl = currentUser.PhotoUrl,
                Role = userRoles.Any() ? userRoles.First() : "Student",
                StudentNumber = currentUser.StudentNumber
            };

            
            return View(userModel);
        }
        [HttpPost]
        public async Task<IActionResult> UserPage(UserModel model)
        {
            var rootFolder = _fileProvider.GetDirectoryContents("wwwroot");
            var photoUrl = "-";
            if (model.PhotoFile.Length > 0 && model.PhotoFile != null)
            {
                var filename = Guid.NewGuid().ToString() + Path.GetExtension(model.PhotoFile.FileName);
                var photoPath = Path.Combine(rootFolder.First(x => x.Name == "Photos").PhysicalPath, filename);
                using var stream = new FileStream(photoPath, FileMode.Create);
                model.PhotoFile.CopyTo(stream);
                photoUrl = filename;

                var userId = _userManager.GetUserId(User);


                // Kullanıcıyı ID'siyle veri tabanından çek
                var user = await _userManager.FindByIdAsync(userId);

                if (user != null)
                {
                    // Kullanıcının PhotoUrl özelliğini güncelle
                    user.PhotoUrl = photoUrl;

                    // Kullanıcıyı veri tabanında güncelle
                    var updateResult = await _userManager.UpdateAsync(user);

                    if (updateResult.Succeeded)
                    {
                        
                        _notify.Success("Fotoğrafınız güncellendi");
                        
                        return RedirectToAction("UserPage");
                    }


                }
            }
            return RedirectToAction("Userpage");
        }


        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {

            var user = await _userManager.FindByNameAsync(model.Username);


            

            if (user == null)
            {
                // Kullanıcı adı ile bulunamazsa öğrenci numarası ile tekrar dene
                user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == model.Username);
   
            }

            if (user != null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);

                if (signInResult.Succeeded)
                {
                    
                    _notify.Success("Giriş Yapıldı.");
                    return RedirectToAction("Index");
                }

                if (signInResult.IsLockedOut)
                {
                    ModelState.AddModelError("", "Kullanıcı Girişi " + user.LockoutEnd + " kadar kısıtlanmıştır!");
                    return View();
                }
                
                ModelState.AddModelError("", "Geçersiz Kullanıcı Adı veya Parola Başarısız Giriş Sayısı :" + await _userManager.GetAccessFailedCountAsync(user) + "/4");
                return View();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Geçersiz Öğrenci Numarası veya Parola!");
                return View(model);
            }
        }

        public async Task<IActionResult> GetUserList()
        {

            
            
            var userModels = await _userManager.Users.Select(x => new UserModel()
            {
                Id = x.Id,
                FullName = x.FullName,
                Email = x.Email,
                Username = x.UserName,
                StudentNumber = x.StudentNumber,
                //Role = _roleManager.GetRoleIdAsync(Id)
            }).ToListAsync();

            return View(userModels);
        }
        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult StudentRegister()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> StudentRegister(StudentRegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);

                foreach (var error in errors)
                {
                    var errorMessage = error.ErrorMessage;
                    // veya
                    var exception = error.Exception;
                    if (exception != null)
                    {
                        Console.WriteLine(exception.ToString());
                    }



                }
                return View(model);

            }
            var existingStudent = await _context.Users
              .FirstOrDefaultAsync(s => s.StudentNumber == model.StudentNumber);
            if (!model.Email.Contains("@ogr.akdeniz.edu.tr"))
            {
                _notify.Error(model.Email + " hatalı mail");
                return View(model);
            }
            if (existingStudent != null)
            {
                ModelState.AddModelError("StudentNumber", "Bu öğrenci numarası zaten kullanılmaktadır.");
                return View(model);
            }
            string universityDepartment = "";
            string substring45 = model.StudentNumber.Substring(6, 2);
            if (substring45 == "29")
            {
                universityDepartment = "Bilgisayar Programcılığı";
            }
            else if (substring45 == "30")
            {
                universityDepartment = "Elektrik Elektronik";
            }
            else if (substring45 == "31")
            {
                universityDepartment = "Harita Ve Kadastro";
            }
            string photoUrl = "NoUser.jpg";
            var createdAt = DateTime.Now;
            var identityResult = await _userManager.CreateAsync(new() { UserName = model.Username,PhotoUrl = photoUrl,UniversityDepartment = universityDepartment, StudentNumber= model.StudentNumber, Email = model.Email, FullName = model.FullName, CreatedAt = createdAt }, model.Password);

            if (!identityResult.Succeeded)
            {
                foreach (var item in identityResult.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

                return View(model);
            }

            // default olarak Uye rolü ekleme
            var user = await _userManager.FindByNameAsync(model.Username);
            var roleExist = await _roleManager.RoleExistsAsync("Student");
            if (!roleExist)
            {
                var role = new AppRole { Name = "Student" };
                await _roleManager.CreateAsync(role);
            }

            await _userManager.AddToRoleAsync(user, "Student");

            _notify.Success("Kayıt Başarılı Şekilde Yapıldı.");
            return RedirectToAction("Login");
        }
        public IActionResult TeacherRegister()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TeacherRegister(TeacherRegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);

                foreach (var error in errors)
                {
                    var errorMessage = error.ErrorMessage;
                    // veya
                    var exception = error.Exception;
                    if (exception != null)
                    {
                        Console.WriteLine(exception.ToString());
                    }



                }
                return View(model);

            }
            if (!model.Email.Contains("@akdeniz.edu.tr")){
                _notify.Error(model.Email+" hatalı mail");
                return View(model);
            }
            var photoUrl = "NoUser.jpg"; 
            var createdAt = DateTime.Now;
            var identityResult = await _userManager.CreateAsync(new() { UserName = model.Username, Email = model.Email, FullName = model.FullName, CreatedAt = createdAt,PhotoUrl = photoUrl }, model.Password);

            if (!identityResult.Succeeded)
            {
                foreach (var item in identityResult.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

                return View(model);
            }

            
            var user = await _userManager.FindByNameAsync(model.Username);
            var roleExist = await _roleManager.RoleExistsAsync("Teacher");
            if (!roleExist)
            {
                var role = new AppRole { Name = "Teacher" };
                await _roleManager.CreateAsync(role);
            }

            await _userManager.AddToRoleAsync(user, "Teacher");

            _notify.Success("Giriş Başarılı Şekilde Yapıldı.");
            return RedirectToAction("Login");
        }
        public IActionResult Register()
        {

            return View();
        }
        
        public IActionResult Contact()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Contact(IletisimFormuModel model)
        {
            return RedirectToAction("Concact");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _notify.Success("Başarıyla çıkış yapıldı.");
            return RedirectToAction("Index");
        }


        public IActionResult RoleAdd()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RoleAdd(AppRole model)
        {
            var role = await _roleManager.FindByNameAsync(model.Name);
            if (role == null)
            {

                var newrole = new AppRole();
                newrole.Name = model.Name; ;
                await _roleManager.CreateAsync(newrole);
            }
            return RedirectToAction("Index");
        }

    }
}