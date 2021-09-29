using System.Threading.Tasks;
using Refit;

namespace Pokedex.Services {
  public interface IAscentApi {
    [Headers("Content-Type: application/json")]
    [Post("/auth")]
    Task<AuthResponse> PostAuth([Body] AuthBody body);

    [Multipart]
    [Post("/medias")]
    Task PostMedias(
      [AliasAs("file")] StreamPart stream,
      [AliasAs("additional_json_file_information")] string info,
      [Authorize("Bearer")] string token
    );
  }

  public class AuthBody {
    public string username { get; set; }
    public string password { get; set; }
  }

  public class AuthResponse {
    [AliasAs("jwt_token")]
    public string JwtToken { get; set; }
    [AliasAs("refresh_token")]
    public string RefreshToken { get; set; }
  }
}