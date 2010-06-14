namespace Victoria.Test {
    public class TrueException : AssertException {
        public TrueException() : base("Assert.True failed") { }
    }
}