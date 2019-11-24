using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IE.Mobile.Forms.CustomRenderers
{
    public class NillSectionSeperatorTableView : TableView
    {
        public static readonly BindableProperty SeperatorVisibleProperty = BindableProperty.Create("SeperatorVisible", typeof(SeparatorVisibility), typeof(NillSectionSeperatorTableView), SeparatorVisibility.None);
        public SeparatorVisibility SeperatorVisible
        {
            get { return (SeparatorVisibility)GetValue(SeperatorVisibleProperty); }
            set { SetValue(SeperatorVisibleProperty, value); }
        }

        public void ForceRefresh()
        {
            if (_UpdateCollection != null)
                _UpdateCollection.Invoke("", EventArgs.Empty);
        }

        public event EventHandler _UpdateCollection;
    }
}
