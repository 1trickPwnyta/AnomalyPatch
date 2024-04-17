using RimWorld;
using System;
using System.Linq;
using UnityEngine;
using Verse;

namespace AnomalyPatch.BioferriteHarvesterMultipleSelection
{
    public static class InspectPaneUtility
    {
        public static void DoPaneContentsForMultipleBioferriteHarvesters(Rect rect)
        {
            if (Find.Selector.NumSelected > 1 && Find.Selector.SelectedObjects.All(obj => obj is Thing && ((Thing)obj).def == ThingDefOf.BioferriteHarvester))
            {
                Rect rect2 = rect.AtZero();
                rect2.yMin += 26f;

                try
                {
                    Widgets.BeginGroup(rect2);
                    string text = "BioferriteHarvesterContained".Translate() + ": ";
                    float amount = 0f;
                    foreach (object obj in Find.Selector.SelectedObjects)
                    {
                        amount += ((Building_BioferriteHarvester)obj).containedBioferrite;
                    }
                    text += amount.ToString("0.00");
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
}
