using BookShop.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookShop.Domain.Entities
{
    public class DeliveryBase
    {
        public int Id { get; set; }
        public virtual string DeliveryName { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int DeliveryType { get; set; }
        public decimal DeliveryCost { get; set; }      
        public int Mobile { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryNote { get; set; }

        [NotMapped]
        public DeliveryType DeliveryTypeEnums
        {
            get
            {
                return (DeliveryType)DeliveryType;
            }
            set
            {
                DeliveryType = (int)value;
            }
        }

    }
}
