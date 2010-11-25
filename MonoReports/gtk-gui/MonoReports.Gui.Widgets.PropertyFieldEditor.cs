
// This file has been generated by the GUI designer. Do not modify.
namespace MonoReports.Gui.Widgets
{
	public partial class PropertyFieldEditor
	{
		private global::Gtk.Table table1;
		private global::Gtk.ComboBox combobox1;
		private global::Gtk.Label dafValueLabel;
		private global::Gtk.Entry defaultValueEntry;
		private global::Gtk.Label label1;
		private global::Gtk.Label label3;
		private global::Gtk.Entry nameEntry;
		private global::Gtk.Button buttonCancel;
		private global::Gtk.Button buttonOk;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget MonoReports.Gui.Widgets.PropertyFieldEditor
			this.Events = ((global::Gdk.EventMask)(1896448));
			this.ExtensionEvents = ((global::Gdk.ExtensionMode)(1));
			this.Name = "MonoReports.Gui.Widgets.PropertyFieldEditor";
			this.Title = global::Mono.Unix.Catalog.GetString ("Edit Property");
			this.Icon = global::Stetic.IconLoader.LoadIcon (this, "gtk-index", global::Gtk.IconSize.Menu);
			this.WindowPosition = ((global::Gtk.WindowPosition)(2));
			this.Modal = true;
			this.BorderWidth = ((uint)(3));
			this.Resizable = false;
			this.DefaultWidth = 200;
			this.DestroyWithParent = true;
			// Internal child MonoReports.Gui.Widgets.PropertyFieldEditor.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog1_VBox";
			w1.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.table1 = new global::Gtk.Table (((uint)(3)), ((uint)(2)), false);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.combobox1 = global::Gtk.ComboBox.NewText ();
			this.combobox1.Name = "combobox1";
			this.table1.Add (this.combobox1);
			global::Gtk.Table.TableChild w2 = ((global::Gtk.Table.TableChild)(this.table1 [this.combobox1]));
			w2.TopAttach = ((uint)(2));
			w2.BottomAttach = ((uint)(3));
			w2.LeftAttach = ((uint)(1));
			w2.RightAttach = ((uint)(2));
			w2.XOptions = ((global::Gtk.AttachOptions)(4));
			w2.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.dafValueLabel = new global::Gtk.Label ();
			this.dafValueLabel.Name = "dafValueLabel";
			this.dafValueLabel.LabelProp = global::Mono.Unix.Catalog.GetString ("Deault Value");
			this.table1.Add (this.dafValueLabel);
			global::Gtk.Table.TableChild w3 = ((global::Gtk.Table.TableChild)(this.table1 [this.dafValueLabel]));
			w3.TopAttach = ((uint)(1));
			w3.BottomAttach = ((uint)(2));
			w3.XOptions = ((global::Gtk.AttachOptions)(4));
			w3.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.defaultValueEntry = new global::Gtk.Entry ();
			this.defaultValueEntry.CanFocus = true;
			this.defaultValueEntry.Name = "defaultValueEntry";
			this.defaultValueEntry.IsEditable = true;
			this.defaultValueEntry.InvisibleChar = '•';
			this.table1.Add (this.defaultValueEntry);
			global::Gtk.Table.TableChild w4 = ((global::Gtk.Table.TableChild)(this.table1 [this.defaultValueEntry]));
			w4.TopAttach = ((uint)(1));
			w4.BottomAttach = ((uint)(2));
			w4.LeftAttach = ((uint)(1));
			w4.RightAttach = ((uint)(2));
			w4.XOptions = ((global::Gtk.AttachOptions)(4));
			w4.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label1 = new global::Gtk.Label ();
			this.label1.Name = "label1";
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString ("Name");
			this.label1.Justify = ((global::Gtk.Justification)(1));
			this.table1.Add (this.label1);
			global::Gtk.Table.TableChild w5 = ((global::Gtk.Table.TableChild)(this.table1 [this.label1]));
			w5.XOptions = ((global::Gtk.AttachOptions)(4));
			w5.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label3 = new global::Gtk.Label ();
			this.label3.Name = "label3";
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString ("Type");
			this.table1.Add (this.label3);
			global::Gtk.Table.TableChild w6 = ((global::Gtk.Table.TableChild)(this.table1 [this.label3]));
			w6.TopAttach = ((uint)(2));
			w6.BottomAttach = ((uint)(3));
			w6.XOptions = ((global::Gtk.AttachOptions)(4));
			w6.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.nameEntry = new global::Gtk.Entry ();
			this.nameEntry.CanFocus = true;
			this.nameEntry.Events = ((global::Gdk.EventMask)(65536));
			this.nameEntry.ExtensionEvents = ((global::Gdk.ExtensionMode)(1));
			this.nameEntry.Name = "nameEntry";
			this.nameEntry.IsEditable = true;
			this.nameEntry.MaxLength = 512;
			this.nameEntry.InvisibleChar = '•';
			this.table1.Add (this.nameEntry);
			global::Gtk.Table.TableChild w7 = ((global::Gtk.Table.TableChild)(this.table1 [this.nameEntry]));
			w7.LeftAttach = ((uint)(1));
			w7.RightAttach = ((uint)(2));
			w7.YOptions = ((global::Gtk.AttachOptions)(4));
			w1.Add (this.table1);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(w1 [this.table1]));
			w8.Position = 0;
			w8.Expand = false;
			w8.Fill = false;
			// Internal child MonoReports.Gui.Widgets.PropertyFieldEditor.ActionArea
			global::Gtk.HButtonBox w9 = this.ActionArea;
			w9.Name = "dialog1_ActionArea";
			w9.Spacing = 10;
			w9.BorderWidth = ((uint)(5));
			w9.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonCancel = new global::Gtk.Button ();
			this.buttonCancel.CanDefault = true;
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseStock = true;
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = "gtk-cancel";
			this.AddActionWidget (this.buttonCancel, -6);
			global::Gtk.ButtonBox.ButtonBoxChild w10 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w9 [this.buttonCancel]));
			w10.Expand = false;
			w10.Fill = false;
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonOk = new global::Gtk.Button ();
			this.buttonOk.CanDefault = true;
			this.buttonOk.CanFocus = true;
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.UseStock = true;
			this.buttonOk.UseUnderline = true;
			this.buttonOk.Label = "gtk-ok";
			this.AddActionWidget (this.buttonOk, -5);
			global::Gtk.ButtonBox.ButtonBoxChild w11 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w9 [this.buttonOk]));
			w11.Position = 1;
			w11.Expand = false;
			w11.Fill = false;
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultHeight = 178;
			this.Show ();
			this.nameEntry.Changed += new global::System.EventHandler (this.OnNameEntryChanged);
		}
	}
}
