#if DEBUG
using System;
using System.Collections;
using System.Collections.Generic;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Testing;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors;
using UnityEngine;

namespace noheroexpmultiplier.Tests;

public class HeroXpTests : ModTest
{
    public override IEnumerator Test()
    {
        yield return Timeout(SetupSimEnvironment(), 60);
        AssertNotNull(Environment, "The simulation did not load.");

        try
        {
            var simulation = Bridge.Simulation;
            simulation.DistributeXpToHeroes(10, true);

            var model = AssertNotNull(Bridge.Model.GetTowerWithName("Quincy"));
            var heroes = new List<Hero>();
            var soloGain = 0f;

            for (var count = 1; count <= 4; count++)
            {
                var tower = AssertNotNull(CreateTowerAt(Bridge, new Vector2(count * 10, 0), model, costOverride: 0));
                heroes.Add(AssertNotNull(tower.tower.GetTowerBehavior<Hero>()));

                var before = heroes.ConvertAll(hero => hero.totalXp);
                simulation.DistributeXpToHeroes(10, true);
                if (count == 1)
                {
                    soloGain = heroes[0].totalXp - before[0];
                    Assert(soloGain > 0, "A solo hero must receive XP.");
                }

                for (var i = 0; i < count; i++)
                {
                    var gain = heroes[i].totalXp - before[i];
                    Assert(Math.Abs(gain - soloGain) < 0.01f,
                        $"With {count} heroes, hero {i + 1} gained {gain} XP; expected {soloGain}.");
                }
            }

            var dart = AssertNotNull(Bridge.Model.GetTowerWithName("DartMonkey"));
            AssertNotNull(CreateTowerAt(Bridge, new Vector2(-20, 0), dart, costOverride: 0));
            var previousXp = heroes[0].totalXp;
            simulation.DistributeXpToHeroes(10, true);
            Assert(Math.Abs(heroes[0].totalXp - previousXp - soloGain) < 0.01f,
                "Non-hero towers must not affect XP sharing.");

            previousXp = heroes[0].totalXp;
            heroes[0].AddXp(10);
            Assert(Math.Abs(heroes[0].totalXp - previousXp - 10) < 0.01f,
                "Direct XP grants must not be multiplied.");

            ModHelper.Msg<noheroexpmultiplier>("Hero XP checks passed: 0-4 heroes, non-hero towers, direct XP.");
        }
        finally
        {
            DisposeSimEnvironment();
        }
    }
}
#endif
