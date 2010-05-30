using System.Collections.ObjectModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Driverslog.Models {
    [DataContract]
    public class ActiveRecord<TEntity> where TEntity : ActiveRecord<TEntity>, new() {

        private static readonly string FileName =  typeof(TEntity).Name.ToLower() + "s.json"; //lame pluralization.. 

        private static readonly ObservableCollection<TEntity> _all = new ObservableCollection<TEntity>();

        public static ObservableCollection<TEntity> All {
            get { return _all; }
        }

        public static void Persist() {
            var data = new DataContainer<TEntity>();
            foreach (var trip in All) {
                data.Records.Add(trip);
            }
            WriteObject(data);
        }

        private static void WriteObject(DataContainer<TEntity> list) {
            using (var file = GetFile(FileMode.Truncate)) {
                GetSerializer().WriteObject(file, list);
            }
        }

        private static DataContractJsonSerializer GetSerializer() {
            return new DataContractJsonSerializer(typeof(DataContainer<TEntity>));
        }

        private static IsolatedStorageFileStream GetFile(FileMode fileMode) {
            var file = IsolatedStorageFile
                .GetUserStoreForApplication()
                .OpenFile(FileName, fileMode);
            return file;
        }

        public static void Load() {
            using (var file = GetFile(FileMode.OpenOrCreate)) {
                All.Clear();
                var list = ReadObject(file);
                if (list != null) {
                    list.Records.ForEach(t => All.Add(t));
                }
            }
        }

        private static DataContainer<TEntity> ReadObject(Stream file) {
            var serializer = GetSerializer();
            return serializer.ReadObject(file) as DataContainer<TEntity>;
        }

    }
}