using System.Reactive.Subjects;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Pokedex.Services {
  public interface IPhotoService {
    public ISubject<string> ErrorSubject { get; set; }

    public Task<string> TakePhoto();
    public Task<string> SavePhoto(FileResult photo);

  }
}