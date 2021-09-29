namespace Pokedex.Models {
  public class PokeapiResponse {
    public int count { get; set; }
    public PokeapiResponseResult[] results { get; set; }
  }

  public class PokeapiResponseResult {
    public string name { get; set; }
    public string url { get; set; }
  }
}