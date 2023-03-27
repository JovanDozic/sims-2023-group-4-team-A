using System.Linq;

namespace SIMSProject.Controller.UserController
{
    public class UserController
    {
        public readonly OwnerController _ownerController;
        public readonly GuestController _guestController;
        public readonly GuideController _guideController;

        public UserController()
        {
            _ownerController = new OwnerController();
            _guestController = new GuestController();
            _guideController = new GuideController();
        }

        public object GetByUsername(string username)
        {
            var owners = _ownerController.GetAll();
            var guests = _guestController.GetAll();
            var guides = _guideController.GetAll();

            var owner = owners.FirstOrDefault(x => x.Username == username);
            if (owner != null)
            {
                return owner;
            }

            var guest = guests.FirstOrDefault(x => x.Username == username);
            if (guest != null)
            {
                return guest;
            }

            var guide = guides.FirstOrDefault(x => x.Username == username);
            if (guide != null)
            {
                return guide;
            }

            return null;
        }
    }
}