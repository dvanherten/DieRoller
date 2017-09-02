using System;
using System.Linq;

namespace DieRoller
{
    public class RollResult
    {
        public IRollTarget Target { get; }
        public SingleRollResult InitialRollResult { get; }
        public SingleRollResult RerollResult { get; }
        public int Final { get; }
        public int ModifierValue { get; }
        public bool IsSuccessful { get; }

        public RollResult(IRollTarget target, SingleRollResult initialRollResult, SingleRollResult rerollResult, int modifierValue, int final)
        {
            InitialRollResult = initialRollResult ?? throw new ArgumentNullException(nameof(initialRollResult));
            Target = target;
            RerollResult = rerollResult;
            ModifierValue = modifierValue;
            Final = final;
            IsSuccessful = target.GetSuccessfulSides(initialRollResult.Die.TotalSides).Contains(final);
        }

        public override string ToString()
        {
            var rerollText = $"{RerollResult?.SideRolled.ToString() ?? "No Reroll"}";
            var modifyText = ModifierValue == 0 ? "No Modifier" : ModifierValue.ToString();
            return $@"Rolling D{InitialRollResult.Die.TotalSides} - Targeting: {Target}
~~~~~~~~~~~~~~~~~~
Initial: {InitialRollResult.SideRolled}
Reroll: {rerollText}
Modifier: {modifyText}
Final: {Final}
Successful: {IsSuccessful}
~~~~~~~~~~~~~~~~~~
";

        }
    }
}