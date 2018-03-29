using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Models
{
    public class CheckOutViewModel
    {
        
        [HiddenInput(DisplayValue = false)]
        public Order OrderID { get; set; }
        public User UserID { get; set; }
        public Order Address { get; set; }
    }
}