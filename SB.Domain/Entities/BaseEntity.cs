using SB.Domain.Shared;

namespace SB.Domain.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            this.Notification = new Notification();
        }
        public State State { get; set; }

        public Notification Notification { get; set; }
    }
}