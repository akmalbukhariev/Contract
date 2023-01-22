using ContractAPI.DataAccess;
using ContractAPI.Helper;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using LibContract.HttpModels;
using LibContract.HttpResponse;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Notification.service.impl
{
    public class NotificationService : AppBaseService, INotificationService
    {
        public NotificationService(ContractMakerContext db)
        {
            dataBase = db;
        }

        public async Task<ResponseNotification> sendNotification(NotificationInfo info)
        {
            ResponseNotification response = new ResponseNotification();
            User foundUser = await dataBase.Users
                .Where(item => item.phone_number.Equals(info.phone_number))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (foundUser == null || string.IsNullOrEmpty(foundUser.token))
            {
                response.data = null;
                response.message = Constants.DoNotExist;
                return response;
            }
             
            var message = new Message();
            message.Data = new Dictionary<string, string>()
            {
                {"data_key","data_value" }
            };
            message.Token = foundUser.token;
            message.Notification = new FirebaseAdmin.Messaging.Notification
            {
                Title = info.title,
                Body =info.message
            };

            response.result = true;
            if (FirebaseMessaging.DefaultInstance != null)
                response.message = await FirebaseMessaging.DefaultInstance.SendAsync(message);

            return response;
        }
    }
}
