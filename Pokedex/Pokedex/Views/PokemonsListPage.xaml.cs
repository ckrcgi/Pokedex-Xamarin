using Pokedex.Services;
using Pokedex.ViewModels;
using Xamarin.Forms;

namespace Pokedex.Views {
  public partial class PokemonsListPage : ContentPage {
    private readonly PokemonsListViewModel _viewModel;
    public PokemonsListPage() {
      InitializeComponent();
      // Check PokemonDetail pour Injection
      BindingContext = _viewModel = new PokemonsListViewModel();
    }

    protected override void OnAppearing() {
      base.OnAppearing();
      _viewModel.OnAppearing();
    }
  }
}
