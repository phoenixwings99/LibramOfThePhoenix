using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabletopTweaks.Core.Utilities;

namespace LibramOfThePhoenix.Bugfixes
{
    internal class RadianceLevel2Fix
    {
        public static void Fixes()
        {
            BlueprintBuff radianceBuff = BlueprintTools.GetBlueprint<BlueprintBuff>("f10cba2c41612614ea28b5fc2743bc4c");

            Main.LotPContext.Logger.Log("Patching Radiance +6");


            BuffConfigurator buff = BuffConfigurator.New("Radiance+6HolyBuff", Main.LotPContext.Blueprints.GetGUID("Radiance+6HolyBuff").m_Guid.ToString());
            buff.SetFlags(BlueprintBuff.Flags.HiddenInUi, BlueprintBuff.Flags.StayOnDeath);

            
           
            buff.AddBuffEnchantSpecificWeaponWorn(enchantmentBlueprint: "28a9964d81fedae44bae3ca45710c140", weaponBlueprint: "cf5c1a507825f184dacbc3abe14b9db1");

            BlueprintBuff builtBuff = buff.Configure();

            ActionsBuilder apply = ActionsBuilder.New().ApplyBuffPermanent(builtBuff.AssetGuidThreadSafe, toCaster: true, asChild: true);

            ActionsBuilder remove = ActionsBuilder.New().RemoveBuff(builtBuff.AssetGuidThreadSafe, toCaster: true);
            BlueprintWeaponEnchantment HolyAvengerEnchant = BlueprintTools.GetBlueprint<BlueprintWeaponEnchantment>("119b0b2ddae69d4438e6a4bedff32412");

            AddFactContextActions list = HolyAvengerEnchant.Components.OfType<AddFactContextActions>().FirstOrDefault();
            if (Settings.IsEnabled("FixRadianceFinalForm"))
            {

                list.Activated.Actions = list.Activated.Actions.Append(apply.Build().Actions[0]).ToArray();
                list.Deactivated.Actions = list.Activated.Actions.Append(remove.Build().Actions[0]).ToArray();
            }

        }
    }
}
