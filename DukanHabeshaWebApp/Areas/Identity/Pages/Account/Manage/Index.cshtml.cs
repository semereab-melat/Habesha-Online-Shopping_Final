// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using DukanHabeshaDataAccess.Repository.IRepository;
using DukanHabeshaModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DukanHabeshaWebApp.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
             IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

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
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>

            public string Name { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
           
            public string? StreetAddress { get; set; }
            public string? City { get; set; }
            public string? State { get; set; }
            public string? PostalCode { get; set; }
        }



        private async Task LoadAsync(IdentityUser user)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ApplicationUser applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value);

             _userManager.GetUserIdAsync(applicationUser).GetAwaiter().GetResult();
            
           // _signInManager.Options.User.Equals(applicationUser);


            var email = await _userManager.GetEmailAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var name = await _userManager.GetUserNameAsync(user);
            
            
           
            var streeetadress = applicationUser.StreetAddress;
            var city = applicationUser.City;
            var state = applicationUser.State;
            var postalCode = applicationUser.PostalCode;


            Email = email;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Name = name,
               
                StreetAddress = streeetadress,
                City = city,
                State = state,
                PostalCode = postalCode

            };

          
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ApplicationUser applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value);

            //var us = _userManager.GetUserAsync().;
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ApplicationUser applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value);
           


            var user = await _userManager.GetUserAsync(User);
            _signInManager.Options.User.Equals(applicationUser);



            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return LocalRedirect(returnUrl);
            }

            var name = user.UserName;
            var phoneNumber =user.PhoneNumber;
            var streetadress = applicationUser.StreetAddress;
            var city = applicationUser.City;
            var state = applicationUser.State;
            var postal = applicationUser.PostalCode;

            //var streetadress = _unitOfWork.ApplicationUser.GetFirstOrDefault(x => x.StreetAddress == Input.StreetAddress);

            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }


            }


            
           
            if (Input.Name != name)
            {
                var setNamaeResult = await _userManager.SetUserNameAsync(user, Input.Name);
                _unitOfWork.Save();

            }

            if (Input.StreetAddress != streetadress)
            {
                var setStreetResult = _unitOfWork.ApplicationUser.GetFirstOrDefault(x => x.StreetAddress == Input.StreetAddress);
                _unitOfWork.Save();
            }

            if (Input.City != city)
            {
                var setCitytResult = _unitOfWork.ApplicationUser.GetFirstOrDefault(x => x.City == Input.City);
                _unitOfWork.Save();
            }

            if (Input.State != state)
            {
                var setStateResult = _unitOfWork.ApplicationUser.GetFirstOrDefault(x => x.State == Input.State);
                _unitOfWork.Save();
            }

            if (Input.PostalCode != postal)
            {
                var setPostalResult = _unitOfWork.ApplicationUser.GetFirstOrDefault(x => x.PostalCode == Input.PostalCode);
                _unitOfWork.Save();
            }


            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return LocalRedirect(returnUrl);
        }



        /*private ApplicationUser GetApplicationUser()
        {
            try
            {
                return Activator.CreateInstanceFrom(GetApplicationUser);
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }*/
        
    }
}
