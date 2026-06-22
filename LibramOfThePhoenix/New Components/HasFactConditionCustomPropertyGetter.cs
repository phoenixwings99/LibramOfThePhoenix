using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Facts;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Mechanics.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibramOfThePhoenix.New_Components
{
    public class HasFactConditionCustomPropertyGetter : PropertyValueGetter
    {
        public override int GetBaseValue(UnitEntityData unit)
        {
            if (unit.Descriptor.HasFact(factReference.Get()))
                return property.GetValue(unit);
            else return 0;

        }

        public PropertyValueGetter property;

        public BlueprintUnitFactReference factReference;
    }
}
