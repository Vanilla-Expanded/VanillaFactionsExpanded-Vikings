using HarmonyLib;
using RimWorld;
using Verse;

namespace VFEV
{

    [StaticConstructorOnStartup]
    public static class HarmonyInit
    {
        static HarmonyInit()
        {
            new Harmony("VFEV.OskarPotocki").PatchAll();
        }
    }

    [HarmonyPatch(typeof(Toils_LayDown))]
    [HarmonyPatch("ApplyBedThoughts", MethodType.Normal)]
    public class PatchToils_LayDown
    {
        [HarmonyPostfix]
        static void PostFix(Pawn actor)
        {
            if (actor.CurrentBed() is Building_Bed bed && actor.needs?.mood?.thoughts?.memories != null && (bed.def.defName == "VFEV_FurBed" || bed.def.defName == "VFEV_DoubleFurBed"))
                actor.needs.mood.thoughts.memories.RemoveMemoriesOfDef(ThoughtDefOf.SleptInCold);
        }
    }

    [HarmonyPatch(typeof(RaceProperties))]
    [HarmonyPatch("IsMechanoid", MethodType.Getter)]
    public class PatchIsMechanoid
    {
        [HarmonyPostfix]
        static void PostFix(ref RaceProperties __instance, ref bool __result)
        {
            if (__instance.body == VFEV_DefOf.VFEV_Odin)
                __result = false;
        }
    }
}