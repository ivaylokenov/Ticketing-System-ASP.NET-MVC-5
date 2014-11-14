namespace TicketingSystem.Web.Infrastructure.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper.QueryableExtensions;

    using TicketingSystem.Data;
    using TicketingSystem.Web.ViewModels.Home;
    using TicketingSystem.Web.Infrastructure.Services.Base;
    using TicketingSystem.Web.Infrastructure.Services.Contracts;

    public class HomeServices : BaseServices, IHomeServices
    {
        public HomeServices(ITicketSystemData data)
            : base(data)
        {
        }

        public IList<TicketViewModel> GetIndexViewModel(int numberOfTickets)
        {
            var indexViewModel = this.Data
                .Tickets
                .All()
                .OrderByDescending(t => t.Comments.Count())
                .Take(numberOfTickets)
                .Project()
                .To<TicketViewModel>()
                .ToList();

            return indexViewModel;
        }
    }
}