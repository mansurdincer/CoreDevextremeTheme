namespace CoreDevextremeTheme.Controllers
{
    [Authorize]
    public abstract class BaseController : Controller
    {

        //public readonly INotyfService _toastNotification;

        //public BaseController(IToastNotification toastNotification)
        //{
        //     _toastNotification = toastNotification;
        //}

        public IActionResult ModalPopup()
        {
            return PartialView("_ModalPopup");
        }
    }
}
