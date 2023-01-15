using LibContract.HttpModels;
using LibContract.HttpResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Notification.service
{
    public interface INotificationService
    {
        Task<ResponseNotification> sendNotification(NotificationInfo info);
    }
}
