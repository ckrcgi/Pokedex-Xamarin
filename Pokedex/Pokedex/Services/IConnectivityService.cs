using System;
using Xamarin.Essentials;

namespace Pokedex.Services {
  public interface IConnectivityService {
    public IObservable<NetworkAccess> ConnectivityObservable { get; set; }
    public void Init();
  }
}