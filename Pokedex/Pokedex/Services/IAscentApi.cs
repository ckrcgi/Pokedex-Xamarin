using System.Threading.Tasks;
using Refit;

namespace Pokedex.Services {
  public interface IAscentApi {
        [Multipart]
        [Post("/medias")]
        Task PostMedias(
            string data,
            [AliasAs("files.file")] StreamPart stream
        );
  }
}