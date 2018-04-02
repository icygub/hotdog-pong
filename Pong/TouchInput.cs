namespace Pong
{
    public class TouchInput
    {
        private bool _up;
        private bool _down;

        public TouchInput()
        {

        }

        public bool Up { get => _up; set => _up = value; }
        public bool Down { get => _down; set => _down = value; }
    }
}