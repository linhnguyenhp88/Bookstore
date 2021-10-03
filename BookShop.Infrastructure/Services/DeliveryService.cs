using AutoMapper;
using BookShop.Domain.Entities;
using BookShop.Domain.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Infrastructure.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IMapper _mapper;
     
        public DeliveryService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task<DeliveryBase> GetDeliveryCost()
        {
            throw new NotImplementedException();
        }

        private decimal SeasonDeliveryCost()
        {
            return 0;
        }
    }
}
