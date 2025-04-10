using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Reforged.Helper;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Reforged
{

    public abstract class ModifierItem : ModItem
    {

        public bool ApplyMelee(Item item)
        {
            return item.melee;
        }
        public virtual bool ApplyOn(Item weapon)
        {
            return true;
        }
    }

    public class ModifierBeam : ModifierItem
    {

    }
	public class Reforged : Mod
	{
        public override object Call(params object[] args)
        {
            return "[Reforged] : None";
        }
	}

	public struct RiStat
    {
        public string type;
		public byte tier;
    }

	public class RiWeapon : GlobalItem
	{
		public RiStat modifier;

		public static bool Get(Item item, out RiWeapon wep)
		{
			if (item.TryGetGlobalItem<RiWeapon>(out wep))
			{
                return true;
			}
            return false;
		}


        public override bool InstancePerEntity => true;
        protected override bool CloneNewInstances => true;

        // Handle Shooting
        public override bool? UseItem(Item item, Player player)
        {
            if (Get(item,out var wep))
            {
                if (RiPlayer.Get(player, out var rip))
                {
                    if (rip.localUseCooldown > 0)
                    {
                        if (wep.modifier.type == "beam")
                        {
                            rip.localUseCooldown = item.useTime + 2;
                        }
                    }
                }
            }
            return null;
        }

        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.damage > 0 && entity.maxStack == 1;
        }
	}

    public class RiProjectileExperimental : GlobalProjectile
    {

        
        public override void AI(Projectile projectile)
        {
            base.AI(projectile);
        }
    }
	public class RiPlayer : ModPlayer
	{


        public static bool Get(Player entity, out RiPlayer obj)
		{
			if (entity.TryGetModPlayer<RiPlayer>(out obj))
			{
                return true;
			}
            return false;
		}

        public int localUseCooldown;

        public override void ResetEffects()
        {
            if (localUseCooldown > 0) localUseCooldown--;
        }

        public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (RiWeapon.Get(item,out var wep))
            {
                if (wep.modifier.type == "nodebuff")
                {
                    if (target.ClearBadBuff())
                    {
                        modifiers.ScalingBonusDamage += wep.modifier.tier;
                    }
                }
            }
        }
	}
}
