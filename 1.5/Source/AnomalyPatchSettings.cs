using UnityEngine;
using Verse;

namespace AnomalyPatch
{
    public class AnomalyPatchSettings : ModSettings
    {
        public static bool AtmosphericHeaterFactor = true;
        public static bool BioferriteHarvesterMultipleSelection = true;
        public static bool CharacterHighlighting = true;

        public static void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();

            listingStandard.Begin(inRect);

            listingStandard.CheckboxLabeled("AnomalyPatch_AtmosphericHeaterFactor".Translate(), ref AtmosphericHeaterFactor);
            listingStandard.CheckboxLabeled("AnomalyPatch_BioferriteHarvesterMultipleSelection".Translate(), ref BioferriteHarvesterMultipleSelection);
            listingStandard.CheckboxLabeled("AnomalyPatch_CharacterHighlighting".Translate(), ref CharacterHighlighting);

            listingStandard.End();
        }

        public override void ExposeData()
        {
            Scribe_Values.Look(ref AtmosphericHeaterFactor, "AtmosphericHeaterFactor", true);
            Scribe_Values.Look(ref BioferriteHarvesterMultipleSelection, "BioferriteHarvesterMultipleSelection", true);
            Scribe_Values.Look(ref CharacterHighlighting, "CharacterHighlighting", true);
        }
    }
}
