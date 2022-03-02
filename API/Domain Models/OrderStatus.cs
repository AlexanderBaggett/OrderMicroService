namespace OrderMicroservice.DomainModels
{
    public static class OrderStatus
    {
        public const string Pending = "Pending";
        public const string Billed = "Billed";
        public const string Shipped = "Shipped";
        public const string Delivered = "Delivered";


        public static bool IsValid(string s)
        {
            return s switch
            {
                Pending => true,
                Billed =>  true,
                Shipped => true,
                Delivered => true,
                _ => false,

            };
        }
    }
}
