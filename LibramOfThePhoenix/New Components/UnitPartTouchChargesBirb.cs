using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Controllers;
using Kingmaker.EntitySystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Parts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LibramOfThePhoenix.New_Components
{
    internal class UnitPartTouchChargesBirb : OldStyleUnitPart
    {
        private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(UnitPartTouchChargesBirb));

        [JsonProperty]
        private int Charges = 0;

        internal void Init(int charges)
        {
            Charges = charges;
        }

        internal void UseCharge() { Charges--; }
        internal bool HasCharge() { return Charges > 0; }
        internal void Remove() { Charges = 0; }

        [HarmonyPatch(typeof(TouchSpellsController))]
        static class TouchSpellsController_Patch
        {
            static readonly MethodInfo RemoveUnitPart =
              AccessTools.Method(typeof(EntityDataBase), "Remove", generics: new[] { typeof(UnitPartTouch) });

            private static void ConsumeCharge(UnitEntityData caster)
            {
                var charges = caster.Get<UnitPartTouchChargesBirb>();
                if (charges is null)
                {
                    Logger.Verbose(() => "No charges to consume");
                    caster.Remove<UnitPartTouch>();
                    return;
                }

                Logger.Verbose(() => $"Consuming touch charge on {caster.CharacterName}");
                charges.UseCharge();
                if (!charges.HasCharge())
                {
                    Logger.Verbose(() => "No charges remaining");
                    caster.Remove<UnitPartTouch>();
                    
                }
            }

            [HarmonyPatch(nameof(TouchSpellsController.OnAbilityEffectApplied)), HarmonyPrefix]
            static bool OnAbilityEffectApplied(TouchSpellsController __instance, AbilityExecutionContext context)
            {
                try
                {
                    UnitEntityData maybeCaster = context.MaybeCaster;
                    if (maybeCaster == null)
                    {
                        return true;
                    }
                    UnitPartTouch unitPartTouch = maybeCaster.Get<UnitPartTouch>();
                    if (unitPartTouch && unitPartTouch.Ability.Data == context.Ability && context.Ability.Blueprint.GetComponent<TouchChargesBirb>() is not null && maybeCaster.Get<UnitPartTouchChargesBirb>() is not null)
                    {
#if DEBUG
                        Logger.Log($"Before ConsumeCharge called on: {context.Ability.Name} there are {maybeCaster.Get<UnitPartTouchChargesBirb>().Charges} remaining");
#endif
                        ConsumeCharge(maybeCaster);

#if DEBUG
                        if (maybeCaster.Get<UnitPartTouchChargesBirb>())
                            Logger.Log($"After ConsumeCharge called on: {context.Ability.Name} there are {maybeCaster.Get<UnitPartTouchChargesBirb>().Charges} remaining");
                        else
                        {
                            Logger.Log($"After ConsumeCharge called on: {context.Ability.Name} UnitPartTouchChargesBirb is removed, {(maybeCaster.Get<UnitPartTouch>() is null ? "UnitPartTouch is removed" : "UnitPartTouch is not removed")}");
                        }
#endif

                        return false;
                    }
                    else return true;
                    
                }
                catch (Exception e)
                {
                    Logger.LogException("TouchSpellsController.OnAbilityEffectApplied", e);
                    return false;
                }
              
            }
        }

        [HarmonyPatch(typeof(UnitPartTouch))]
        static class UnitPartTouch_Patch
        {
            [HarmonyPatch(nameof(UnitPartTouch.Init)), HarmonyPostfix]
            static void Init(BlueprintAbility ability, AbilityExecutionContext context)
            {
                try
                {
                    var caster = context.MaybeCaster;
                    if (caster is null)
                    {
                        Logger.Warning("No caster");
                        return;
                    }

                    var touchCharges = ability.GetComponent<TouchChargesBirb>();
                    if (touchCharges is null)
                    {
                        Logger.Verbose(() => "Not a touch ability");
                        return;
                    }

                    var count = touchCharges.GetChargeCount(context);
                    var charges = caster.Ensure<UnitPartTouchChargesBirb>();
                    charges.Init(count);
                    Logger.Verbose(() => $"Added {count} touch charges for {caster.CharacterName}");
                }
                catch (Exception e)
                {
                    Logger.LogException("UnitPartTouch_Patch.Init", e);
                }
            }

            [HarmonyPatch(nameof(UnitPartTouch.OnDispose)), HarmonyPostfix]
            static void OnDispose(UnitPartTouch __instance)
            {
                try
                {
                    __instance.Owner.Remove<UnitPartTouchChargesBirb>();
                }
                catch (Exception e)
                {
                    Logger.LogException("UnitPartTouch_Patch.OnDispose", e);
                }
            }
        }

        [AllowedOn(typeof(BlueprintAbility))]
        [TypeId("10e90122-6ef9-4967-a284-259aed042cd7")]
        internal class TouchChargesBirb : UnitFactComponentDelegate
        {
            private readonly ContextValue Charges;

            internal TouchChargesBirb(ContextValue charges)
            {
                Charges = charges;
            }

            internal int GetChargeCount(MechanicsContext context)
            {
                return Charges.Calculate(context);
            }
        }
    }
}