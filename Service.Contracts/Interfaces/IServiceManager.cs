﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.Interfaces
{
    public interface IServiceManager
    {
        IInventoryService InventoryService { get; }
        IUserService UserService { get; }
    }
}