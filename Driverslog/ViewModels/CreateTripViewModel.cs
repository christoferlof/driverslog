using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO.IsolatedStorage;
using Driverslog.Commands;
using Driverslog.Models;

namespace Driverslog.ViewModels {
    public class CreateTripViewModel {

        public CreateTripViewModel() {
            SaveTripCommand = new DelegateCommand(SaveTrip);
        }

        public string From { get; set; }

        public string To { get; set; }

        public int OdometerStart { get; set; }

        public int OdometerStop { get; set; }

        public int Distance { get; private set; }

        public string Notes { get; set; }

        public ICommand SaveTripCommand { get; private set; }

        private void SaveTrip(object obj) {

            //serialize trip
            var path = "trips.json";
            var storage = IsolatedStorageFile.GetUserStoreForApplication();
            
            var file = storage.OpenFile(path, FileMode.OpenOrCreate);
            file.Seek(0, SeekOrigin.Begin);
            
            var serializer = new DataContractJsonSerializer(typeof (TripList));
            var list = serializer.ReadObject(file) as TripList;
            if(list == null) {
                list = new TripList();
            }

            list.Trips.Add(new Trip());

            file.Seek(0,SeekOrigin.Begin);
            serializer.WriteObject(file, list);

            file.Close();
        }
    }
}
