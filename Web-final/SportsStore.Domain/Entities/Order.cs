using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SportsStore.Domain.Entities
{
    public class Order
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int OrderID { get; set; }
        public int UserID { get; set; }

        [Required(ErrorMessage = "请输入您的地址")]
        public string Address { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
