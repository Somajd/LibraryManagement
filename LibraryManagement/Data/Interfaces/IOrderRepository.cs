﻿using LibraryManagement.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Data.Interfaces
{
    public interface IOrderRepository: IRepository<Order>
    {
        IEnumerable<Order> GetAllAvailableCatalog();

        IEnumerable<Order> GetOrderWithCatalog(int OrderID);
    }
}
