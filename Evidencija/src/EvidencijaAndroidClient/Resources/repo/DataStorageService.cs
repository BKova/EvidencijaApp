using Android.Content;
using Newtonsoft.Json;
using System;

namespace EvidencijaAndroidClient.Resources.repo
{
    public static class DataStorageService
    {
        public static void StoreData<T>(T Object, string fileName, Context context)
        {
            string data = JsonConvert.SerializeObject(Object);
            var preference = context.GetSharedPreferences("EvidencijaApp", FileCreationMode.Private);
            var Editor = preference.Edit();
            Editor.PutString(fileName, data);
            Editor.Commit();
        }

        public static T LoadData<T>(string fileName, Context context)
        {
            var preference = context.GetSharedPreferences("EvidencijaApp", FileCreationMode.Private);
            string data = preference.GetString(fileName, null);
            if (data == null) return default(T);

            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}