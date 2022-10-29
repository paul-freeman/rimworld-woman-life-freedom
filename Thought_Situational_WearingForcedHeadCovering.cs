using System.Collections.Generic;
using RimWorld;
using Verse;

public class Thought_Situational_WearingForcedHeadCovering : Thought_Situational
{
    public override string LabelCap => base.CurStage.label.Formatted(SourceApparelPrecept.apparelDef.label.Named("APPAREL")).CapitalizeFirst();

    public override string Description => base.CurStage.description.Formatted(Find.ActiveLanguageWorker.WithIndefiniteArticlePostProcessed(SourceApparelPrecept.apparelDef.label).Named("APPAREL"), pawn.Named("PAWN")).CapitalizeFirst() + base.CausedByBeliefInPrecept;

    private Precept_Apparel SourceApparelPrecept => (Precept_Apparel)sourcePrecept;

    protected override ThoughtState CurrentStateInternal()
    {
        if (!ModsConfig.IdeologyActive || pawn.Ideo == null)
        {
            return ThoughtState.Inactive;
        }
        List<Precept> allPreceptsAllowingSituationalThought = pawn.Ideo.GetAllPreceptsAllowingSituationalThought(def);
        if (allPreceptsAllowingSituationalThought.Count == 0)
        {
            return ThoughtState.Inactive;
        }
        foreach (Precept item in allPreceptsAllowingSituationalThought)
        {
            if (item is Precept_Apparel precept_Apparel && (precept_Apparel.TargetGender == Gender.None || precept_Apparel.TargetGender == pawn.gender))
            {
                if (!HasApparel(precept_Apparel.apparelDef))
                {
                    continue;
                }
                // iterate through apparel body part groups
                foreach (BodyPartGroupDef bodyPartGroupDef in precept_Apparel.apparelDef.apparel.bodyPartGroups)
                {
                    if (bodyPartGroupDef == BodyPartGroupDefOf.FullHead || bodyPartGroupDef == BodyPartGroupDefOf.UpperHead)
                    {
                        return base.CurrentStateInternal();
                    }
                }
            }
        }
        return ThoughtState.Inactive;
    }

    private bool HasApparel(ThingDef thingDef)
    {
        foreach (Apparel item in pawn.apparel.WornApparel)
        {
            if (item.def == thingDef)
            {
                return true;
            }
        }
        return false;
    }
}