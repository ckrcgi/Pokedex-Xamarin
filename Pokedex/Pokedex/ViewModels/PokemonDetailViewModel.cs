using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using Pokedex.Models;
using Pokedex.Services;
using Xamarin.Forms;

namespace Pokedex.ViewModels {
  public class PokemonDetailViewModel : BaseViewModel, IQueryAttributable {
    private readonly IPokemonsService _pokemonsService;

    public Pokemon PokemonDetail { get; set; }

    public PokemonDetailViewModel() {
      Title = "Pokémon";
      _pokemonsService = DependencyService.Get<IPokemonsService>();
    }

    public async void FetchPokemon(string name) {
      try {
        var response = await _pokemonsService.GetPokemon(name);
        if (response != null) PokemonDetail = response;
      } catch (Exception) {
        Debug.WriteLine("Failed to load Pokémon.");
      }
    }

    public void ApplyQueryAttributes(IDictionary<string, string> query) {
      string name = HttpUtility.UrlDecode(query["name"]);
      Title = char.ToUpper(name[0]) + name.Substring(1);
      FetchPokemon(name);
    }
  }
}