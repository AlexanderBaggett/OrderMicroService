using OrderMicroservice.DomainModels;

namespace OrderMicroservice.RequestModels
{
    public class OrderUpdateRequestModel
    {
        public long OrderId { get; set; }
        public string OrderStatus {get; set;}
        public decimal? Total { get; set; }
    }
}
