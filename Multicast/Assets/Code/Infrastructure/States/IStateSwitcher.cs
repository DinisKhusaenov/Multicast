using Infrastructure.States.States;

namespace Infrastructure.StateMachine
{
    public interface IStateSwitcher
    {
        void SwitchState<State>() where State : IState;
    }
}
