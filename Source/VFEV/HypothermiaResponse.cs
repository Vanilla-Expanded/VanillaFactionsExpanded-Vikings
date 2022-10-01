using RimWorld;
using Verse;
using Verse.AI;

namespace VFEV
{
    internal class JobDriver_HypothermiaResponse : JobDriver_LayDown
    {
        public override string GetReport() => "Recorvering from hypothermia";
    }

    internal class JobGiver_HypothermiaResponse : ThinkNode_JobGiver
    {
        protected override Job TryGiveJob(Pawn pawn)
        {
            if (pawn.InMentalState)
            {
                return null;
            }
            if (pawn.Map == null)
            {
                return null;
            }
            if (HealthAIUtility.ShouldSeekMedicalRest(pawn))
            {
                return null;
            }
            if (RestUtility.DisturbancePreventsLyingDown(pawn))
            {
                return null;
            }
            if (pawn.CurJobDef != null && pawn.CurJobDef == VFEV_DefOf.VFEV_HypothermiaResponse)
            {
                return null;
            }
            if (!pawn.health.hediffSet.HasHediff(HediffDefOf.Hypothermia, false))
            {
                return null;
            }
            if (pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.Hypothermia, false) is Hediff hediffHypothermia && hediffHypothermia.Severity >= 0.35f && FindFurBedOf(pawn, out Building_Bed bed))
            {
                return new Job(VFEV_DefOf.VFEV_HypothermiaResponse, bed);
            }
            return null;
        }

        private bool FindFurBedOf(Pawn sleeper, out Building_Bed bed)
        {
            bed = sleeper.ownership?.OwnedBed;

            if (bed == null || (bed.def.defName != "VFEV_FurBed" && bed.def.defName != "VFEV_DoubleFurBed"))
            {
                bed = null;
                return false;
            }

            return true;
        }
    }

    internal class ThinkNode_HypothermiaResponse : ThinkNode_Conditional
    {
        protected override bool Satisfied(Pawn pawn)
        {
            return (pawn.IsColonistPlayerControlled && pawn.health.capacities.CapableOf(PawnCapacityDefOf.Moving))
                && !pawn.Downed
                && !pawn.IsBurning()
                && !pawn.InMentalState
                && !pawn.Drafted
                && pawn.Awake()
                && !HealthAIUtility.ShouldSeekMedicalRest(pawn);
        }
    }
}