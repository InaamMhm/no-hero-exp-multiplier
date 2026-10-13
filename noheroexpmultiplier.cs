using MelonLoader;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Extensions;
using HarmonyLib;
using Il2CppAssets.Scripts.Simulation;
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
}

[HarmonyPatch(typeof(Simulation), nameof(Simulation.DistributeXpToHeroes))]
internal static class DistributeHeroXp
{
    private static void Prefix(Simulation __instance, ref float xp)
    {
        if (xp <= 0 || __instance.towerManager == null) return;

        var heroCount = 0;
        foreach (var tower in __instance.towerManager.GetTowers().ToList())
        {
            if (tower.towerModel.IsHero()) heroCount++;
        }

        // Compensate before the game splits shared XP. Direct grants and bought levels stay unchanged.
        if (heroCount > 1) xp *= heroCount;
    }
}
