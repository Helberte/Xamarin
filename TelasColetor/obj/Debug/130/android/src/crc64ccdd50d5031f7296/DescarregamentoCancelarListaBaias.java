package crc64ccdd50d5031f7296;


public class DescarregamentoCancelarListaBaias
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
		mono.android.Runtime.register ("TelasColetor.Fonte.Descarregamento.DescarregamentoCancelarListaBaias, TelasColetor", DescarregamentoCancelarListaBaias.class, __md_methods);
	}


	public DescarregamentoCancelarListaBaias ()
	{
		super ();
		if (getClass () == DescarregamentoCancelarListaBaias.class) {
			mono.android.TypeManager.Activate ("TelasColetor.Fonte.Descarregamento.DescarregamentoCancelarListaBaias, TelasColetor", "", this, new java.lang.Object[] {  });
		}
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
