using IE.Mobile.Forms.Utils;
using IE.Utilities.DataHandlers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using XF.Material.Forms.UI.Dialogs;
using Caliburn.Micro;
using IE.Utilities.Common;
using Plugin.Permissions.Abstractions;
using Plugin.Permissions;
using Plugin.Media;
using Plugin.Media.Abstractions;
using NM.Utilities.Constants;
using Xamarin.Forms;

namespace IE.Mobile.Forms.ViewModels
{
    internal class PackmanPageViewModel : BaseViewModel, IHandle<PackmanRotationDataHandler>,
        IHandle<PackmanPositionDataHandler>, IHandle<PackmanGridDataHandler>
    {
        private double GridHeight { get; set; }

        private string _Title;
        public string Title
        {
            get => _Title;
            set => this.Set(ref _Title, value);
        }

        private bool _OpenDrawer;
        public bool OpenDrawer
        {
            get => _OpenDrawer;
            set => this.Set(ref _OpenDrawer, value);
        }

        private int _TabPosition;
        public int TabPosition
        {
            get => _TabPosition;
            set
            {
                if (value == 0)
                    Title = "Packman";
                else
                    Title = "Settings";

                this.Set(ref _TabPosition, value);
            }
        }

        //Navigation Drawer
        public ICommand IRunOpenDrawer => new RelayExtension(RunOpenDrawer, CanRunOpenDrawer);

        public bool CanRunOpenDrawer() => true;
        public async void RunOpenDrawer()
        {
            if (!OpenDrawer)
            {
                await _aggregator.PublishOnUIThreadAsync(new NavigationDrawerDataHandler() { OpenDrawer = true });
                OpenDrawer = true;
                return;
            }
            else
            {
                await _aggregator.PublishOnUIThreadAsync(new NavigationDrawerDataHandler() { CloseDrawer = true });
                OpenDrawer = false;
                return;
            }
        }

        private string _Avatar;
        public string Avatar
        {
            get => _Avatar;
            set
            {
                this.Set(ref _Avatar, value);
            }
        }

        private string _Author;
        public string Author
        {
            get => _Author;
            set
            {
                this.Set(ref _Author, value);
            }
        }

        private string _AuthorUsername;
        public string AuthorUsername
        {
            get => _AuthorUsername;
            set
            {
                this.Set(ref _AuthorUsername, value);
            }
        }

        #region Settings
        private double _XPosition;
        public double XPosition
        {
            get => _XPosition;
            set
            {
                this.Set(ref _XPosition, Math.Ceiling(value));
            }
        }

        private double _YPosition;
        public double YPosition
        {
            get => _YPosition;
            set
            {
                this.Set(ref _YPosition, Math.Ceiling(value));
            }
        }

        public string[] LocalDirections = new string[4] { "North", "South", "East", "West" }; //This will be disposed by the garbage collector
        private string _SelectedDirection;
        public string SelectedDirection
        {
            get => _SelectedDirection;
            set
            {
                this.Set(ref _SelectedDirection, value);
            }
        }

        private int _SelectedDirectionIndex;
        public int SelectedDirectionIndex
        {
            get => _SelectedDirectionIndex;
            set
            {
                SelectedDirection = LocalDirections[value];
                this.Set(ref _SelectedDirectionIndex, value);
            }
        }

        public ICommand IPlacePackman => new RelayExtension(PlacePackman, CanPlacePackman);
        public bool CanPlacePackman() => true;

        public async void PlacePackman()
        {
            if (string.IsNullOrWhiteSpace(SelectedDirection))
                await AlertHelper.ShowAlertDialogueAsync("Selected direction is required", "Missing Input");
            else
            {
                IsPackmanVisible = true;
                //After validation make sure to call the place function to change the initial position of the packman
                await _aggregator.PublishOnUIThreadAsync(new PackmanPositionDataHandler()
                {
                    BeginPackmanMotionFromSettings = true,
                    PositionX = XPosition,
                    PositionY = (GridHeight - YPosition),
                    Direction = (PackmanDirection)SelectedDirectionIndex
                });

                if (SelectedDirection.Contains("Choose a direction"))
                    SelectedDirectionIndex = 0;

                TabPosition = 0;
            }
        }

        private bool _IsPackmanVisible;
        public bool IsPackmanVisible
        {
            get => _IsPackmanVisible;
            set => this.Set(ref _IsPackmanVisible, value);
        }

        //Reporting
        private bool _IsStatusVisible;
        public bool IsStatusVisible
        {
            get => _IsStatusVisible;
            set => this.Set(ref _IsStatusVisible, value);
        }

        public ICommand IOpenReport => new RelayExtension(OpenReport, CanOpenReport);

        public bool CanOpenReport() => true;
        public void OpenReport()
        {
            if (IsStatusVisible)
            {
                IsStatusVisible = false;
                return;
            }
            else
            {
                IsStatusVisible = true;
                return;
            }
        }

        //Error Managers
        private bool _HasSelectedDirectionError;
        public bool HasSelectedDirectionError
        {
            get => _HasSelectedDirectionError;
            set => this.Set(ref _HasSelectedDirectionError, value);
        }
        #endregion

        #region Packman Page

        public ICommand IChangeProfilePicture => new RelayExtension(ChangeProfilePicture, CanChangeProfilePicture);
        public bool CanChangeProfilePicture() => true;

        public async void ChangeProfilePicture()
        {
            //Change the Profile Avatar
            var permissions = await CrossPermissions.Current.RequestPermissionsAsync(new Permission[] { Permission.Camera, Permission.Photos });
            if (permissions[Permission.Camera] == PermissionStatus.Granted && permissions[Permission.Photos] == PermissionStatus.Granted)
            {
                try
                {
                    CrossMedia.Current.Initialize();
                    var response = await AlertHelper.ShowConfirmationDialogueAsync("", "Change your Avatar?", "Via Camera", "Via Gallery");
                    if (response.HasValue && response.Value)
                    {
                        var Photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
                        {
                            Directory = $"{AppInformation.AppName}",
                            SaveToAlbum = true,
                            Name = "avatar.jpg",
                            DefaultCamera = CameraDevice.Rear,
                            ModalPresentationStyle = MediaPickerModalPresentationStyle.FullScreen,
                            PhotoSize = PhotoSize.Full
                        });

                        if (Photo != null)
                        {
                            Avatar = Photo.Path;
                        }
                    }
                    else
                    {
                        if (response.HasValue)
                        {
                            var PhotoGallery = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions()
                            {
                                ModalPresentationStyle = MediaPickerModalPresentationStyle.FullScreen,
                                PhotoSize = PhotoSize.Full,
                            });

                            if (PhotoGallery != null)
                            {
                                Avatar = PhotoGallery.Path;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }

        }

        public ICommand IPlaceNewPackman => new RelayExtension(PlaceNewPackman, CanPlaceNewPackman);
        public bool CanPlaceNewPackman() => true;

        public async void PlaceNewPackman()
        {
            var response = await AlertHelper.ShowConfirmationDialogueAsync("Are you sure you want to do this? Your packman's position will be reset to the values on settings", "Place a new Packman?", "Place");
            if (response.HasValue && response.Value)
                BeginNewPackmanPlacement();
        }

        private async void BeginNewPackmanPlacement()
        {
            await _aggregator.PublishOnUIThreadAsync(new PackmanPositionDataHandler()
            {
                BeginPackmanMotionFromSettings = true,
                Direction = PackmanDirection.North,
                PositionX = 0,
                PositionY = GridHeight
            });
            TabPosition = 1; //User is shifted to the Settings page
            IsPackmanVisible = false;

            //Setting Values
            SelectedDirectionIndex = 0;
            SelectedDirection = "Choose a direction (North, South, East, West)"; ;
            XPosition = 0;
            YPosition = 0;
        }

        #endregion

        /// <summary>
        /// Any dependencies are injected here, via constructor injection
        /// </summary>
        public PackmanPageViewModel()
        {
            InitializeViewModel();
        }
        private void InitializeViewModel()
        {
            Title = "Packman";
         //   InitialStartupMessage();

            //Set the Tab Position of the Packman Tab View to 1 to force the user to enter this data first
            InitialData();
        }

        /// <summary>
        /// This is used for reading X,Y,Z Positions, and which direction the packman is facing
        /// </summary>
        private async void InitialStartupMessage()
        {
            await AlertHelper.ShowAlertDialogueAsync("This is a fun little project created by Dan Gerchcovich. To get started" +
                " please provide the X,Y, and facing positions of packman", "Welcome to Packman!");
        }

        private void InitialData()
        {
            SelectedDirectionIndex = 0;
            IsStatusVisible = false;
            IsPackmanVisible = false;
            TabPosition = 1;
            Author = "Dan Gerchcovich";
            AuthorUsername = "Xamarin Master";
            Avatar = "appicon.png"; //This can be read from Blob Storage or from the local resources folder
            SelectedDirection = "Choose a direction (North, South, East, West)";
        }

        //Handlers
        public void Handle(PackmanRotationDataHandler message)
        {
            SelectedDirectionIndex = (int)message.Direction;
        }

        public void Handle(PackmanPositionDataHandler message)
        {
            if (message.UpdateTextLocation)
            {
                XPosition = message.PositionX;
                YPosition = message.PositionY;
            }
        }

        public void Handle(PackmanGridDataHandler message)
        {
            GridHeight = message.GridHeight;
        }
    }
}
