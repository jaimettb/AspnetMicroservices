using System;

namespace EventBus.Messages.Events
{
    public class IntegrationBaseEvent
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }

        public IntegrationBaseEvent()
        {
            Id = Guid.NewGuid();
            CreateDate = DateTime.Now;
        }
        public IntegrationBaseEvent(Guid id, DateTime createDate)
        {
            Id = id;
            CreateDate = createDate;
        }
    }
}
