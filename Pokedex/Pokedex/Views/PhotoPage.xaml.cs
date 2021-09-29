using Pokedex.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pokedex.Views {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class PhotoPage : ContentPage {
    public PhotoPage() {
      InitializeComponent();
      BindingContext = new PhotoViewModel();
    }
  }
}