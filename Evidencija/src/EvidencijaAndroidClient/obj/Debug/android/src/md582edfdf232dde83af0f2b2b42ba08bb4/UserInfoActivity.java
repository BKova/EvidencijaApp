package md582edfdf232dde83af0f2b2b42ba08bb4;


public class UserInfoActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("EvidencijaAndroidClient.Activities.UserInfoActivity, EvidencijaAndroidClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", UserInfoActivity.class, __md_methods);
	}


	public UserInfoActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == UserInfoActivity.class)
			mono.android.TypeManager.Activate ("EvidencijaAndroidClient.Activities.UserInfoActivity, EvidencijaAndroidClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
