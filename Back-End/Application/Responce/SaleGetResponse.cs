using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Responce
{
    public class SaleGetResponse
    {
        public int Id { get; set; }
        public decimal TotalPay { get; set; }
        public int TotalQuantity { get; set; }
        public DateTime Date { get; set; }
    }
}
