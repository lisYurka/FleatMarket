using FleatMarket.Base.Entities;
using FleatMarket.Base.Interfaces;

namespace FleatMarket.Service.BusinessLogic
{
    public class NotificationService:INotificationService
    {
        private readonly IBaseRepository repository;
        public NotificationService(IBaseRepository _repository)
        {
            repository = _repository;
        }

        public void AddNotification(Notification notification)
        {
            repository.Create(notification);
        }

        public void UpdateNotification(Notification notification)
        {
            repository.Update(notification);
        }
    }
}
