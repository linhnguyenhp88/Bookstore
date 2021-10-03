using BookShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Domain.Interfaces.IServices
{
    public interface IDeliveryService
    {
        Task<DeliveryBase> GetDeliveryCost();
    }
}
