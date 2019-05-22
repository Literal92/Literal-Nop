using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Plugin.Faradata.FirstTest.Services;
using Nop.Services.Catalog;
using Nop.Services.Customers;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nop.Plugin.Faradata.FirstTest.Area.Api.Controllers
{
    [Area(FirstTestAreaNames.Api)]
    public class AuthController : BasePluginController
    {
        private readonly ICustomerService _CustomerService;
        private readonly ICustomerTokenService _CustomerTokenService;
        private readonly CustomerSettings _customerSettings;
        private readonly ICustomerRegistrationService _customerRegistrationService;

        public AuthController(ICustomerService customerService, ICustomerRegistrationService customerRegistrationService, CustomerSettings customerSettings, ICustomerTokenService CustomerTokenService)
        {
            _CustomerService = customerService;
            _customerSettings = customerSettings;
            _customerRegistrationService = customerRegistrationService;
            _CustomerTokenService = CustomerTokenService;
        }

        public IActionResult Login(string username, string password, string deviceId)
        {
            var loginResult = _customerRegistrationService.ValidateCustomer(_customerSettings.UsernamesEnabled ? username : username, password);

            switch (loginResult)
            {
                case CustomerLoginResults.Successful:
                    var customer = new Customer();
                    if (!_customerSettings.UsernamesEnabled)
                        customer = _CustomerService.GetCustomerByEmail(username);
                    else
                        customer = _CustomerService.GetCustomerByUsername(username);
                    var token = _CustomerTokenService.SetToken(customer.Id, deviceId);
                    return Ok(token);

                case CustomerLoginResults.CustomerNotExist:
                    return NotFound("CustomerNotExist");
                case CustomerLoginResults.WrongPassword:
                    return NotFound("WrongPassword");
                case CustomerLoginResults.NotActive:
                    return NotFound("NotActive");
                case CustomerLoginResults.Deleted:
                    return NotFound("Deleted");
                case CustomerLoginResults.NotRegistered:
                    return NotFound("NotRegistered");
                case CustomerLoginResults.LockedOut:
                    return NotFound("LockedOut");
                default:
                    return BadRequest("faild Login");
            }

        }
    }
}
