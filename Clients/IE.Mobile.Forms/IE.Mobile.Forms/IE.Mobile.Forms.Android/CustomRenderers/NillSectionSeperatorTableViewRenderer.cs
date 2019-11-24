using Android.Content;
using Gr = Android.Graphics;
using Android.Graphics.Drawables;
using IE.Mobile.Forms.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Views;

[assembly: ExportRenderer(typeof(NillSectionSeperatorTableView), typeof(IE.Mobile.Droid.Renderers.NillSectionSeperatorTableViewRenderer))]
namespace IE.Mobile.Droid.Renderers
{
    public class NillSectionSeperatorTableViewRenderer : TableViewRenderer
    {
        public NillSectionSeperatorTableViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<TableView> e)
        {
            base.OnElementChanged(e);

            var listView = Control as global::Android.Widget.ListView;
            listView.Divider = new ColorDrawable(Gr.Color.Transparent);
            listView.Selector = new ColorDrawable(Gr.Color.Transparent);
            listView.DividerHeight = 0;
            listView.VerticalScrollBarEnabled = false;
        }
    }
}