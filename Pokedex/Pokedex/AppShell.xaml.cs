using Pokedex.Views;
using Xamarin.Forms;

namespace Pokedex {
  public partial class AppShell : Shell {
    public AppShell() {
      InitializeComponent();

      Routing.RegisterRoute("home", typeof(PokemonsListPage));
      Routing.RegisterRoute("pokemon", typeof(PokemonDetailPage));
      Routing.RegisterRoute("connectivity", typeof(ConnectivityPage));
      Routing.RegisterRoute("photo", typeof(ConnectivityPage));
    }
  }
}
