package crc64b1a4cca7acb3ab72;


public class SepararPaleteListaDeBaias_RecyclerViewHolder
	extends androidx.recyclerview.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("TelasColetor.Fonte.SepararPaleteListaDeBaias+RecyclerViewHolder, TelasColetor", SepararPaleteListaDeBaias_RecyclerViewHolder.class, __md_methods);
	}


	public SepararPaleteListaDeBaias_RecyclerViewHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == SepararPaleteListaDeBaias_RecyclerViewHolder.class) {
			mono.android.TypeManager.Activate ("TelasColetor.Fonte.SepararPaleteListaDeBaias+RecyclerViewHolder, TelasColetor", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
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
