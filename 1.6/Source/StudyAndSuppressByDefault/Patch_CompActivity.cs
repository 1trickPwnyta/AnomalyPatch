using HarmonyLib;
using RimWorld;
using SpecialSauce.Multipatch;
using System;

namespace AnomalyPatch.StudyAndSuppressByDefault
{
    [HarmonyPatch_Compatibility(SpecialMod_Multipatch_Anomaly.PACKAGE_ID, Settings.StudyAndSuppressByDefault)]
    [HarmonyPatch(typeof(CompActivity))]
    [HarmonyPatch(MethodType.Constructor)]
    [HarmonyPatch(new Type[] { })]
    public static class Patch_CompActivity_ctor
    {
        public static void Postfix(CompActivity __instance)
        {
            if (Settings.StudyAndSuppressByDefault.Enabled())
            {
                __instance.suppressionEnabled = true;
            }
        }
    }
}
