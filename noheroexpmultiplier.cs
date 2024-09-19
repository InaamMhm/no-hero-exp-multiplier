using System;
using MelonLoader;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Extensions;
using HarmonyLib;
using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Towers.Weapons;
using Il2CppAssets.Scripts.Simulation;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors;
using Il2CppAssets.Scripts.Unity.Bridge;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using noheroexpmultiplier;

[assembly: MelonInfo(typeof(noheroexpmultiplier.noheroexpmultiplier), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace noheroexpmultiplier;

public class noheroexpmultiplier : BloonsTD6Mod
{
    public override void OnApplicationStart()
    {
        ModHelper.Msg<noheroexpmultiplier>("noheroexpmultiplier loaded!");
    }

    [HarmonyPatch(typeof(Hero), nameof(Hero.AddXp))]
    static class Hero_AddXp
    {
        static void Prefix(ref float amount)
        {
            var towers = InGame.instance.bridge.GetAllTowers().ToList();
            int count = 0;
            foreach (var tower in towers)
            {
                if (tower.tower.towerModel.IsHero())
                {
                    count = count + 1;
                }
            
            
            }
            amount = amount * count;
  
        }
    }
}


