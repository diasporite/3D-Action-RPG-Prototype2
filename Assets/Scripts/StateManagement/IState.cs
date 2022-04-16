namespace RPG_Project
{
    public interface IState
    {
        void Enter(params object[] args);
        void ExecuteFrame();
        void ExecuteFrameFixed();
        void ExecuteFrameLate();
        void Exit();
    }
}