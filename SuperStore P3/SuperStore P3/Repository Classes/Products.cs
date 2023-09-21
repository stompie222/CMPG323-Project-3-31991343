using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcoPower_Logistics.Data;
using EcoPower_Logistics.Models;

namespace EcoPower_Logistics.Repository_Classes
{
    public class Products : Generic<Products>, IProducts
    {
        public Products(SuperStoreContext context) : base(context)
        {

        }
    }
}
