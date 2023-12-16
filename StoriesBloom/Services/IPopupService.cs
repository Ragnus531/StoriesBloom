using CommunityToolkit.Maui.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoriesBloom.Services
{
    public  interface IPopupService
    {
        void ShowPopup(Popup popup);
        void HidePopup();
    }
}
