namespace Turns
{
    public sealed class PassiveTurnMaker : TurnMaker
    {
        public override bool CanMakeTurn => true;
        public override void MakeTurn() { }
    }
}