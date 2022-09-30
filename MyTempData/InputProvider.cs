using Inputs;

namespace InputSystem
{
    public sealed class InputProvider
    {
        private ScreenControl _control;
        
        public ScreenControl Control => _control;
        public MoveDirectionInput MoveDirectionComponent { get; }

        public InputProvider()
        {
            _control = new ScreenControl();
            MoveDirectionComponent = new MoveDirectionInput(_control.MoveDirection);
        }
    }
}