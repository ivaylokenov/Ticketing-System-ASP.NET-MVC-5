namespace TicketingSystem.Web.Areas.Administration.ViewModel.Categories
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using TicketingSystem.Models;
    using TicketSystem.Web.Infrastructure.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {
        [HiddenInput(DisplayValue = false)]
        public int? Id { get; set; }

        [Required]
        [UIHint("String")]
        public string Name { get; set; }
    }
}