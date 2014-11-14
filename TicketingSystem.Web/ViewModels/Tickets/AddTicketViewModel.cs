namespace TicketingSystem.Web.ViewModels.Tickets
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Web.Mvc;
    using TicketingSystem.Models;
    using TicketingSystem.Web.Infrastructure.Validation;
    using TicketSystem.Web.Infrastructure.Mapping;

    public class AddTicketViewModel : IMapFrom<Ticket>
    {
        [UIHint("Enum")]
        public PriorityType Priority { get; set; }

        [DoesNotContain("bug")]
        [Required]
        [StringLength(50)]
        [UIHint("SingleLineText")]
        public string Title { get; set; }

        [StringLength(1000)]
        [UIHint("MultiLineText")]
        public string Description { get; set; }

        [Display(Name = "Category")]
        [UIHint("DropDownList")]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public HttpPostedFileBase UploadedImage { get; set; }
    }
}