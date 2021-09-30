using Honeywell.AIDC.CrossPlatform;
using Pokedex.Services;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Forms;

namespace Pokedex.ViewModels {
  public class PhotoViewModel : BaseViewModel {
    private readonly IPhotoService _photoService;
    private readonly IBarCodeReader _reader;
    private ImageSource _imageSrc;
    private string _photoPath;
    private string _barCode;

    public Command TakePhotoCommand { get; }
    public Command NavigateCommand { get; }
    public Command ScanCommand { get; }

    public ImageSource ImageSrc {
      get => _imageSrc;
      set => SetProperty(ref _imageSrc, value);
    }

    public string PhotoPath {
      get => _photoPath;
      set => SetProperty(ref _photoPath, value);
    }

    public string BarCode {
      get => _barCode;
      set => SetProperty(ref _barCode, value);
    }

    public PhotoViewModel() {
      _photoService = DependencyService.Get<IPhotoService>();
      _reader = DependencyService.Get<IBarCodeReader>();

      _reader.BarcodeDataHasBeenReceived += OnBarCodeDataReceived;

      TakePhotoCommand = new Command(async () => PhotoPath = await _photoService.TakePhoto());
      NavigateCommand = new Command(async () => await Shell.Current.GoToAsync("home"));
    }

    private void OnBarCodeDataReceived(object sender, BarcodeDataArgs e) {
      BarCode = e.Data;
    }
  }
}