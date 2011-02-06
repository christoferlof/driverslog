using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Caliburn.Micro;
using Victoria.Data;

namespace Driverslog.Models {

    [DataContract]
    public abstract class LogItem<T> : ActiveRecord<T>, IHaveId, ICanHaveValidationErrors, INotifyPropertyChanged where T : ActiveRecord<T>, new() {

        protected LogItem() {
            Id      = Guid.NewGuid();
            Date    = DateTime.Now.Date;
            EnsureValidationMessagesCollection();
        }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Notes { get; set; }

        [DataMember]
        public string Car { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [OnDeserializing]
        public void OnDeserializing(StreamingContext context) {
            EnsureValidationMessagesCollection();
        }

        private void EnsureValidationMessagesCollection() {
            if (ValidationMessages == null) { 
                ValidationMessages = new Dictionary<string, string>();
            }
        }

        public bool IsValid() {
            ValidationMessages.Clear();
            OnValidating();
            return ValidationMessages.Count == 0;
        }

        protected virtual void OnValidating() {}

        [IgnoreDataMember]
        public Dictionary<string, string> ValidationMessages { get; private set; }

        protected void Notify<TProperty>(Expression<Func<TProperty>> property) {
            var name = property.GetMemberInfo().Name;
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}