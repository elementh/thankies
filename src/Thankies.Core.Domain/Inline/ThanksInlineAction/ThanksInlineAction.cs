using Navigator.Abstraction;
using Navigator.Actions;
using Navigator.Actions.Abstraction;

namespace Thankies.Core.Domain.Inline.ThanksInlineAction
{
    public class ThanksInlineAction : Action
    {
        public override string Type => ActionType.InlineQuery;
        public string? Name { get; protected set; }
        
        public override IAction Init(INavigatorContext ctx)
        {
            Name = string.IsNullOrWhiteSpace(ctx.Update.InlineQuery.Query) ? null : ctx.Update.InlineQuery.Query;

            return this;
        }

        public override bool CanHandle(INavigatorContext ctx)
        {
            return true;
        }

    }
}