using Infrastructure.States.States;

namespace Infrastructure.States.Factory
{
    public interface IStateFactory
    {
        T CreateState<T>() where T : IState;
    }
}