using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TabletopTweaks.Core.Utilities.ClassTools;

namespace LibramOfThePhoenix.New_Components.BloodlineMutation
{
    internal class BloodHavocComponent : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleCalculateDamage>, IRulebookHandler<RuleCalculateDamage>, ISubscriber, IInitiatorRulebookSubscriber
    {

        public List<BlueprintCharacterClassReference> classes = new();

        public List<BlueprintSpellbookReference> archetypeBooks = new();

        protected List<BlueprintAbilityReference> m_ElementalScalingAttackPowers;

        protected List<BlueprintAbilityReference> m_BadScalingAttackPowers;

        protected List<BlueprintAbilityReference> allattackPowers => m_ElementalScalingAttackPowers.Concat(m_BadScalingAttackPowers).ToList();

        private Dictionary<SpellSchool, BlueprintFeatureReference> spellFocuses;

        private BlueprintFeatureReference _spellFocus;
        public void OnEventAboutToTrigger(RuleCalculateDamage evt)
        {



            MechanicsContext context = evt.Reason.Context;


            if (((context != null) ? context.SourceAbility : null) == null)
            {
                return;
            }
            if (context.SourceAbilityContext?.Ability == null)
            {
                return;
            }
            else if (AppliesToAbility(context.SourceAbilityContext.Ability))
            {
                foreach (BaseDamage baseDamage in evt.DamageBundle)
                {
                    if (!baseDamage.Precision)
                    {
                        DiceFormula modifiedValue = baseDamage.Dice.ModifiedValue;
                        int bonus = modifiedValue.Rolls;
                        baseDamage.AddModifier(bonus, base.Fact);
                    }
                }
            }


        }

        public void OnEventDidTrigger(RuleCalculateDamage evt)
        {

        }




        protected virtual bool AppliesToAbility(AbilityData ability, bool resourceUsing = false)
        {
            if (spellFocuses == null)
            {
                spellFocuses = new();
                spellFocuses.Add(SpellSchool.Abjuration, BlueprintTool.GetRef<BlueprintFeatureReference>("71a3f1c1ac77ae3488b9b3d6d2aac01a"));
                spellFocuses.Add(SpellSchool.Conjuration, BlueprintTool.GetRef<BlueprintFeatureReference>("d342cc595f499434687f9765f56d525c"));
                spellFocuses.Add(SpellSchool.Divination, BlueprintTool.GetRef<BlueprintFeatureReference>("955e97411611d384db2cbc00d7ed5ead"));
                spellFocuses.Add(SpellSchool.Enchantment, BlueprintTool.GetRef<BlueprintFeatureReference>("c5bf645f128c39b40850cde005b8538f"));
                spellFocuses.Add(SpellSchool.Evocation, BlueprintTool.GetRef<BlueprintFeatureReference>("c5bf645f128c39b40850cde005b8538f"));
                spellFocuses.Add(SpellSchool.Illusion, BlueprintTool.GetRef<BlueprintFeatureReference>("e588279a80eb7a24b813fadad4bc83b5"));
                spellFocuses.Add(SpellSchool.Necromancy, BlueprintTool.GetRef<BlueprintFeatureReference>("8791da25011fd1844ad61a3fea6ece54"));
                spellFocuses.Add(SpellSchool.Transmutation, BlueprintTool.GetRef<BlueprintFeatureReference>("49907a2e51b49d641aad3c9781a3a698"));
                _spellFocus = BlueprintTool.GetRef<BlueprintFeatureReference>("16fa59cc9a72a6043b566b49184f53fe");
            }

            if (ability.Blueprint.IsSpell)
            {


                if (classes.Any(x => x.Equals(ability.SpellbookBlueprint.m_CharacterClass) || archetypeBooks.Any(x => ability.m_SpellbookBlueprint)))
                {
                    var part = Owner.Ensure<UnitPartBloodlineSpellTracker>();
                    if (part.IsBloodlineSpell(ability.Blueprint.ToReference<BlueprintAbilityReference>()))
                    {
                        return true;
                    }

                    var school = ability.Blueprint.School;
                    if (school == SpellSchool.None)
                        return false;
                    else if (Owner.Progression.Features.HasFact(spellFocuses[school]))
                        return true;
                    var expandedstudypart = Owner.Get<UnitPartExpandedArsenal>();
                    if (expandedstudypart == null)
                        return false;
                    else
                        return expandedstudypart.HasSpellSchoolEntry(school) && Owner.Progression.Features.HasFact(_spellFocus);


                }
                else
                {
                    return false;
                }


            }
            else if (ability.Blueprint.Type == Kingmaker.UnitLogic.Abilities.Blueprints.AbilityType.SpellLike || ability.Blueprint.Type == Kingmaker.UnitLogic.Abilities.Blueprints.AbilityType.Supernatural && Settings.IsEnabled("BloodlineMutationsForPowers"))
            {
                if (resourceUsing)
                    return m_ElementalScalingAttackPowers.Contains(ability.Blueprint.ToReference<BlueprintAbilityReference>());
                else
                    return allattackPowers.Contains(ability.Blueprint.ToReference<BlueprintAbilityReference>());

            }
            else
            {
                return false;
            }
        }
    }
}