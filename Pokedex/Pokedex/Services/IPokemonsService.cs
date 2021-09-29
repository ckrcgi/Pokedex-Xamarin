using System.Collections.Generic;
using System.Threading.Tasks;
using Pokedex.Models;

namespace Pokedex.Services {
  public interface IPokemonsService {
    Task<PokeapiResponse> GetPokemons(int offset);
    Task<Pokemon> GetPokemon(string name);
  }
}