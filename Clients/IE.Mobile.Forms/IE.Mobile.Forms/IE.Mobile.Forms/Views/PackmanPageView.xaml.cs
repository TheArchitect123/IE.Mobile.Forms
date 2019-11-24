using Caliburn.Micro;
using IE.Utilities.Common;
using IE.Utilities.DataHandlers;
using NM.Utilities.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;

namespace IE.Mobile.Forms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PackmanPageView : BasePage, IHandle<PackmanMotionDataHandler>,
        IHandle<PackmanPositionDataHandler>, IHandle<NavigationDrawerDataHandler>
    {
        private double ScreenWidth { get; set; }
        private double ScreenHeight { get; set; }

        private PackmanDirection CachedDirection { get; set; }
        public PackmanPageView()
        {
            InitializeComponent();
            InitializeView();
        }

        public async void Handle(PackmanMotionDataHandler message)
        {
            if (message.ResetPackmanPosition)
            {
                //Packman resets to the original position -- This occurs only if the user chooses another place form
                await this.PackmanView.FadeTo(0, 250, Easing.Linear);
                this.PackmanView.IsVisible = false;
            }
        }

        private async void InitializeView()
        {
            this.packmanDrawer.IsOpen = true;
            //Set the Maximum Positional values of the sliders
            this.DirectionsView.ItemsSource = new string[4] { "North", "South", "East", "West" }; //Load the item Sources
        }

        #region Packman Animation & Transform Handlers
        private void RotatePackmanLeftDegrees(object sender, EventArgs e)
        {
            NotifyViewModelOfDirectionalChange(false);
        }

        private void RotatePackmanRightDegrees(object sender, EventArgs e)
        {
            NotifyViewModelOfDirectionalChange(true);
        }

        private void MovePackmanByClicked(object sender, EventArgs e)
        {
            MovePackman();
        }

        private async void NotifyViewModelOfDirectionalChange(bool isRight)
        {
            switch (CachedDirection)
            {
                #region Packman Rotations
                case PackmanDirection.East:

                    if (isRight)
                    {
                        CachedDirection = PackmanDirection.South;
                        this.PackmanView.Rotation += 90;
                    }
                    else
                    {
                        CachedDirection = PackmanDirection.North;
                        this.PackmanView.Rotation -= 90;
                    }
                    break;
                case PackmanDirection.West:
                    if (isRight)
                    {
                        CachedDirection = PackmanDirection.North;
                        this.PackmanView.Rotation += 90;
                    }
                    else
                    {
                        CachedDirection = PackmanDirection.South;
                        this.PackmanView.Rotation -= 90;
                    }
                    break;
                case PackmanDirection.South:
                    if (isRight)
                    {
                        CachedDirection = PackmanDirection.West;
                        this.PackmanView.Rotation += 90;
                    }
                    else
                    {
                        CachedDirection = PackmanDirection.East;
                        this.PackmanView.Rotation -= 90;
                    }
                    break;

                case PackmanDirection.North:
                    if (isRight)
                    {
                        CachedDirection = PackmanDirection.East;
                        this.PackmanView.Rotation += 90;
                    }
                    else
                    {
                        CachedDirection = PackmanDirection.West;
                        this.PackmanView.Rotation -= 90;
                    }
                    break;
                    #endregion
            }

            await IoC.Get<IEventAggregator>()
                .PublishOnUIThreadAsync(new PackmanRotationDataHandler()
                { Direction = CachedDirection }); //Notify the Message Broker of changes to CachedDirection
        }

        private async void PlacePackmanForReset(PackmanDirection direction, double positionX, double positionY)
        {
            CachedDirection = direction;
            this.PackmanView.Rotation = 0; //Set thre Packman position to its default
            switch (direction)
            {
                case PackmanDirection.North:
                    this.PackmanView.Rotation -= 90;
                    break;
                case PackmanDirection.West:
                    this.PackmanView.Rotation -= 180;
                    break;
                case PackmanDirection.South:
                    this.PackmanView.Rotation += 90;
                    break;
            }

            if (positionX >= ScreenWidth) //Format Position X
                positionX -= 45;

            if (positionY >= ScreenHeight) //Format Position X
                positionY -= 45;

            //Prevent any Negative Values
            if (positionY < 0)
                positionY = 0;
            if (positionX < 0)
                positionX = 0;

            this.PackmanView.TranslationX = positionX;
            this.PackmanView.TranslationY = positionY;
        }

        private async void MovePackman()
        {
            await AudioHelper.PlayAudioTwitterSound().ConfigureAwait(false); //Plays click sound everytime the packman moves
            //Depending on the direction of the packman set in the view model will determine the loop values
            switch (CachedDirection)
            {
                case PackmanDirection.East:
                    //If packman is heading east
                    if (this.PackmanView.TranslationX < (ScreenWidth - 45))
                        this.PackmanView.TranslationX += 5;
                    break;
                case PackmanDirection.West:
                    if (this.PackmanView.TranslationX > 0)
                        this.PackmanView.TranslationX -= 5;
                    break;
                case PackmanDirection.South:
                    if (this.PackmanView.TranslationY < (ScreenHeight - 115))
                        this.PackmanView.TranslationY += 5;
                    break;
                case PackmanDirection.North:
                    if (this.PackmanView.TranslationY > 0)
                        this.PackmanView.TranslationY -= 5;
                    break;
            }

            await IoC.Get<IEventAggregator>()
            .PublishOnUIThreadAsync(new PackmanPositionDataHandler()
            { UpdateTextLocation = true, PositionX = this.PackmanView.TranslationX, PositionY = this.PackmanView.TranslationY }); //Notify the Message Broker of changes to CachedDirection
        }

        public void Handle(PackmanPositionDataHandler message)
        {
            if (message.BeginPackmanMotionFromSettings)
                PlacePackmanForReset(message.Direction, message.PositionX, message.PositionY);
        }

        public void Handle(NavigationDrawerDataHandler message)
        {
            if (message.OpenDrawer)
                this.packmanDrawer.IsOpen = true;
            else if (message.OpenDrawer)
                this.packmanDrawer.IsOpen = false;
        }

        #endregion

        private async void PositionTabBar_SelectionChanged(object sender, Syncfusion.XForms.TabView.SelectionChangedEventArgs e)
        {
            if (e.Index == 0 && !this.PackmanView.IsVisible)
            {
                //Output an alert dialogue that will force the user back into the settings page, considering that the place function must occur before anything else
                await AlertHelper.ShowAlertDialogueAsync("You must place the packman first, somewhere on the screen by completing the form on the settings page",
                    "Place function is missing");
                this.PositionTabBar.SelectedIndex = 1;
            }
            else if (e.Index == 1)
            {
                ScreenWidth = this.XPositionView.MaxValue = Application.Current.MainPage.Width;
                ScreenHeight = this.YPositionView.MaxValue = Application.Current.MainPage.Height - 110;

                await IoC.Get<IEventAggregator>()
       .PublishOnUIThreadAsync(new PackmanGridDataHandler()
       { GridHeight = ScreenHeight }); //Since this Grid Height applies only to the this page, and not to any others,
                //Then use EventAggregation instead of a constant

                ApplicationConstants.Origin = new Point(0, ScreenHeight);
            }
        }

        private void CloseNavigationDrawer(object sender, EventArgs e)
        {
            this.packmanDrawer.IsOpen = false;
        }
    }
}