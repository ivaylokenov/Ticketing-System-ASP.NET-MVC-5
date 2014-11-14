namespace TicketingSystem.Web.Areas.Administration.Controllers
{
    using System.Collections;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using TicketingSystem.Data;
    using TicketingSystem.Models;
    using TicketingSystem.Web.Areas.Administration.Controllers;
    using TicketingSystem.Web.Infrastructure.Caching;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;

    using Model = TicketingSystem.Models.Category;
    using ViewModel = TicketingSystem.Web.Areas.Administration.ViewModel.Categories.CategoryViewModel;

    public class CategoriesController : KendoGridAdministrationController
    {
        private readonly ICacheService service;

        public CategoriesController(ITicketSystemData data, ICacheService service)
            : base(data)
        {
            this.service = service;
        }

        public ActionResult Index()
        {
            return View();
        }

        protected override IEnumerable GetData()
        {
            return this.Data
                .Categories
                .All()
                .Project()
                .To<ViewModel>();
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Categories.GetById(id) as T;
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            var dbModel = base.Create<Model>(model);
            if (dbModel != null) model.Id = dbModel.Id;
            this.ClearCategoryCache();
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            base.Update<Model, ViewModel>(model, model.Id);
            this.ClearCategoryCache();
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var category = this.Data.Categories.GetById(model.Id.Value);

                foreach (var ticketId in category.Tickets.Select(t => t.Id).ToList())
                {
                    var comments = this.Data
                        .Comments
                        .All()
                        .Where(c => c.TicketId == ticketId)
                        .Select(c => c.Id)
                        .ToList();

                    foreach (var commentId in comments)
                    {
                        this.Data.Comments.Delete(commentId);
                    }

                    this.Data.SaveChanges();

                    this.Data.Tickets.Delete(ticketId);
                }

                this.Data.SaveChanges();

                this.Data.Categories.Delete(category);
                this.Data.SaveChanges();
            }

            this.ClearCategoryCache();
            return this.GridOperation(model, request);
        }

        private void ClearCategoryCache()
        {
            this.service.Clear("categories");
        }
    }
}