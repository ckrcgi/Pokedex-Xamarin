using System;
using System.Reactive.Linq;
using Pokedex.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Pokedex.ViewModels {
  public class ConnectivityViewModel : BaseViewModel {
    private readonly IConnectivityService _connectivityService;
    private string _internetAccess;
    private string _statusColor;

    public IDisposable ConnectivityDisposable { get; set; }
    public string InternetAccess {
      get => _internetAccess;
      set => SetProperty(ref _internetAccess, value);
    }
    public string StatusColor {
      get => _statusColor;
      set => SetProperty(ref _statusColor, value);
    }

    public ConnectivityViewModel() {
      _connectivityService = DependencyService.Get<IConnectivityService>();
      _connectivityService.Init();
      ConnectivityDisposable = _connectivityService.ConnectivityObservable.Subscribe(ChangeText);
    }

    public void OnAppearing() {
      //ConnectivityDisposable = _connectivityService.ConnectivityObservable.Subscribe(ChangeText);
    }
    public void OnDisappearing() {
      ConnectivityDisposable.Dispose();
    }

    private void ChangeText(NetworkAccess network) {
      if (network == NetworkAccess.Internet) {
        InternetAccess = "👍 INTERNET 👍";
        StatusColor = "#00C853";
      } else {
        InternetAccess = "👎 NO INTERNET 👎";
        StatusColor = "#D50000";
      }
    }
  }
}