using AspNetCoreHero.ToastNotification.Abstractions;

namespace CoreDevextremeTheme.Controllers;

[Authorize]
public class HomeController : BaseController
{
    private readonly ApplicationDbContext _context;

    private readonly ILogger<HomeController> _logger;

    private readonly INotyfService _toastNotification;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, INotyfService toastNotification)
    {
        _logger = logger;
        _context = context;
        _toastNotification = toastNotification;
    }

    public IActionResult Index()
    {
        //var menuItems = _context.MenuItems.ToList();     
        //var sb = new StringBuilder();
        //sb.Append("<nav>");
        //sb.Append("<ul>");
        //foreach (var menuItem in menuItems.Where(mi => mi.ParentId == null))
        //{
        //    sb.Append("<li>");
        //    sb.AppendFormat("<a href='{0}'>{1}</a>", menuItem.Url, menuItem.Name);
        //    if (menuItems.Any(mi => mi.ParentId == menuItem.Id))
        //    {
        //        sb.Append("<ul>");
        //        foreach (var childMenuItem in menuItems.Where(mi => mi.ParentId == menuItem.Id))
        //        {
        //            sb.Append("<li>");
        //            sb.AppendFormat("<a href='{0}'>{1}</a>", childMenuItem.Url, childMenuItem.Name);
        //            sb.Append("</li>");
        //        }
        //        sb.Append("</ul>");
        //    }
        //    sb.Append("</li>");
        //}
        //sb.Append("</ul>");
        //sb.Append("</nav>");

        //ViewBag.MenuHtml = sb.ToString();

        return View();
    }

    public IActionResult Privacy()
    {
        string title = "Başlık";
        string content = "Popup İçeriği";

        return PartialView("_PopupPartial", new { title = title, content = content });

    }

    public IActionResult Scheduler()
    {
        return View();
    }

    public IActionResult Report()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SendMail(string mailHead, string mailBody, string mailReceiver)
    {
        EmailSender emailSender = new EmailSender();
        var address = mailReceiver;
        emailSender.SendEmailAsync(address, mailHead, mailBody);
        _toastNotification.Success($"Mail {address} adresine gönderildi");
        return Json(new { success = true });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}