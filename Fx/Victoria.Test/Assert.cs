namespace Victoria.Test {
    public class Assert {
        public static void True(bool condition) {
            if(!condition) throw new TrueException();
        }
    }
}