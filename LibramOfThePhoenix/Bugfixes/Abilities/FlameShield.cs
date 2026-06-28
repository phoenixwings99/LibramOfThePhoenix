using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using LibramOfThePhoenix.New_Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabletopTweaks.Core.NewComponents;
using TabletopTweaks.Core.Utilities;

namespace LibramOfThePhoenix.Bugfixes.Abilities
{
    internal class FlameShield
    {
        public static void PatchFlameSheild()
        {
            if (Settings.IsDisabled("FixFlameShield"))
                return;

            var buff = BlueprintTool.Get<BlueprintBuff>("23c0f0417981608479131d25d4349f7d");
            buff.AddComponent<ElementalBarrierDamageDivisor>(x =>
            {
                x.m_Type = Kingmaker.Enums.Damage.DamageEnergyType.Cold;
            });

            var ability = BlueprintTool.Get<BlueprintAbility>("c3a13237b17de5742a2dbf2da46f23d5");
            ability.AddComponent<AbilityRequirementHasBuff>(x =>
            {
                x.RequiredBuff = buff.ToReference<BlueprintBuffReference>();
                x.Not = true;
            });
        }
    }
}
