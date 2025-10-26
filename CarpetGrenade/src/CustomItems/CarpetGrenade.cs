using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Items;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Map;

namespace CarpetGrenade
{
    [CustomItem(ItemType.GrenadeHE)]
    public class CarpetGrenade : CustomItem
    {
        public override string Description { get; set; } = "A Grenade That Explodes Into Many Small Grenades";
        public override float Weight { get; set; } = 1f;
        public override uint Id { get; set; } = 5692;
        public override string Name { get; set; } = "Carpet Grenade";

        public virtual bool Check(CarpetGrenade item)
        {
            // If this method is called on a CarpetGrenade, simply return true
            return item is CarpetGrenade;
        }

        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties
        {
            DynamicSpawnPoints = new List<DynamicSpawnPoint>
            {
                new DynamicSpawnPoint { Chance = 40, Location = SpawnLocationType.InsideSurfaceNuke },
                new DynamicSpawnPoint { Chance = 20, Location = SpawnLocationType.InsideLczArmory },
                new DynamicSpawnPoint { Chance = 50, Location = SpawnLocationType.InsideHczArmory },
                new DynamicSpawnPoint { Chance = 30, Location = SpawnLocationType.Inside079Armory },
                new DynamicSpawnPoint { Chance = 35, Location = SpawnLocationType.Inside049Armory },
                new DynamicSpawnPoint { Chance = 25, Location = SpawnLocationType.InsideHidChamber },
            },
        };


        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Map.ExplodingGrenade += OnExploding;
            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Map.ExplodingGrenade -= OnExploding;
            base.UnsubscribeEvents();
        }


        protected void OnExploding(ExplodingGrenadeEventArgs ev)
        {

            if (!(Check(ev.Projectile)))
                return;

            // Spawn 8 mini grenades
            for (int i = 0; i < 8; i++)
            {
                var grenade = (ExplosiveGrenade)ExplosiveGrenade.Create(ItemType.GrenadeHE, null);
                grenade.ScpDamageMultiplier = 0.4f;
                grenade.MaxRadius = 0.5f;
                grenade.SpawnActive(ev.Position);
            }
        }
    }
}
