using System;
using Exiled.API.Features;
using Exiled.CustomItems.API.Features;

namespace CarpetGrenade
{
    public class Plugin : Plugin<Config>
    {
        // CREDIT: Rakso087 for idea
        public override string Name => "CarpetGrenade"; 
        public override string Author => "gben5692"; 
        public override Version Version => new Version(1, 0, 0);

        public override void OnEnabled()
        {
            Log.Info("CarpetGrenade enabled!");
            CustomItem.RegisterItems();
        }

        public override void OnDisabled()
        {
            Log.Info("CarpetGrenade disabled!");
        }
    }
}
