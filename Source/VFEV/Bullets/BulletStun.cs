using RimWorld;
using Verse;

namespace VFEV
{
    internal class BulletStun : Bullet
    {
        protected override void Impact(Thing hitThing, bool blockedByShield = false)
        {
            if (Rand.RangeInclusive(0, 100) >= 50 && hitThing is Pawn pawn)
            {
                pawn.stances.stunner.StunFor(250, null, false, true);
            }
            base.Impact(hitThing);
        }
    }
}