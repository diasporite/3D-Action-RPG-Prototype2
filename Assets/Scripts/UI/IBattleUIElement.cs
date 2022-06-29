namespace RPG_Project
{
    public interface IBattleUIElement
    {
        void InitUI(PartyController party);

        void SubscribeToDelegates();
        void UnsubscribeFromDelegates();
    }
}