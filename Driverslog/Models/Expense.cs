using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Victoria.Data;

namespace Driverslog.Models {
    
    [DataContract]
    public class Expense : LogItem<Expense> {
        
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public double Amount { get; set; }

        protected override void OnValidating() {
            if (string.IsNullOrEmpty(Title)) {
                ValidationMessages.Add("Title", "You must specify a title of the expense");
            }
        }
        
    }
}