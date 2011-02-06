using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Victoria.Data;

namespace Driverslog.Models {
    
    [DataContract]
    public class Expense : ActiveRecord<Expense>, IHaveId, ICanHaveValidationErrors {
        public Expense() {
            Id      = Guid.NewGuid();
            Date    = DateTime.Now.Date;
            EnsureValidationMessagesCollection();
        }

        [OnDeserializing]
        public void OnDeserializing(StreamingContext context) {
            EnsureValidationMessagesCollection();
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

        private void EnsureValidationMessagesCollection() {
            ValidationMessages = new Dictionary<string, string>();
        }

        public bool IsValid() {
            ValidationMessages.Clear();

            if (string.IsNullOrEmpty(Title)) {
                ValidationMessages.Add("Title", "You must specify a title of the expense");
            }
            return ValidationMessages.Count == 0;
        }

        [IgnoreDataMember]
        public Dictionary<string, string> ValidationMessages { get; private set; }

    }
}