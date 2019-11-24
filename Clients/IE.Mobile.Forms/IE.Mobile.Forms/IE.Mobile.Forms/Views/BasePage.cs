using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IE.Mobile.Forms.Views
{
    /// <summary>
    /// Any Base Business Logic gets written here....
    /// </summary>
    public class BasePage : ContentPage
    {
        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false); //Hide the navigation bar on all pages
            GenerateSubscriptions();
        }

        protected virtual void GenerateSubscriptions()
        {
            IoC.Get<IEventAggregator>().Subscribe(this); //Subscribe to any Event Aggregator messages -- This will act as the message broker
        }
    }
}
