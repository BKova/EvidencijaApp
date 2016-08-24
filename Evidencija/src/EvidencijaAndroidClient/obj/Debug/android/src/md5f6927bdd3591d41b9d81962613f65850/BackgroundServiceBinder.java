package md5f6927bdd3591d41b9d81962613f65850;


public class BackgroundServiceBinder
	extends android.os.Binder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("EvidencijaAndroidClient.BackgroundServiceBinder, EvidencijaAndroidClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", BackgroundServiceBinder.class, __md_methods);
	}


	public BackgroundServiceBinder () throws java.lang.Throwable
	{
		super ();
		if (getClass () == BackgroundServiceBinder.class)
			mono.android.TypeManager.Activate ("EvidencijaAndroidClient.BackgroundServiceBinder, EvidencijaAndroidClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public BackgroundServiceBinder (md5f6927bdd3591d41b9d81962613f65850.BackgroundService p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == BackgroundServiceBinder.class)
			mono.android.TypeManager.Activate ("EvidencijaAndroidClient.BackgroundServiceBinder, EvidencijaAndroidClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "EvidencijaAndroidClient.BackgroundService, EvidencijaAndroidClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
