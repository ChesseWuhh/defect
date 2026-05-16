using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;


namespace Defect.Scripts;

public abstract class TemporaryFrostFocusPower : CustomPowerTemplate, ITemporaryPower
{
	private bool _shouldIgnoreNextInstance;

	public override PowerType Type => PowerType.Buff;

	public override PowerStackType StackType => PowerStackType.Counter;

	public abstract AbstractModel OriginModel { get; }

    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
        HoverTipFactory.FromPower<FrostFocusPower>(),
		HoverTipFactory.FromCard(OriginModel as CardModel)
    ];

	public override LocString Title
	{
		get
		{
			AbstractModel originModel = OriginModel;
			if (!(originModel is CardModel cardModel))
			{
				throw new InvalidOperationException();
			}
			return cardModel.TitleLocString;
		}
	}

    public PowerModel InternallyAppliedPower => ModelDb.Power<FrostFocusPower>();

    public void IgnoreNextInstance()
	{
		_shouldIgnoreNextInstance = true;
	}

	public override async Task BeforeApplied(Creature target, decimal amount, Creature? applier, CardModel? cardSource)
	{
		if (_shouldIgnoreNextInstance)
		{
			_shouldIgnoreNextInstance = false;
		}
		else
		{
			await PowerCmd.Apply<FrostFocusPower>(new ThrowingPlayerChoiceContext(), target, (decimal)amount, applier, cardSource, silent: true);
		}
	}

	public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
	{
		if (!(amount == (decimal)base.Amount) && power == this)
		{
			if (_shouldIgnoreNextInstance)
			{
				_shouldIgnoreNextInstance = false;
			}
			else
			{
				await PowerCmd.Apply<FrostFocusPower>(choiceContext, base.Owner, (decimal)amount, applier, cardSource, silent: true);
			}
		}
	}

	public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
	{
		if (side == base.Owner.Side)
		{
			Flash();
			await PowerCmd.Remove(this);
			await PowerCmd.Apply<FrostFocusPower>(choiceContext, base.Owner, - base.Amount, base.Owner, null);
		}
	}
}
