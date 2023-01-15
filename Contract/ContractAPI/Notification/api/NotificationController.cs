using ContractAPI.Helper;
using ContractAPI.Notification.service;
using LibContract.HttpModels;
using LibContract.HttpResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Notification.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : AppBaseController<INotificationService>
    {
        public NotificationController(INotificationService service) : base(service)
        {

        }

        [HttpPost("sendNotification")]
        public async Task<IActionResult> sendNotification([FromBody] NotificationInfo info)
        {
            ResponseNotification response = await Service.sendNotification(info);
            return MakeResponse(response, response.error_code);
        }
    }
}
