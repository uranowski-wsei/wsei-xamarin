using AirMonitor.ViewModels;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace AirMonitor.Views
{
    public partial class MapPage : ContentPage
    {
        private HomeViewModel _viewModel => BindingContext as HomeViewModel;
        public MapPage()
        {
            InitializeComponent();
            BindingContext = new HomeViewModel(Navigation);
            InitializeMap();
        }

        private async void InitializeMap()
        {
            var location = await _viewModel.GetLocation();

            if (location == null)
                return;

            Map.IsShowingUser = true;

            Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude,location.Longitude), Distance.FromKilometers(1)));
        }

        private void Pin_InfoWindowClicked(object sender, Xamarin.Forms.Maps.PinClickedEventArgs e)
        {
            if(sender is Pin pin)
            _viewModel?.MapPinTappedCommand.Execute(pin.Address);
        }
    }
}
