using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Victoria.Data;

namespace Driverslog.Models {

    [DataContract]
    public class Expense : LogItem<Expense> {
        
        private string _title;

        [DataMember]
        public string Title {
            get { return _title; }
            set {
                _title = value;
                Notify(() => Title);
            }
        }

        private double _amount;

        [DataMember]
        public double Amount {
            get { return _amount; }
            set {
                _amount = value;
                Notify(() => Amount);
                Notify(() => AmountWithCurrency);
            }
        }

        [IgnoreDataMember]
        public string AmountWithCurrency {
            get{ return Amount.ToString("c"); }
        }

        protected override void OnValidating() {
            if (string.IsNullOrEmpty(Title)) {
                ValidationMessages.Add("Title", "You must specify a title of the expense");
            }
        }

    }
}