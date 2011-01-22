using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Victoria.Data;

namespace Driverslog.Models {
    
    [DataContract]
    public class Expense : ActiveRecord<Expense>, IHaveId {
        public Expense() {
            Id = Guid.NewGuid();
            ValidationMessages = new Dictionary<string, string>();
        }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Car { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public string Notes { get; set; }

        [DataMember]
        public double Amount { get; set; }

        public bool IsValid() {
            if (string.IsNullOrEmpty(Title)) {
                ValidationMessages.Add("Title", "You must specify a title of the expense");
            }
            return ValidationMessages.Count == 0;
        }

        [IgnoreDataMember]
        public Dictionary<string, string> ValidationMessages { get; set; }

    }
}