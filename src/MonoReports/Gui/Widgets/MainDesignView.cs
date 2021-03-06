// 
// MainDesignView.cs
//  
// Author:
//       Tomasz Kubacki <Tomasz.Kubacki (at) gmail.com>
// 
// Copyright (c) 2010 Tomasz Kubacki 2010
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using Gtk;
using MonoReports.Services;
using MonoReports.Core;
using MonoReports.ControlView;
using MonoReports.Model.Engine;
using MonoReports.Model;
using Cairo;
using Gdk;
using MonoReports.Extensions.CairoExtensions;
using System.Reflection;
using System.Collections.Generic;
using MonoReports.Model.Data;

namespace MonoReports.Gui.Widgets
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class MainDesignView : Gtk.Bin
	{
		DesignService designService;

		public DesignService DesignService {
			get {
				return this.designService;
			}
			set {
				
				designService = value;
				if(designService != null) {
					designService.OnReportChanged += HandleDesignServiceOnReportChanged;
					 codeTextview.Buffer.Text = designService.Report.DataScript;
				}
			}
		}
		
		public CompilerService Compiler {get;set;}

		void HandleDesignServiceOnReportChanged (object sender, EventArgs e)
		{
		   codeTextview.Buffer.Text = designService.Report.DataScript;
		}

		ReportRenderer reportRenderer;

		public ReportRenderer ReportRenderer {
			get {
				return this.reportRenderer;
			}
			set {
				reportRenderer = value;
			}
		}

		int pageNumber = 0;
		IWorkspaceService workSpaceService;

		public IWorkspaceService WorkSpaceService {
			get {
				return this.workSpaceService;
			}
			set {
				workSpaceService = value;
			}
		}

		ToolBarSpinButton pageSpinButton = null;
		ReportEngine reportEngine;

		public DrawingArea DesignDrawingArea { 
			get { return drawingarea;}
		}

		public DrawingArea PreviewDrawingArea { 
			get { return previewDrawingArea;}
		}
		
		static Cairo.Color backgroundPageColor = new Cairo.Color(1,1,1);

		public MainDesignView ()
		{
			this.Build ();			
			buildPreviewToolbar ();
			
			Gtk.Drag.DestSet (DesignDrawingArea, DestDefaults.All, new TargetEntry[]{new TargetEntry ("Field", TargetFlags.OtherWidget,2)}, DragAction.Copy);
			
		
				
			DesignDrawingArea.DragDrop += delegate(object o, DragDropArgs args) {
					var source = Gtk.Drag.GetSourceWidget (args.Context);
						if (source.GetType () == typeof(TreeView)){
						TreeIter item;
						TreeView treeView = ((TreeView)source) as TreeView;
						treeView.Selection.GetSelected (out item);
					    var model = treeView.Model as Gtk.TreeStore;					
						var wrapper = model.GetValue (item, 0) as TreeItemWrapper;	 					
						Gtk.Drag.Finish (args.Context, true, false, 0);
					
						if(wrapper.Object is Field) {
							Field f = wrapper.Object as Field;
							designService.CreateTextBlockAtXY (f.Name,f.Name, f.FieldKind,args.X, args.Y);
						} else if(wrapper.Object is KeyValuePair<string,byte[]>) {
							var kvp = (KeyValuePair<string,byte[]>)wrapper.Object;
							designService.CreateImageAtXY(kvp.Key, args.X, args.Y);
						}  
					}
			};								
			
			previewDrawingArea.ModifyBg(Gtk.StateType.Normal,base.Style.Background (StateType.Insensitive));
		}

		void buildPreviewToolbar ()
		{
			pageSpinButton = new ToolBarSpinButton (40, 1, 1, 1);
			pageSpinButton.SpinButton.ValueChanged += delegate(object sender, EventArgs e) {
				
				pageNumber = (int)(pageSpinButton.SpinButton.Value);
				pageNumber -= 1;
				previewDrawingArea.QueueDraw ();
			};
			
			ToolBarLabel pagelabel = new ToolBarLabel ("Page: ");
			previewToolbar.Insert (pagelabel, 0);
			previewToolbar.Insert (pageSpinButton, 1);
			
		}

		protected virtual void OnDrawingareaExposeEvent (object o, Gtk.ExposeEventArgs args)
		{
			if (designService != null) {
				DrawingArea area = (DrawingArea)o;
				Cairo.Context cr = Gdk.CairoHelper.Create (area.GdkWindow);
				cr.Antialias = Cairo.Antialias.None;
				designService.RedrawReport (cr);
				area.SetSizeRequest ((int)designService.Width, (int)designService.Height);
				(cr as IDisposable).Dispose ();
			}
		}

		protected virtual void OnPreviewDrawingareaExposeEvent (object o, Gtk.ExposeEventArgs args)
		{
			
			DrawingArea area = (DrawingArea)o;
			if (designService.Report.Pages.Count > 0) {
				Cairo.Context cr = Gdk.CairoHelper.Create (area.GdkWindow);
				cr.Antialias = Cairo.Antialias.None;								 				 
				reportRenderer.Context  = cr;
				Cairo.Rectangle r = new Cairo.Rectangle(0,0,designService.Report.WidthWithMargins,designService.Report.HeightWithMargins);
				cr.FillRectangle(r,backgroundPageColor);
				cr.Translate(designService.Report.Margin.Left,designService.Report.Margin.Top);
				reportRenderer.RenderPage (designService.Report.Pages [pageNumber]);
				area.SetSizeRequest ((int)designService.Report.HeightWithMargins,(int) designService.Report.HeightWithMargins+5);
			
				(cr as IDisposable).Dispose ();
			}
		}

		protected virtual void OnDrawingareaButtonPressEvent (object o, Gtk.ButtonPressEventArgs args)
		{
			
			if (designService.IsDesign) {
				DesignDrawingArea.GrabFocus();
				workSpaceService.Status (String.Format ("press x:{0} y:{1} | xroot:{2} yroot:{3}", args.Event.X, args.Event.Y, args.Event.XRoot, args.Event.YRoot));
				
				int click = 1;
				if (args.Event.Type == EventType.TwoButtonPress)
					click = 2;
				
				designService.ButtonPress (args.Event.X, args.Event.Y, click);
				 
			}
			
		}

		protected virtual void OnDrawingareaMotionNotifyEvent (object o, Gtk.MotionNotifyEventArgs args)
		{
			
			if (designService.IsDesign) {
				designService.MouseMove (args.Event.X, args.Event.Y);
				workSpaceService.Status (String.Format ("move x:{0} y:{1}", args.Event.X, args.Event.Y));
			}
			
		}

		protected virtual void OnDrawingareaButtonReleaseEvent (object o, Gtk.ButtonReleaseEventArgs args)
		{
			if (designService.IsDesign) {
				designService.ButtonRelease (args.Event.X, args.Event.Y);
			}
		}

		protected virtual void OnMainNotebookSwitchPage (object o, Gtk.SwitchPageArgs args)
		{
			if (designService != null) {
				if (args.PageNum == 1) {
					designService.IsDesign = false;		
					evaluate ();

					ImageSurface imagesSurface = new ImageSurface (Format.Argb32, (int)designService.Report.Width, (int)designService.Report.Height);
					Cairo.Context cr = new Cairo.Context (imagesSurface);				
					reportRenderer.Context = cr;
					reportEngine = new ReportEngine (designService.Report, reportRenderer);

					
					reportEngine.Process ();
					(cr as IDisposable).Dispose ();
					pageSpinButton.SpinButton.SetRange (1, designService.Report.Pages.Count);
					previewDrawingArea.QueueDraw ();
				} else {
					designService.IsDesign = true;
					drawingarea.QueueDraw ();
				}
			}
		}
		protected virtual void OnDrawingareaKeyPressEvent (object o, Gtk.KeyPressEventArgs args)
		{			 
			DesignService.KeyPress(args.Event.Key);
		}
		
		protected virtual void OnDrawingareaKeyReleaseEvent (object o, Gtk.KeyReleaseEventArgs args)
		{
		}
		
		protected virtual void OnExecuteActionActivated (object sender, System.EventArgs e)
		{
			if(evaluate()){
				designService.RefreshDataFieldsFromDataSource();
			}
		}
		
		bool evaluate() {			
 
			object result = null;
			string meassage = null;			 
			bool res = false;
			string usings = String.Empty;
			string code = codeTextview.Buffer.Text;
			designService.Report.Parameters.Clear();
			designService.Report.DataSource = null;
			
			if( Compiler.Evaluate (out result, out meassage,new object[]{usings,code})  ) {
				var ds = (result as object[]);
				var datasource = ds[0] ;
				
				Dictionary<string,object> parametersDictionary = ds[1] as Dictionary<string,object>;
 				foreach (KeyValuePair<string, object> kvp in parametersDictionary) {
					 designService.Report.Parameters.AddRange(FieldBuilder.CreateFields(kvp.Value, kvp.Key,FieldKind.Parameter));
				}
					
				if (datasource != null) {
					designService.Report.DataSource = datasource;
					designService.Report.DataScript = codeTextview.Buffer.Text;		
					res = true;
				}
			}
			
			outputTextview.Buffer.Text = meassage;
			return res;
		}
		
	}
}

