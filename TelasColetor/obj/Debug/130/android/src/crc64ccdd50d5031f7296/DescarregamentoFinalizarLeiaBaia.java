package crc64ccdd50d5031f7296;


public class DescarregamentoFinalizarLeiaBaia
	extends crc645d24e28d00d9eb3f.MainClassApplication
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("TelasColetor.Fonte.Descarregamento.DescarregamentoFinalizarLeiaBaia, TelasColetor", DescarregamentoFinalizarLeiaBaia.class, __md_methods);
	}


	public DescarregamentoFinalizarLeiaBaia ()
	{
		super ();
		if (getClass () == DescarregamentoFinalizarLeiaBaia.class) {
			mono.android.TypeManager.Activate ("TelasColetor.Fonte.Descarregamento.DescarregamentoFinalizarLeiaBaia, TelasColetor", "", this, new java.lang.Object[] {  });
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
