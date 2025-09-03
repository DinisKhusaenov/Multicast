using System.Collections.Generic;
using System.Linq;
using Infrastructure.StateMachine;
using Infrastructure.States.Factory;
using Infrastructure.States.States;
using Zenject;

namespace Infrastructure.States
{
    public class ApplicationStateMachine : IStateSwitcher, IInitializable
    {
        private readonly IStateFactory _stateFactory;

        private List<IState> _states;
        private IState _currentState;

        public ApplicationStateMachine(IStateFactory stateFactory)
        {
            _stateFactory = stateFactory;
        }

        public void SwitchState<State>() where State : IState
        {
            IState state = _states.FirstOrDefault(state => state is State);

            _currentState.Exit();
            _currentState = state;
            _currentState.Enter();
        }

        public void Initialize()
        {
            _states = new List<IState>
            {
                _stateFactory.CreateState<InitializeState>(),
                _stateFactory.CreateState<MenuState>(),
                _stateFactory.CreateState<GameState>()
            };

            _currentState = _states[0];
            _currentState.Enter();
        }
    }
}
