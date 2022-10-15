﻿using ContractAPI.DataAccess;
using Contract.HttpModels;
using Contract.HttpResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Users.service
{
    public interface IUserInfoService
    {
        Task<ResponseUser> getUser(string phoneNumber);
        Task<ResponseLogin> updateUserPassword(User user);
        Task<IList<User>> getAllUsers();
    }
}
