using RimWorld;
using Verse;

public class ThoughtWorker_Precept_WearingForcedHeadCovering : ThoughtWorker_Precept
{
    protected override ThoughtState ShouldHaveThought(Pawn pawn)
    {
        return pawn.gender == Gender.Female;
    }
}