using System.Threading.Tasks;
using Pokedex.Models;
using Refit;

namespace Pokedex.Services {
  public class PokemonsService : IPokemonsService {
    private readonly IPokeapi _api;

    public PokemonsService() {
      _api = RestService.For<IPokeapi>("https://pokeapi.co");
    }

    public async Task<PokeapiResponse> GetPokemons(int offset) {
      return await _api.GetPokemons(offset).ConfigureAwait(false);
    }

    public async Task<Pokemon> GetPokemon(string name) {
      return await _api.GetPokemon(name).ConfigureAwait(false);
    }
  }
}