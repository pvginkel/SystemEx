using System.Text;
using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace SystemEx.Persist
{
    public class Store
    {
        private const string FILENAME = "persist.db";
        private static Store _instance = null;
        private int _version;
        private Dictionary<string, object> _items;
        private StoreItems _storeItems;

        private Store(int version)
        {
            _version = version;
            _items = new Dictionary<string, object>();
            _storeItems = new StoreItems(_items);
        }

        public static event CommitStoreHandler BeforeCommit;

        public static void Initialize(int version)
        {
            _instance = new Store(version);

            _instance.Read();
        }

        public static void Commit()
        {
            if (BeforeCommit != null)
            {
                BeforeCommit(_instance, new CommitStoreEventArgs(_instance._storeItems));
            }

            _instance.Write();
        }

        private void Write()
        {
            var data = new ArrayList();

            data.Add(_version);
            data.Add(_items);

            var bf = new BinaryFormatter();

            using (var fs = File.Create(GetFilename()))
            {
                bf.Serialize(fs, data);

                fs.Close();
            }
        }

        private string GetFilename()
        {
            string path =
                    Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        Assembly.GetExecutingAssembly().GetName().Name
                    );

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return Path.Combine(path, FILENAME);
        }

        private void Read()
        {
            try
            {
                string filename = GetFilename();

                if (!File.Exists(filename))
                {
                    return;
                }

                var bf = new BinaryFormatter();

                object data;

                using (var fs = File.OpenRead(filename))
                {
                    data = bf.Deserialize(fs);

                    fs.Close();
                }

                if (data == null || !(data is ArrayList))
                {
                    return;
                }

                var list = (ArrayList)data;

                if ((int)list[0] != _version)
                {
                    return;
                }

                _items = (Dictionary<string, object>)list[1];
                _storeItems = new StoreItems(_items);
            }
            catch
            {
                // Ignore exceptions
            }
        }

        public static StoreItems Items
        {
            get { return _instance._storeItems; }
        }
    }
}
