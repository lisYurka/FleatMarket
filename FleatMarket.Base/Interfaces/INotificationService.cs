using FleatMarket.Base.Entities;

namespace FleatMarket.Base.Interfaces
{
    public interface INotificationService
    {
        void AddNotification(Notification notification);
        void UpdateNotification(Notification notification);
    }
}
