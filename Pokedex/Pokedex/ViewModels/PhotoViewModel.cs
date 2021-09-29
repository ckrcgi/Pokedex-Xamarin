using Pokedex.Services;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Forms;

namespace Pokedex.ViewModels {
  public class PhotoViewModel : BaseViewModel {
    private readonly IPhotoService _photoService;
    private ImageSource _imageSrc;
    private string _photoPath;

    public Command TakePhotoCommand { get; }
    public Command NavigateCommand { get; }
    public ImageSource ImageSrc {
      get => _imageSrc;
      set => SetProperty(ref _imageSrc, value);
    }
    public string PhotoPath {
      get => _photoPath;
      set => SetProperty(ref _photoPath, value);
    }

    public PhotoViewModel() {
      _photoService = DependencyService.Get<IPhotoService>();
      TakePhotoCommand = new Command(async () => PhotoPath = await _photoService.TakePhoto());
      NavigateCommand = new Command(async () => await Shell.Current.GoToAsync("home"));
    }
  }
}