using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;

namespace TestDLL
{
    public class Test<T>
    {
        private T data;

        public void Set(T indata)
        {
            if (indata == null)
                throw new ArgumentNullException("Indata is null");
            data = indata;
        }

        public bool GetIntegerStatus()
        {
            if (data.ToString() == null)
                throw new ArgumentNullException("Data is null");
            else
            {
                int tmp = 0;
                return int.TryParse(data.ToString(), out tmp);
            }
        }

        public bool GetDoubleStatus()
        {
            if (data.ToString() == null)
                throw new ArgumentNullException("Data is null");
            else
            {
                double tmp = 0;
                return double.TryParse(data.ToString(), out tmp);
            }
        }
        
        public bool GetEmailStatus()
        {
            if (data.ToString() == null)
                throw new ArgumentNullException("Data is null");
            else
            {
                var reg = new Regex("^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$");
                return reg.IsMatch(data.ToString()); 
            }
        }

        public string PrintMainMetaInfo()
        {
            if (data == null)
                throw new ArgumentNullException("Data is null");
            return "\nName " + data.GetType().FullName + "\nVisible " + data.GetType().IsVisible + "\nSerializable " + data.GetType().IsSerializable;
        }

        public void SerializeObj(string indata)
        {
            if (data == null)
                throw new ArgumentNullException("Data is null");
            if (!data.GetType().IsSerializable)
                throw new ArgumentException("Data is not serializable");
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream(indata, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, data);
            }
        }

        public void DeserializeObj(string indata)
        {
            if (!File.Exists(indata))
                throw new IOException("File not exist");
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream(indata, FileMode.Open))
            {
                data = (T)formatter.Deserialize(fs);
            }
        }
    }
}
