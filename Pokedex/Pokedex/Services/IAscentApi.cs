using System.Threading.Tasks;
using Refit;

namespace Pokedex.Services {
  public interface IAscentApi {
    [Multipart]
    [Post("/medias")]
    [Headers("Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6ImUwMDAwMTEiLCJuYmYiOjE2MzY0NjgyNjYsImV4cCI6MTYzNjQ2OTE2NiwiaWF0IjoxNjM2NDY4MjY2fQ.GUefh7uORxC2HXEBkiwU8xP0TTGdpErBixttnI_-H4s")]
    Task PostMedias(
        string additional_json_file_information,
        [AliasAs("file")] StreamPart stream
    );
  }
}