namespace TicketingSystem.Web.Infrastructure.Services.Base
{
    using TicketingSystem.Data;

    public abstract class BaseServices
    {
        protected ITicketSystemData Data { get; private set; }

        public BaseServices(ITicketSystemData data)
        {
            this.Data = data;
        }
    }
}