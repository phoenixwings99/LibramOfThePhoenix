using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.ActivatableAbilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabletopTweaks.Core.Utilities;

namespace LibramOfThePhoenix.Modified_Content.Bloodlines
{
    internal class BetterAbyssalClaws
    {
        public static string[] AbyssalClaws = {"408b5ec07bf0fd1468d3f16ef569613a",
"ee7356a601de6bb40a4929d074337b48",
"fe7a7bebab335fc4e936e3f9d23fedb4",
"f68af48f9ebf32549b5f9fdc4edfd475" };
        public static void GiveInfiniteUses()
        {
            if (Settings.IsDisabled("UnlimitedSorcererBloodlineClaws"))
            {
                return;
            }
            foreach (string s in AbyssalClaws)
            {
                BlueprintActivatableAbility claw = BlueprintTools.GetBlueprint<BlueprintActivatableAbility>(s);
                claw.Components.OfType<ActivatableAbilityResourceLogic>().FirstOrDefault().m_FreeBlueprint = claw.ToReference<BlueprintUnitFactReference>();
                claw.DeactivateIfCombatEnded = false;
                claw.OnlyInCombat = false;
                claw.DeactivateImmediately = false;
                claw.DoNotTurnOffOnRest = true;



            }
        }
    }
}
