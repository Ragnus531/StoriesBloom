using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoriesBloom.Platforms.Android
{
    public class CustomPickerHandler : PickerHandler
    {
        /// <summary>
        /// Create Platform View
        /// </summary>
        /// <returns>Platform View</returns>
        protected override MauiPicker CreatePlatformView()
        {
            // Create Platform View
            var platformView = base.CreatePlatformView();
            // Initialize Key Listener
            platformView.KeyListener = null;

            return platformView;
        }

        /// <summary>
        /// Connect Handler
        /// </summary>
        /// <param name="platformView">Platform View</param>
        protected override void ConnectHandler(MauiPicker platformView)
        {
            base.ConnectHandler(platformView);
        }

        /// <summary>
        /// Disconnect Handler
        /// </summary>
        /// <param name="platformView">Platform View</param>
        protected override void DisconnectHandler(MauiPicker platformView)
        {
            base.DisconnectHandler(platformView);
        }
    }
}
