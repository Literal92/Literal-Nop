using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Orders;
using Nop.Plugin.Faradata.AlarmShopping.Domain;
using Nop.Plugin.Faradata.AlarmShopping.Services;
using Nop.Services.Customers;
using Nop.Services.Orders;
using Nop.Web.Factories;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using Nop.Web.Framework.Security;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Faradata.AlarmShopping.Controllers
{
    [Area(AreaNames.Admin)]
    [AuthorizeAdmin]
    public partial class FaraBotController : BasePluginController
    {
        public readonly IFaraBotUserService _BotUserService;
        public readonly IFaraBotConfigService _BotConfigService;
        private readonly IWebHelper _webHelper;
        private readonly CustomerSettings _customerSettings;
        public readonly ICustomerRegistrationService _CustomerService;

        #region Fields
        private readonly IOrderService _orderService;
        private readonly IWorkContext _workContext;
        private readonly OrderSettings _orderSettings;
        private readonly ICheckoutModelFactory _checkoutModelFactory;
        private readonly IStoreContext _storeContext;
        #endregion

        public FaraBotController(IFaraBotUserService BotService, IFaraBotConfigService BotConfigService, IOrderService orderService, ICustomerRegistrationService CustomerService,
        IWorkContext workContext, OrderSettings orderSettings, IStoreContext storeContext, ICheckoutModelFactory checkoutModelFactory, IWebHelper webHelper, CustomerSettings customerSettings)
        {
            _BotUserService = BotService;
            _BotConfigService = BotConfigService;
            _orderService = orderService;
            _workContext = workContext;
            _orderSettings = orderSettings;
            _storeContext = storeContext;
            _checkoutModelFactory = checkoutModelFactory;
            _customerSettings = customerSettings;
            _webHelper = webHelper;
            _CustomerService = CustomerService;
        }


        /// <summary>
        /// دریافت لیست کاربران بات
        /// </summary>
        /// <returns></returns>
        [Route("Admin/FaraBot/Users")]
        public IActionResult Users()
        {
            var model = _BotUserService.Get(3);
            if (model == null)
                model = new List<FaraBotUser>();

            return View("~/Plugins/Faradata.AlarmShopping/Views/Users.cshtml", model);
        }


        /// <summary>
        /// تنظیمات بات
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Admin/FaraBot/Config")]
        public IActionResult Get()
        {
            var model = _BotConfigService.Get();
            if (model == null)
                model = new FaraBotConfig();

            return View("~/Plugins/Faradata.AlarmShopping/Views/Config.cshtml", model);
        }


        /// <summary>
        /// ذخیره تنظیمات
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Admin/FaraBot/Config")]
        public IActionResult Post(FaraBotConfig model)
        {
            try
            {
                if (model.Id > 0)
                    _BotConfigService.Update(model);
                else
                    _BotConfigService.Insert(model);

                TempData["message"] = "تغییرات با موفقیت ذخیره شد.";
            }
            catch (Exception ee)
            {
                TempData["message"] = "خطا در زمان ذخیره تغییرات.";
            }

            var modelOut = _BotConfigService.Get();
            return View("~/Plugins/Faradata.AlarmShopping/Views/Config.cshtml", modelOut);
        }

        /// <summary>
        /// حذف کاربر
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Admin/FaraBot/Users")]
        public IActionResult Delete(int id)
        {
            _BotUserService.DeleteUser(id);

            var model = _BotUserService.Get(3);
            return View("~/Plugins/Faradata.AlarmShopping/Views/Users.cshtml", model);

        }

        [Route("/Checkout/Completed")]
        public virtual IActionResult Completed(int? orderId)
        {
            MessageController msgController = new MessageController(_BotUserService, _webHelper, _BotConfigService, _CustomerService, _customerSettings);


            //validation
            if (_workContext.CurrentCustomer.IsGuest() && !_orderSettings.AnonymousCheckoutAllowed)
                return Challenge();

            Order order = null;
            if (orderId.HasValue)
            {
                //load order by identifier (if provided)
                order = _orderService.GetOrderById(orderId.Value);
            }
            if (order == null)
            {
                order = _orderService.SearchOrders(storeId: _storeContext.CurrentStore.Id,
                customerId: _workContext.CurrentCustomer.Id, pageSize: 1)
                    .FirstOrDefault();
            }
            if (order == null || order.Deleted || _workContext.CurrentCustomer.Id != order.CustomerId)
            {
                return RedirectToRoute("HomePage");
            }

            //disable "order completed" page?
            if (_orderSettings.DisableOrderCompletedPage)
            {
                msgController.SendReport(order.Id);
                return RedirectToRoute("OrderDetails", new { orderId = order.Id });
            }

            //model
            //string model = null;
            var model = _checkoutModelFactory.PrepareCheckoutCompletedModel(order);
            return View(model);
        }
    }
}
