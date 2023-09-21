using EcoPower_Logistics.Data;
using EcoPower_Logistics.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace EcoPower_Logistics.Repository_Classes
{
    public class Orders : Generic<Orders>, IOrders
    {
        public Orders(SuperStoreContext context) : base(context)
        {

        }

        // Had to override these functions to include products and customers
        public override async Task<IEnumerable<Orders>> GetAll()
        {
            return await _context.Set<Orders>().Include(d => d.Products).Include(d => d.Customers).ToListAsync();
        }

        // Had to override these functions to include products and customers
        public override async Task<Orders> FindById(Guid? id)
        {
            if (id == null) return null;

            return await _context.Orders
                .Include(d => d.Products)
                .Include(d => d.Customers)
                .FirstOrDefaultAsync(m => m.OrderId == id);
        }
    }
}

