using CommunityToolkit.Maui.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoriesBloom.Services
{
    public class PopupService : IPopupService
    {
        private Popup _currentPopup;

        public void HidePopup()
        {
            _currentPopup.Close();
        }

        public void ShowPopup(Popup popup)
        {
            _currentPopup = popup;
            Page page = Application.Current?.MainPage ?? throw new NullReferenceException();
            page.ShowPopup(popup);

//            Shell.Current.CurrentPage.ShowPopup(popup);
        }
    }
}
