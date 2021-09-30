using System;
using Pokedex.Services;
using Pokedex.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pokedex {
  public partial class App : Application {

    public App() {
      InitializeComponent();
      DependencyService.Register<PokemonsService>();
      DependencyService.Register<ConnectivityService>();
      DependencyService.Register<PhotoService>();
      DependencyService.Register<BarCodeReader>();
      MainPage = new AppShell();
    }

    protected override void OnStart() {
    }

    protected override void OnSleep() {
    }

    protected override void OnResume() {
    }
  }
}
