using RimWorld;
using Verse;

namespace VFEV
{
    internal class CryptoBullet : Bullet
    {
        protected override void Impact(Thing hitThing, bool blockedByShield = false)
        {
            if (hitThing is Pawn hitPawn && hitPawn != null && hitPawn.RaceProps.FleshType == FleshTypeDefOf.Normal)
            {
                if (hitPawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.Hypothermia) is Hediff hypo && hypo != null)
                {
                    hypo.Severity += 0.1f;
                }
                else
                {
                    hitPawn.health.AddHediff(HediffDefOf.Hypothermia);
                    hitPawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.Hypothermia).Severity = 0.2f;
                }
            }
            base.Impact(hitThing);
        }
    }

    internal class DamageWorker_CryptoCut : DamageWorker_Cut
    {
        public override DamageResult Apply(DamageInfo dinfo, Thing thing)
        {
            if (thing is Pawn hitPawn && hitPawn != null && hitPawn.RaceProps.FleshType == FleshTypeDefOf.Normal)
            {
                if (hitPawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.Hypothermia) is Hediff hypo && hypo != null)
                {
                    hypo.Severity += 0.1f;
                }
                else
                {
                    hitPawn.health.AddHediff(HediffDefOf.Hypothermia);
                    hitPawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.Hypothermia).Severity = 0.2f;
                }
            }
            return base.Apply(dinfo, thing);
        }
    }
}