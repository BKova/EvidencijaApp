using Android.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace EvidencijaAndroidClient.Resources.repo
{
    static class DataStorageService
    {
        public static void StoreData(object data)
        {
            XmlSerializer serializer = new XmlSerializer(data.GetType());
            string textdata;
            using (StringWriter stringWriter = new StringWriter())
            using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter))
            {
                serializer.Serialize(xmlWriter, data);
                textdata = stringWriter.ToString();
            }
            string path = Android.OS.Environment.DataDirectory.AbsolutePath;
            string filePath = Path.Combine(path, "EvidencijaApp/data.xml");
            using (var file = File.Open(filePath, FileMode.OpenOrCreate, FileAccess.Write))
            using (var streamWriter = new StreamWriter(file))
                streamWriter.Write(textdata);
        }

        public static object LoadData(Type t)
        {
            object data = new object();
            string path = Android.OS.Environment.DataDirectory.AbsolutePath;
            string filePath = Path.Combine(path, "EvidencijaApp/data.xml");
            if(!File.Exists(filePath))
            {
                File.Create(filePath);
            }
            try
            {
                using (var file = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    XmlSerializer serializer = new XmlSerializer(t);
                    data = serializer.Deserialize(file);
                }
            }
            catch(Exception ex)
            {
                Log.Warn("FileLoad",ex.Message);
            }
            return data;
        }
    }
}