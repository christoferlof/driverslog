using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;

namespace Driverslog.Models {
    public class TripAndExpenseCollection : ObservableCollection<object> {

        private static TripAndExpenseCollection _current;
        public static TripAndExpenseCollection Current {
            get { return _current ?? (_current = new TripAndExpenseCollection()); }
        }

        public TripAndExpenseCollection() {
            RegisterListeners();
        }

        private void RegisterListeners() {
            RegisterListener(Trip.All);
            RegisterListener(Expense.All);
        }

        private void RegisterListener<T>(ObservableCollection<T> collection) {
            
            collection.CollectionChanged += (s, e) => {
                switch (e.Action) {
                    case NotifyCollectionChangedAction.Add:
                        if (e.NewItems == null) return;
                        foreach (var item in e.NewItems) {
                            Add(item);
                        }
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        if (e.OldItems == null) return;
                        foreach (var item in e.OldItems) {
                            Remove(item);
                        }
                        break;
                }
            };
        }
    }
}