using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabletopTweaks.Core.ModLogic;
using UnityModManagerNet;

namespace LibramOfThePhoenix
{
    internal class LotPModContext : ModContextBase
    {
        public LotPModContext(UnityModManager.ModEntry modEntry) : base(modEntry)
        {
#if DEBUG
            Debug = true;
#endif
        }

        public override void LoadAllSettings()
        {
            LoadBlueprints("LibramOfThePhoenix.Config", this);
        }

        public override void AfterBlueprintCachePatches()
        {
            base.AfterBlueprintCachePatches();
            if (Debug)
            {

                SaveSettings(BlueprintsFile, Blueprints);


            }

        }
    }
}
