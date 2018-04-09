namespace Pong {
    public class GameObjects {
        private TouchInput touchInput;

        public GameObjects() { }

        public Paddle PlayerPaddle { set; get; }
        public Paddle ComputerPaddle { set; get; }
        public Ball Ball { set; get; }
        
        public TouchInput TouchInput { get => touchInput; set => touchInput = value; }
        public Score Score { get; set; }
    }
}