package crc648e78c68299137e84;


public class DialogLeiaMenu
	extends android.app.DialogFragment
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreateView:(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;:GetOnCreateView_Landroid_view_LayoutInflater_Landroid_view_ViewGroup_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("TelasColetor.Fonte.SeparacaoFracionada.DialogLeiaMenu, TelasColetor", DialogLeiaMenu.class, __md_methods);
	}


	public DialogLeiaMenu ()
	{
		super ();
		if (getClass () == DialogLeiaMenu.class) {
			mono.android.TypeManager.Activate ("TelasColetor.Fonte.SeparacaoFracionada.DialogLeiaMenu, TelasColetor", "", this, new java.lang.Object[] {  });
		}
	}

	public DialogLeiaMenu (java.lang.String p0, java.lang.String p1)
	{
		super ();
		if (getClass () == DialogLeiaMenu.class) {
			mono.android.TypeManager.Activate ("TelasColetor.Fonte.SeparacaoFracionada.DialogLeiaMenu, TelasColetor", "System.String, mscorlib:System.String, mscorlib", this, new java.lang.Object[] { p0, p1 });
		}
	}


	public android.view.View onCreateView (android.view.LayoutInflater p0, android.view.ViewGroup p1, android.os.Bundle p2)
	{
		return n_onCreateView (p0, p1, p2);
	}

	private native android.view.View n_onCreateView (android.view.LayoutInflater p0, android.view.ViewGroup p1, android.os.Bundle p2);

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
