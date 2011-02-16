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

        public static bool HasRecords {
            get {
                return All != null && All.Count > 0;
            }
        }

        protected LogItem() {
            Id      = Guid.NewGuid();
            Date    = DateTime.Now.Date;
            Car     = Setting.Current.DefaultCar;
            EnsureValidationMessagesCollection();
        }

        private Guid _id;

        [DataMember]
        public Guid Id {
            get { return _id; }
            set {
                _id = value;
                Notify(() => Id);
            }
        }

        private string _notes;

        [DataMember]
        public string Notes {
            get { return _notes; }
            set {
                _notes = value;
                Notify(() => Notes);
            }
        }

        private string _car;

        [DataMember]
        public string Car {
            get { return _car; }
            set {
                _car = value;
                Notify(() => Car);
            }
        }

        private DateTime _date;

        [DataMember]
        public DateTime Date {
            get { return _date; }
            set {
                _date = value;
                Notify(() => Date);
            }
        }

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

        protected virtual void OnValidating() { }

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