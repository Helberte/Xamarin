package crc645d24e28d00d9eb3f;


public class MainClassApplication
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("TelasColetor.MainClassApplication, TelasColetor", MainClassApplication.class, __md_methods);
	}


	public MainClassApplication ()
	{
		super ();
		if (getClass () == MainClassApplication.class) {
			mono.android.TypeManager.Activate ("TelasColetor.MainClassApplication, TelasColetor", "", this, new java.lang.Object[] {  });
		}
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
