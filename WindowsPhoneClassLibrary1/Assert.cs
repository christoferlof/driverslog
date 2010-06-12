using System;

namespace WindowsPhoneClassLibrary1 {
    public class Assert {
        public static void True(bool condition) {
            if(!condition) throw new ArgumentException("Condition is not true");
        }
    }
}