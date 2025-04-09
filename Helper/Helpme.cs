using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
namespace Reforged.Helper
{
    public static class Helpme 
    {


        
        /// <summary>
        /// Check if NPC has any corresponding buffs
        /// </summary>
        /// <param name="npc"></param>
        /// <param name="buff"></param>
        /// <returns></returns>
        public static bool HasAnyBuff(this NPC npc,params int[] buff)
        {
            // check immunity
            for (int i = 0; i < buff.Length; i++)
            {
                if (npc.buffImmune[i])
                {
                    return false;
                }   
            }

            // iterate npc buffs
            for (int i = 0; i < NPC.maxBuffs; i++)
            {
                // iterate params
                for (int j = 0; j < buff.Length; j++)
                {
                    // check buff
                    if (npc.buffTime[i] >= 1 && npc.buffType[i] == buff[j])
                    {
                        return true;
                    }   
                }
            }
            return false;
        }


        public static bool HasBadBuff(this NPC npc)
        {
            // iterate npc buffs
            for (int i = 0; i < NPC.maxBuffs; i++)
            {
                // check buff
                if (npc.buffTime[i] >= 1 && Main.debuff[npc.buffType[i]])
                {
                    return true;
                }   
            }
            return false;
        }

        public static bool ClearBadBuff(this NPC npc)
        {
            bool hasBuff = false;

            // iterate npc buffs
            for (int i = 0; i < NPC.maxBuffs; i++)
            {
                // check buff
                if (npc.buffTime[i] >= 1 && Main.debuff[npc.buffType[i]])
                {
                    npc.DelBuff(i);
                    hasBuff = true;
                }   
            }
            return hasBuff;
        }
    }
}