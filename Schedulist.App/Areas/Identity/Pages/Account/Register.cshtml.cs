// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Schedulist.DAL;
using Schedulist.DAL.Models;
using Schedulist.DAL.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;

namespace Schedulist.App.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IUserStore<User> _userStore;
        private readonly IUserEmailStore<User> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IPositionRepository _positiontRepository;
        //private readonly SchedulistDbContext _db { get; set; }

        public RegisterModel(
            UserManager<User> userManager,
            IUserStore<User> userStore,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender, RoleManager<IdentityRole> roleManager, SchedulistDbContext db,
            IDepartmentRepository departmentRepository, IPositionRepository positionRepository)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _departmentRepository = departmentRepository;
            _positiontRepository = positionRepository;
            //_db = db;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            //[ValidateNever]
            //public IEnumerable<SelectListItem> RoleList { get; set; }
            //public string? Role { get; set; }
            [Required]
            [StringLength(100, ErrorMessage = "Name must be minimum length of 2 characters", MinimumLength = 2)]
            [Display(Name = "Name")]
            public string Name { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "Surname must be minimum length of 2 characters", MinimumLength = 2)]
            [Display(Name = "Surname")]
            public string Surname { get; set; }

            [Required(ErrorMessage = "Please select a Department")]
            [Display(Name = "Department")]
            public int DepartmentId { get; set; }

            [Required(ErrorMessage = "Please select a Position")]
            [Display(Name = "Position")]
            public int PositionId { get; set; }

            public List<Department> Departments { get; set; } = new List<Department>();

        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            //Input = new InputModel()
            //{
            //    RoleList = _roleManager.Roles.Select(x => x.Name).Select(a => new SelectListItem
            //    {
            //        Text = a,
            //        Value = a
            //    }),
            //};

            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            ViewData["Departments"] = await _departmentRepository.GetAllDepartmentsAsync(); 
            ViewData["Positions"] = await _positiontRepository.GetAllPositionsAsync(); 
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

                ViewData["Departments"] = new List<Department>
                    {
                new Department() { Id = 1, Name = "IT" },
                new Department() { Id = 2, Name = "Construction" },
                new Department() { Id = 3, Name = "Human Resources" },
                new Department() { Id = 4, Name = "Marketing" },
                new Department() { Id = 5, Name = "Production" },
                new Department() { Id = 6, Name = "Finance and Accounting" },
                new Department() { Id = 7, Name = "Customer Service" },
                new Department() { Id = 8, Name = "Administration" },
                new Department() { Id = 9, Name = "Procurement" },
                new Department() { Id = 10, Name = "Sales" }
                    };
                ViewData["Positions"] = new List<Position>
                    {
                new Position() { Id = 1, Name = "Software Developer" },
                new Position() { Id = 2, Name = "Constructor" },
                new Position() { Id = 3, Name = "Human Resources Manager" },
                new Position() { Id = 4, Name = "Marketing Manager" },
                new Position() { Id = 5, Name = "CNC Operator" },
                new Position() { Id = 6, Name = "Financial Controller" },
                new Position() { Id = 7, Name = "Customer Service Supporter" },
                new Position() { Id = 8, Name = "Administrative Assistant" },
                new Position() { Id = 9, Name = "Procurement Specialist" },
                new Position() { Id = 10, Name = "Sales Representative" }
                    };

                user.Name = Input.Name;
                user.Surname = Input.Surname;
                user.DepartmentId = Input.DepartmentId;
                user.PositionId = Input.PositionId;

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    //await _userManager.AddToRoleAsync(user, "USER");
                    _logger.LogInformation("User created a new account with password.");

                    //if (!String.IsNullOrEmpty(Input.Role))
                    //{
                    //    await _userManager.AddToRoleAsync(user, Input.Role);
                    //} else
                    //{
                    await _userManager.AddToRoleAsync(user, "User");
                    //}

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private User CreateUser()
        {
            try
            {
                return Activator.CreateInstance<User>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(User)}'. " +
                    $"Ensure that '{nameof(User)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<User> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<User>)_userStore;
        }
    }
}
