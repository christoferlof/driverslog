using System;

namespace Victoria.Test.Runner {
    public abstract class OutputWriter {
    
        public abstract void Write(string message);
    
    }

    public class ConsoleOutputWriter : OutputWriter {
        public override void Write(string message) {
            Console.WriteLine(message);
        }
    }
}