using UIKit;
using Foundation;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using IE.Mobile.Forms.CustomRenderers;

[assembly: ExportRenderer(typeof(NillSectionSeperatorTableView), typeof(IE.Mobile.iOS.Renderers.NillSectionSeperatorTableViewRenderer))]
namespace IE.Mobile.iOS.Renderers
{
    public class NillSectionSeperatorTableViewRenderer : TableViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<TableView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
                return;

            var zoControl = (NillSectionSeperatorTableView)e.NewElement;
            if (zoControl.SeperatorVisible == SeparatorVisibility.None)
            {
                var tableView = Control as UITableView;
                zoControl._UpdateCollection += ZoControl__UpdateCollection;
                tableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
                tableView.AllowsSelection = false;
                tableView.ShowsVerticalScrollIndicator = false;

                //NSNotificationCenter.DefaultCenter.AddObserver(UIDevice.OrientationDidChangeNotification, (orientation) =>
                //{
                //    tableView.ReloadData(); //Reload all Data && Sections 
                //    tableView.ScrollToRow(NSIndexPath.FromRowSection(0, 0), UITableViewScrollPosition.Top, true); //This will resolve the issue with the table view reload
                //});
            }
        }

        private void ZoControl__UpdateCollection(object sender, System.EventArgs e)
        {
            var tableView = Control as UITableView;
            if (tableView != null)
            {
                tableView.ReloadData(); //Reload all Data && Sections 
                tableView.ScrollToRow(NSIndexPath.FromRowSection(0, 0), UITableViewScrollPosition.Top, true); //This will resolve the issue with the table view reload
            }
        }
    }
}