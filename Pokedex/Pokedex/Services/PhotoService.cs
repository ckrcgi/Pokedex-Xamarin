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
      _api = RestService.For<IAscentApi>("https://192.168.1.123:5001");
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
      using (var stream = await photo.OpenReadAsync().ConfigureAwait(false))
      using (var newStream = File.OpenWrite(newFile)) {
        await stream.CopyToAsync(newStream).ConfigureAwait(false);
        // await SendPhoto(newStream, photo);
      }
      return newFile;
    }

    async Task SendPhoto(FileStream stream, FileResult photo) {
      // using (HttpClient client = new HttpClient()) {
      //   using (HttpResponseMessage responseMessage = await client.GetAsync("https://localhost:5001/users")) {
      //     using (HttpContent content = responseMessage.Content) {
      //       var data = await content.ReadAsStreamAsync();
      //       if (data != null) Console.WriteLine("data---------------{0}", data);
      //     }
      //   }
      // }
      try {
        var res = await _api.PostAuth(new AuthBody { username = "username", password = "password" }).ConfigureAwait(false);
        Console.WriteLine(res);
        await _api.PostMedias(
          new StreamPart(stream, photo.FileName, photo.ContentType),
          "{\"picture_event_type\":\"PHOTO_INITIALE_CARGAISON\"",
          res.JwtToken
        ).ConfigureAwait(false);
      } catch (Exception e) {
        Console.WriteLine("Error : ", e.ToString());
      }
    }
  }
}