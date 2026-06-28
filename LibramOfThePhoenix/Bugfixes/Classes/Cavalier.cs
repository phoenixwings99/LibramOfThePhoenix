using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibramOfThePhoenix.Bugfixes.Classes
{
    internal class Cavalier
    {
        public static void FixOrderOfTheStarChannelAssistance()
        {
            if (Settings.IsDisabled("FixOrderOfTheStarCallingChannelingSupport"))
                return;
            //Still needed in EE
            FeatureConfigurator starChannelAssist = FeatureConfigurator.For("eff49ecc28a0ce54caf416bdacedf4f3");

            starChannelAssist.AddIncreaseSpellDescriptorCasterLevel(descriptor: new SpellDescriptorWrapper(SpellDescriptor.ChannelPositiveHeal | SpellDescriptor.ChannelPositiveHarm | SpellDescriptor.ChannelNegativeHeal | SpellDescriptor.ChannelNegativeHarm), bonusCasterLevel: 1, modifierDescriptor: Kingmaker.Enums.ModifierDescriptor.UntypedStackable);




            //Lay On Hands: Self
            starChannelAssist.AddCasterLevelForAbility(spell: "8d6073201e5395d458b8251386d72df1", bonus: 1, descriptor: Kingmaker.Enums.ModifierDescriptor.UntypedStackable);

            //Lay On Hands: Other
            starChannelAssist.AddCasterLevelForAbility(spell: "caae1dc6fcf7b37408686971ee27db13", bonus: 1, descriptor: Kingmaker.Enums.ModifierDescriptor.UntypedStackable);

            //Lay On Hands: Special
            starChannelAssist.AddCasterLevelForAbility(spell: "8337cea04c8afd1428aad69defbfc365", bonus: 1, descriptor: Kingmaker.Enums.ModifierDescriptor.UntypedStackable);

            starChannelAssist.Configure();
        }

    }
}
