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
      if (photo == null) return null;

      string fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
      var newFilePath = Path.Combine(FileSystem.AppDataDirectory, fileName);
      using (var stream = await photo.OpenReadAsync().ConfigureAwait(false)) {
        using (var newStream = File.OpenWrite(newFilePath)) {
          await stream.CopyToAsync(newStream).ConfigureAwait(false);
        }
        try {
          await SendPhoto(stream, fileName, photo.ContentType, "{\"event_type\":\"ARRIVEE_TOURNEE_SUR_PDL_(ZDE1)\",\"is_signature_picture\":false}");
        } catch (Exception e) {
          Console.WriteLine($"SendPhoto THREW: {e.Message}");
        }
      }

      return newFilePath;
    }

    async Task SendPhoto(Stream stream, string fileName, string contentType, string data) {
      Console.WriteLine(stream);
      try {
        StreamPart streamPart = new StreamPart(stream, fileName, contentType);
        await _api.PostMedias(data, streamPart).ConfigureAwait(false);
      } catch (Exception e) {
        Console.WriteLine("Error : ", e.ToString());
      }
    }

    async Task ResendPhoto(string fileName, string data) {
      string filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);
      string fileExtension = Path.GetExtension(filePath);

      using (var stream = File.OpenRead(filePath)) {
        await SendPhoto(stream, fileName, GetMimeType(fileExtension), data);
      }
    }

    string GetMimeType(string extension) {
      switch (extension) {
        case "png": return "image/png";
        default: return "image/jpeg";
      }
    }
  }
}

