namespace WindowsPhoneClassLibrary1 {
    public class TrueException : AssertException {
        public TrueException() : base("Assert.True failed") { }
    }
}