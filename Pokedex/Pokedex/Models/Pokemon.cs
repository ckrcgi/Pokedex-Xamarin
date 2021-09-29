using Newtonsoft.Json;

namespace Pokedex.Models {
  public class Pokemon {
    public int id { get; set; }
    public float height { get; set; }
    public string name { get; set; }
    public PokemonType[] species { get; set; }
    public PokemonStat[] stats { get; set; }
    public PokemonSprite[] sprites { get; set; }
  }

  public class PokemonType {
    public string name { get; set; }
    public string url { get; set; }
  }

  public class PokemonStat {
    public string base_stat { get; set; }
  }

  public class PokemonSprite {
    public PokemonSpriteOther other { get; set; }
  }
  public class PokemonSpriteOther {
    [JsonProperty("official-artwork")]
    public PokemonSpriteOtherOfficialArtWork officialArtWork { get; set; }
  }
  public class PokemonSpriteOtherOfficialArtWork {
    public string front_default { get; set; }
  }
}