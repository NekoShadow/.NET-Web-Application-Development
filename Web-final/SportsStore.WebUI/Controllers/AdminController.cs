using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Concrete;
using System.Collections.Generic;
using System;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository repository;
        private EFDbContext db = new EFDbContext();
        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }
        private Employee[] EmployeeData = {
            new Employee {EmployeeID = 1, Name = "张三",Province = "福建", Role = Role.一级主管},
            new Employee {EmployeeID = 2, Name = "李四",Province = "河北", Role = Role.中级主管},
            new Employee {EmployeeID = 3, Name = "王五",Province = "浙江", Role = Role.快递员},
            new Employee {EmployeeID = 4, Name = "丁一",Province = "四川", Role = Role.快递员},
            new Employee {EmployeeID = 5, Name = "老王",Province = "辽宁", Role = Role.普通员工},
            new Employee {EmployeeID = 6, Name = "老司机",Province = "浙江", Role = Role.普通员工},
            new Employee {EmployeeID = 7, Name = "大BOSS",Province = "福建", Role = Role.高级主管},
            new Employee {EmployeeID = 8, Name = "陈二",Province = "广东", Role = Role.中级主管}
};
        public PartialViewResult EmployeeAjaxData(string selectedRole = "All")
        {
            IEnumerable<Employee> data = EmployeeData;
            if (selectedRole != "All")
            {
                Role selected = (Role)Enum.Parse(typeof(Role), selectedRole);
                data = EmployeeData.Where(p => p.Role == selected);
            }
            return PartialView(data);
        }

        public ActionResult EmployeeAjax(string selectedRole = "All")
        {
            return View((object)selectedRole);
        }

        public ViewResult Index(string Category, string ProductName, Nullable<decimal>lowPrice, Nullable<decimal> highPrice)
        {
            var CategoryLst = new List<string>();
            var CategoryQry = from d in db.Products
                           orderby d.Category
                           select d.Category;
            CategoryLst.AddRange(CategoryQry.Distinct());
            ViewBag.Category = new SelectList(CategoryLst);
            var products = from m in db.Products
                         select m;
            if (!String.IsNullOrEmpty(ProductName))
            {
                products = products.Where(s => s.Name.Contains(ProductName));
            }
            if (lowPrice.HasValue)
            {
                products = products.Where(s=>s.Price > lowPrice);
            }
            if (highPrice.HasValue)          
            {
                products = products.Where(s => s.Price < highPrice);
            }
            if (!string.IsNullOrEmpty(Category))
            {
                products = products.Where(x => x.Category == Category);
            }
            return View(products);;
        }
        public ViewResult Edit(int productId)
        {
            Product product = repository.Products
            .FirstOrDefault(p => p.ProductID == productId);
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }
                repository.SaveProduct(product);
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            }
            else {
                return View(product);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Product());
        }

        [HttpPost]
        public ActionResult Delete(int productId)
        {
            Product deletedProduct = repository.DeleteProduct(productId);
            if(deletedProduct != null)
            {
                TempData["message"] = string.Format("{0} was deleted",
                    deletedProduct.Name);
            }
            return RedirectToAction("Index");
        }

        public ActionResult ViewUser(Nullable<int>UserID,string UserName, string Email,string PhoneNumber)
        {
            var users = from m in db.Users
                           select m;
            if (UserID.HasValue)
            {
                users = users.Where(s => s.UserID==UserID);
            }
            if (!String.IsNullOrEmpty(UserName))
            {
                users = users.Where(s => s.Name.Contains(UserName));
            }          
            if (!String.IsNullOrEmpty(Email))
            {
                users = users.Where(s => s.Email.Contains(Email));
            }
            if (!String.IsNullOrEmpty(PhoneNumber))
            {
                users = users.Where(s => s.PhoneNumber.Contains(PhoneNumber));
            }
            return View(users);
        }

        public ActionResult ViewOrder(Nullable<int> OrderID, Nullable<int> UserID,string Address, Nullable<decimal> low, Nullable<decimal> high)
        {
            var orders = from m in db.Orders
                        select m;
            if (OrderID.HasValue)
            {
                orders = orders.Where(s => s.OrderID == OrderID);
            }
            if (UserID.HasValue)
            {
                orders = orders.Where(s => s.UserID == UserID);
            }
            if (!String.IsNullOrEmpty(Address))
            {
                orders = orders.Where(s => s.Address.Contains(Address));
            }
            if (low.HasValue)
            {
                orders = orders.Where(s => s.TotalPrice > low);
            }
            if (high.HasValue)
            {
                orders = orders.Where(s => s.TotalPrice < high);
            }
            return View(orders);
        }
        public ActionResult ViewOrderItems(Nullable<int> UserID, Nullable<int> OrderID, Nullable<int>ProductID, Nullable<decimal> low, Nullable<decimal> high)
        {
            var orderItemList = from uu in db.Orders
                                join ud in db.OrderItems on uu.OrderID equals ud.OrderID
                                select new OrderAndItem { UserID = uu.UserID, OrderID = uu.OrderID, ProductID = ud.ProductID, Quantity = ud.Quantity};
            if (UserID.HasValue)
            {
                orderItemList = orderItemList.Where(s => s.UserID == UserID);
            }
            if (OrderID.HasValue)
            {
                orderItemList = orderItemList.Where(s => s.OrderID == OrderID);
            }
            if (ProductID.HasValue)
            {
                orderItemList = orderItemList.Where(s => s.ProductID == ProductID);
            }
            if (low.HasValue)
            {
                orderItemList = orderItemList.Where(s => s.Quantity > low);
            }
            if (high.HasValue)
            {
                orderItemList = orderItemList.Where(s => s.Quantity < high);
            }
            return View(orderItemList);
        }
    }
}