using Pokedex.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pokedex.Views {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class ConnectivityPage : ContentPage {
    public ConnectivityPage() {
      InitializeComponent();
      BindingContext = new ConnectivityViewModel();
    }
  }
}