using System.ComponentModel.DataAnnotations;
namespace SportsStore.Domain.Entities {
    public class ShippingDetails
    {
        [Required(ErrorMessage = "请输入您的姓名")]
        public string Name { get; set; }
        [Required(ErrorMessage = "请输入您的详细地址")]
        [Display(Name = "Line 1")]
        public string Line1 { get; set; }
        [Display(Name = "Line 2")]
        public string Line2 { get; set; }
        [Display(Name = "Line 2")]
        public string Line3 { get; set; }
        [Required(ErrorMessage = "请输入您的收货地址所在城市")]
        public string City { get; set; }
        [Required(ErrorMessage = "请输入您的收货地址所在省份")]
        public string State { get; set; }
        public string Zip { get; set; }
        [Required(ErrorMessage = "请输入您的收货地址所在国家")]
        public string Country { get; set; }
        public bool GiftWrap { get; set; }
    }
}