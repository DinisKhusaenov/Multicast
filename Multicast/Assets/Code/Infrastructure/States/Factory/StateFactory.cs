using Infrastructure.States.States;
using Zenject;

namespace Infrastructure.States.Factory
{
    public class StateFactory : IStateFactory
    {
        private readonly IInstantiator _instantiator;

        public StateFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public T CreateState<T>() where T : IState
        {
            return _instantiator.Instantiate<T>();
        }
    }
}