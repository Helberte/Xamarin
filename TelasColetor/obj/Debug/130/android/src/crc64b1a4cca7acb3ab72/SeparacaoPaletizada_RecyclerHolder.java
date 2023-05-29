package crc64b1a4cca7acb3ab72;


public class SeparacaoPaletizada_RecyclerHolder
	extends androidx.recyclerview.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("TelasColetor.Fonte.SeparacaoPaletizada+RecyclerHolder, TelasColetor", SeparacaoPaletizada_RecyclerHolder.class, __md_methods);
	}


	public SeparacaoPaletizada_RecyclerHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == SeparacaoPaletizada_RecyclerHolder.class) {
			mono.android.TypeManager.Activate ("TelasColetor.Fonte.SeparacaoPaletizada+RecyclerHolder, TelasColetor", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
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
