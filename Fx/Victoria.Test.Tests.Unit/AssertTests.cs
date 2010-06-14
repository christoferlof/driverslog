using System;

namespace Victoria.Test.Tests.Unit {
    public class AssertTests {
        
        //test using exceptions - the other tests are based on this one so make sure it works without using the fx.. 
        public void TestTrueThrows() {
            try {
                Assert.True(false);
            } catch(Exception ex) {
                var actual = (ex is TrueException);
                if(!actual)
                    throw new InvalidProgramException("Assert.True doesn't throw a TrueException when false is passed in as argument");
                return;
            }
            throw new InvalidProgramException("Assert.True doesn't throw any exception when false is passed in as argument");
        }

        public void TestTrueDoesntThrow() {
            Assert.True(true);
        }

        public void TestEqualThrowsOnIntegers() {
            try {
                Assert.Equal(1,2);
            } catch(Exception ex) {
                Assert.True(ex is EqualException);
                return;
            }
            Assert.True(false);
        }

        public void TestEqualDoesntThrowOnIntegers() {
            Assert.Equal(1,1);            
        }

    }
}