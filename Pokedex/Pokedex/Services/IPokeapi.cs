using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Pokedex.Models;
using Refit;

namespace Pokedex.Services {
  [Headers("Content-Type: application/json")]
  public interface IPokeapi {
    [Get("/api/v2/pokemon?offset={offset}")]
    Task<PokeapiResponse> GetPokemons(int offset);

    [Get("/api/v2/pokemon/{name}")]
    Task<Pokemon> GetPokemon(string name);
  }
}
