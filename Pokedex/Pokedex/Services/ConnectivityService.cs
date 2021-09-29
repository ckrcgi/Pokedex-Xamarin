using System;
using System.Linq;
using System.Reactive.Linq;
using Xamarin.Essentials;

namespace Pokedex.Services {
  public class ConnectivityService : IConnectivityService {
    public IObservable<NetworkAccess> ConnectivityObservable { get; set; }

    public void Init() {
      ConnectivityObservable = Observable.FromEventPattern<ConnectivityChangedEventArgs>(
        e => Connectivity.ConnectivityChanged += e,
        e => Connectivity.ConnectivityChanged -= e
      )
      .Select(e => e.EventArgs.NetworkAccess)
      .StartWith(Connectivity.NetworkAccess);
    }
  }
}