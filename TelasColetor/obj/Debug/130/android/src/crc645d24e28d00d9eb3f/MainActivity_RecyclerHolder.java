package crc645d24e28d00d9eb3f;


public class MainActivity_RecyclerHolder
	extends androidx.recyclerview.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("TelasColetor.MainActivity+RecyclerHolder, TelasColetor", MainActivity_RecyclerHolder.class, __md_methods);
	}


	public MainActivity_RecyclerHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == MainActivity_RecyclerHolder.class) {
			mono.android.TypeManager.Activate ("TelasColetor.MainActivity+RecyclerHolder, TelasColetor", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
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
