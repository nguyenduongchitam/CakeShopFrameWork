using CakeShop.Data;
using CakeShop.Infrastructure;
using CakeShop.Models;
using CakeShop.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using NuGet.Protocol.Core.Types;

namespace CakeShop.Controllers
{
	public class CartController : Controller
	{
		private readonly ApplicationDbContext _context;

		public CartController(ApplicationDbContext context)
		{
			_context = context;
			//Cart = new Cart();
		}
		public Cart? Cart { get; set; }
		public IActionResult Index()
		{
			return View("Cart", HttpContext.Session.GetJson<Cart>("cart"));
		}
		public IActionResult AddToCart(int productID)
		{
			Product? product = _context.Product?
			.FirstOrDefault(p => p.product_id == productID);
			if (product != null)
			{
				Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
				Cart.AddItem(product, 1);
				HttpContext.Session.SetJson("cart", Cart);
			}
			return View("Cart", Cart);
		}
		public IActionResult AddMultyToCart(int productID,int qty)
		{
			Product? product = _context.Product?
			.FirstOrDefault(p => p.product_id == productID);
			if (product != null)
			{
				Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
				Cart.AddItem(product, qty);
				HttpContext.Session.SetJson("cart", Cart);
			}
			return View("Cart", Cart);
		}
		public IActionResult RemoveFromCart(int productID)
		{
			Product? product = _context.Product?
			.FirstOrDefault(p => p.product_id == productID);
			if (product != null)
			{
				Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
				Cart.RemoveLine(product);
				HttpContext.Session.SetJson("cart", Cart);
			}
			return View("Cart", Cart);
		}
        public IActionResult DecreaseCart(int productID)
        {
            Product? product = _context.Product?
            .FirstOrDefault(p => p.product_id == productID);
            if (product != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(product, -1);
                HttpContext.Session.SetJson("cart", Cart);
            }
            return View("Cart", Cart);
        }
		public IActionResult CheckOut()
		{
            ViewBag.Name = "";
            ViewBag.Phone = "";
            ViewBag.Address = "";
            ViewBag.Email = "";
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString(Const.USERIDSESSION)))
            {
                User cus = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString(Const.USERSESSION).ToString());
                ViewBag.Name = cus.full_name;
                ViewBag.Phone = cus.phone_number;
                ViewBag.Address = cus.address;
                ViewBag.Email = cus.email;
            }
            return View(HttpContext.Session.GetJson<Cart>("cart"));
		}
		public IActionResult ThankYou()
		{
			return View("ThankYou");
		}
        public async Task<IActionResult> AddOrder()
        {
            CheckOut checkout = JsonConvert.DeserializeObject<CheckOut>(HttpContext.Session.GetString(Const.CHECKOUTSESSION));
            var name = checkout.Name.Trim();
            var phone = checkout.Phone.Trim();
            var address = checkout.Address.Trim();
            var city = checkout.City.Trim();
            var district = checkout.District.Trim();
            var note = checkout.Note.Trim();
            var ward = checkout.Ward.Trim();

            if (String.IsNullOrEmpty(HttpContext.Session.GetString(Const.USERIDSESSION)))
            {
                User cus = new User()
                {
                    full_name = name,
                    phone_number = phone,
                    address = address
                };
                _context.User.Add(cus);
                await _context.SaveChangesAsync();

                HttpContext.Session.SetString(Const.USERIDSESSION, cus.user_id.ToString());
            }

            Cart = HttpContext.Session.GetJson<Cart>("cart");
            decimal? totalOrder = Cart.ComputeTotalValue();
            string orderId = Guid.NewGuid().ToString();
            Order order = new Order()
            {
                order_id = int.Parse(orderId),
                address = address,
                user_id = int.Parse(HttpContext.Session.GetString(Const.USERIDSESSION)),
                total_money = (int)totalOrder,
                created_at = DateTime.Now,
                city = city,
                district = district,
                note = note,
                ward = ward,
                delivery_money = 10000,
                status = StatusConst.WAITCONFIRM
            };
            _context.Order.Add(order);
            await _context.SaveChangesAsync();

            // Thêm sản phẩm vào đơn hàng
            foreach (var item in Cart.Lines)
            {
                try
                {
                    Order_Detail orderDetail = new Order_Detail()
                    {
                        order_id = int.Parse(orderId),
                        num = item.Quantity,
                        product_id = item.Product.product_id,
                        price = item.Product.price,
                    };
                    _context.Order_Detail.Add(orderDetail);
                }
                catch (Exception e)
                {
                    throw;
                }
            }

            order.total_money = (int)totalOrder + order.delivery_money;
            _context.Order.Update(order);
            await _context.SaveChangesAsync();

            //_notyfService.Success("Đã đặt hàng thành công! Cảm ơn quý khách hàng đã ủng hộ", 10);
            Cart.Clear();
            return Redirect("ThankYou");
        }

    }
}
