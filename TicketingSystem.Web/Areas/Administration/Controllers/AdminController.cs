namespace TicketingSystem.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;
    using TicketingSystem.Common;
    using TicketingSystem.Data;
    using TicketingSystem.Web.Controllers;

    [Authorize(Roles = GlobalConstants.AdminRole)]
    public abstract class AdminController : BaseController
    {
        public AdminController(ITicketSystemData data)
            : base(data)
        {

        }
    }
}