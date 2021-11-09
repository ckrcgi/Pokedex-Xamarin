using System;
using System.IO;
using System.Net.Http;
using System.Reactive.Subjects;
using System.Text.Json;
using System.Threading.Tasks;
using Refit;
using Xamarin.Essentials;

namespace Pokedex.Services {
  public class PhotoService : IPhotoService {
    public ISubject<string> ErrorSubject { get; set; }
    private IAscentApi _api;

    public PhotoService() {
      ErrorSubject = new Subject<string>();
      _api = RestService.For<IAscentApi>("http://3638-77-131-3-239.ngrok.io");
    }

    public async Task<string> TakePhoto() {
      try {
        var photo = await MediaPicker.CapturePhotoAsync();
        return await SavePhoto(photo);
      } catch (FeatureNotSupportedException e) {
        ErrorSubject.OnNext("Feature not supported");
        Console.WriteLine($"CapturePhotoAsync THREW: {e.Message} (Feature not supported)");
        return null;
      } catch (PermissionException e) {
        ErrorSubject.OnNext("Permission not granted");
        Console.WriteLine($"CapturePhotoAsync THREW: {e.Message} (Permission not granted)");
        return null;
      } catch (Exception e) {
        ErrorSubject.OnNext(e.Message);
        Console.WriteLine($"CapturePhotoAsync THREW: {e.Message} (Other)");
        return null;
      }
    }

    public async Task<string> SavePhoto(FileResult photo) {
      if (photo == null) {
        return null;
      }
      var newFile = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);
      using (var stream = await photo.OpenReadAsync().ConfigureAwait(false)) {
        await SendPhoto(stream, photo);
      }
      //using (var newStream = File.OpenWrite(newFile)) {
      //  await stream.CopyToAsync(newStream).ConfigureAwait(false);
      //  await SendPhoto(newStream, photo);
      //}
      return newFile;
    }

    async Task SendPhoto(Stream stream, FileResult photo) {
      try {
        StreamPart streamPart = new StreamPart(stream, photo.FileName, photo.ContentType);
        await _api.PostMedias("{\"name\":\"test\"}", streamPart).ConfigureAwait(false);
      } catch (Exception e) {
        Console.WriteLine("Error : ", e.ToString());
      }
    }
  }
}