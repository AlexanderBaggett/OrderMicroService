using Microsoft.EntityFrameworkCore;
using OrderMicroservice.DomainModels;

namespace OrderMicroservice.Db
{
    public class OrderContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //}
       public OrderContext(DbContextOptions<OrderContext> options): base(options)
        {

        }
}
    }
