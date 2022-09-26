namespace InputSystem
{
    public class InputProvider
    {
        private CharacterControl _characterControl;
        private CharacterControl.CharacterActions _character;

        public void MoveDirection()
        {
            _character = _characterControl.Character;
        }
    }
}