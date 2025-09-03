namespace Infrastructure.States.States
{
    public interface IState
    {
        void Enter();
        void Exit();
    }
}
