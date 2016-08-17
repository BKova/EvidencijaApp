using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EvidencijaAndroidClient.Resources.models;
using EvidencijaAndroidClient.Resources.repo;

namespace EvidencijaAndroidClient
{
    [Application]
    class EvidencijaApplication : Application
    {
        public UserInfo userInfo { get; set; }

        public ConnectionState connectionState { get; set; }

        public ConnectionSettings connectionSettings { get; set; }

        public EvidencijaApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {

        }
    }
}