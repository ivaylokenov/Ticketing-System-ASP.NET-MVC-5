namespace TicketingSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using TicketingSystem.Data;
    using TicketingSystem.Web.ViewModels.Home;
using TicketingSystem.Web.Infrastructure.Services.Contracts;

    public class HomeController : BaseController
    {
        private IHomeServices homeServices;

        public HomeController(ITicketSystemData data, IHomeServices homeServices)
            : base(data)
        {
            this.homeServices = homeServices;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        [ChildActionOnly]
        [OutputCache(Duration = 60 * 60)]
        public ActionResult MostCommentedTickets()
        {
            return PartialView("_MostCommentedTicketsPartial", this.homeServices.GetIndexViewModel(6));
        }
    }
}