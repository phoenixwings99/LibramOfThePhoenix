using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Kingmaker.Blueprints.Area.FactHolder;

namespace LibramOfThePhoenix.New_Components.BloodlineMutation
{
    [AllowMultipleComponents]
    public class BloodlineSpellComponent : UnitFactComponentDelegate
    {

        public BlueprintAbilityReference m_spell;
        public override void OnActivate()
        {
            OnTurnOn();
        }

        public override void OnDeactivate()
        {
            OnTurnOff();
        }

        public override void OnTurnOn()
        {

            base.OnTurnOn();
            var part = base.Owner.Ensure<UnitPartBloodlineSpellTracker>();
            part.RegisterBloodlineSpell(Fact, m_spell);


        }

        public override void OnTurnOff()
        {
            var part = base.Owner.Get<UnitPartBloodlineSpellTracker>();
            if (part != null)
                part.UnregisterBloodlineSpell(Fact, m_spell);

        }


    }

    
}
