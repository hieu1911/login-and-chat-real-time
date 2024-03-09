using LoginAndChatRealTime.Interfaces;
using LoginAndChatRealTime.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace LoginAndChatRealTime.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IHttpContextAccessor _context;

        private readonly IUserSerivce _userService;

        private readonly IRoomService _roomService;

        public HomeController(ILogger<HomeController> logger, 
            IHttpContextAccessor context, 
            IUserSerivce userSerivce,
            IRoomService roomSerivce)
        {
            _logger = logger;
            _context = context;
            _userService = userSerivce;
            _roomService = roomSerivce;
        }

        public IActionResult Index()
        {
            var context = _context.HttpContext;
            if (!context.User.Identity.IsAuthenticated)
            {
                return Redirect("/Login");
            }

            var userName = context.User.FindFirst(type: ClaimTypes.Name).Value;
            var userId = int.Parse(context.User.FindFirst(type: "Id").Value);

            var rooms = _roomService.GetRoomsByUserId(userId);

            var model = new HomeViewModel()
            {
                UserId = userId,
                UserName = userName,
                Rooms = rooms
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
