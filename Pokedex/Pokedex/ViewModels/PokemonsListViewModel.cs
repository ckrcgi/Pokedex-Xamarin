using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Pokedex.Models;
using Pokedex.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Pokedex.ViewModels {
  public class PokemonsListViewModel : BaseViewModel {
    private readonly IPokemonsService _pokemonsService;
    private readonly Random _random;

    private PokeapiResponseResult _selectedPokemon;
    private string _internetAccess;

    public ObservableCollection<PokeapiResponseResult> Pokemons { get; set; }
    public Command LoadPokemonsCommand { get; }
    public Command<PokeapiResponseResult> PokemonTapped { get; }

    public PokemonsListViewModel() {
      _pokemonsService = DependencyService.Get<IPokemonsService>();
      _random = new Random();
      Title = "Home";
      InternetAccess = "Vérification de l'accès à Internet...";
      Pokemons = new ObservableCollection<PokeapiResponseResult>();
      LoadPokemonsCommand = new Command(async () => await FetchPokemons());
      PokemonTapped = new Command<PokeapiResponseResult>(OnPokemonSelected);
    }

    public PokeapiResponseResult SelectedPokemon {
      get => _selectedPokemon;
      set {
        SetProperty(ref _selectedPokemon, value);
        OnPokemonSelected(value);
      }
    }

    public string InternetAccess {
      get => _internetAccess;
      set => SetProperty(ref _internetAccess, value);
    }

    public async Task FetchPokemons() {
      IsBusy = true;

      if (Connectivity.NetworkAccess != NetworkAccess.Internet) {
        InternetAccess = "Pas d'accès internet !";
        IsBusy = false;
        return;
      } else {
        InternetAccess = "Accès internet !";
      }

      try {
        var response = await _pokemonsService.GetPokemons(_random.Next(1000));
        if (response != null) {
          if (!Enumerable.SequenceEqual(response.results, Pokemons)) {
            Pokemons.Clear();
            foreach (var pkmn in response.results) {
              Pokemons.Add(pkmn);
            }
          }
        }
      } catch (Exception) {
        Debug.WriteLine("Failed to load Pokémons.");
      } finally {
        IsBusy = false;
      }
    }

    async void OnPokemonSelected(PokeapiResponseResult pokemon) {
      if (pokemon == null) return;
      Debug.WriteLine(pokemon);
      await Shell.Current.GoToAsync($"/pokemon?name={pokemon.name}");
    }

    public void OnAppearing() {
      IsBusy = true;
      SelectedPokemon = null;
    }
  }
}