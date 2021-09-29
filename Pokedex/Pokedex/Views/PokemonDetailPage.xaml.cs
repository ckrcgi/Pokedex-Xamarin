using Pokedex.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pokedex.Views {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class PokemonDetailPage : ContentPage {
    public PokemonDetailPage() {
      InitializeComponent();
      BindingContext = new PokemonDetailViewModel();
    }
  }
}