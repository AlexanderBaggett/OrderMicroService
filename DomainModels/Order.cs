using System;
using System.ComponentModel.DataAnnotations;

namespace OrderMicroservice.DomainModels
{
    public class Order
    {
        [Key]
        public long Id { get; set; }

        public long CustomerId { get; set; }

        [StringLength(16)]
        public string OrderNumber { get; set; }
        public DateTime CreatedDate { get; set; }

        [StringLength(16)]
        public string OrderStatus { get; set; }
        public decimal? Total { get; set; }
    }
}
