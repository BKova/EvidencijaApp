package md511d06db65f8bd813bfe0895f0347a3fe;


public class NetworkStatusListener
	extends android.content.BroadcastReceiver
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onReceive:(Landroid/content/Context;Landroid/content/Intent;)V:GetOnReceive_Landroid_content_Context_Landroid_content_Intent_Handler\n" +
			"";
		mono.android.Runtime.register ("EvidencijaAndroidClient.Resources.repo.NetworkStatusListener, EvidencijaAndroidClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", NetworkStatusListener.class, __md_methods);
	}


	public NetworkStatusListener () throws java.lang.Throwable
	{
		super ();
		if (getClass () == NetworkStatusListener.class)
			mono.android.TypeManager.Activate ("EvidencijaAndroidClient.Resources.repo.NetworkStatusListener, EvidencijaAndroidClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public NetworkStatusListener (boolean p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == NetworkStatusListener.class)
			mono.android.TypeManager.Activate ("EvidencijaAndroidClient.Resources.repo.NetworkStatusListener, EvidencijaAndroidClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "System.Boolean, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0 });
	}


	public void onReceive (android.content.Context p0, android.content.Intent p1)
	{
		n_onReceive (p0, p1);
	}

	private native void n_onReceive (android.content.Context p0, android.content.Intent p1);

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
