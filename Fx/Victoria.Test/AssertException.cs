using System;

namespace Victoria.Test {
    public class AssertException : Exception {
        
        public AssertException() {}

        protected AssertException(string userMessage)
            : base(userMessage) {
            UserMessage = userMessage;
        }

        protected AssertException(string userMessage, Exception innerException)
            : base(userMessage,innerException) { }

        public string UserMessage { get; set; }
    }
}