using RimWorld;
using System;
using System.Linq;
using UnityEngine;
using Verse;

namespace AnomalyPatch.BioferriteHarvesterMultipleSelection
{
    public static class InspectPaneUtility
    {
        public static bool ShouldShowContentsForMultipleBioferriteHarvesters()
        {
            return AnomalyPatchSettings.BioferriteHarvesterMultipleSelection && Find.Selector.NumSelected > 1 && Find.Selector.SelectedObjects.All(obj => obj is Thing && ((Thing)obj).def == ThingDefOf.BioferriteHarvester);
        }

        public static void DoPaneContentsForMultipleBioferriteHarvesters(Rect rect)
        {
            Rect rect2 = rect.AtZero();
            rect2.yMin += 26f;

            try
            {
                Widgets.BeginGroup(rect2);
                string text = "BioferriteHarvesterContained".Translate() + ": ";
                float contained = 0f;
                float perDay = 0f;
                foreach (object obj in Find.Selector.SelectedObjects)
                {
                    contained += ((Building_BioferriteHarvester)obj).containedBioferrite;
                    perDay += (float)AnomalyPatchRefs.m_Building_BioferriteHarvester_get_BioferritePerDay.Invoke(obj, new object[] { });
                }
                text += contained.ToString("0.00");
                text += $" (+{perDay.ToString("0.00")} {"BioferriteHarvesterPerDay".Translate()})";
                InspectPaneFiller.DrawInspectString(text, rect2);
            }
            catch (Exception ex)
            {
                Debug.Log(string.Concat(new object[]
                {
                "Error in DoPaneContentsForMultipleBioferriteHarvesters ",
                Find.Selector.FirstSelectedObject,
                ": ",
                ex.ToString()
                }));
            }
            finally
            {
                Widgets.EndGroup();
            }
        }
    }
}
