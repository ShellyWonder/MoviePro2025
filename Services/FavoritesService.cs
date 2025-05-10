using Microsoft.JSInterop;
using MoviePro2025.Models;
using System.Text.Json;

namespace MoviePro2025.Services
{
    /// <summary>
    /// Service to manage favorites in local storage.
    /// </summary>
    /// <param name="Js"></param>
    public class FavoritesService(IJSRuntime Js)
    {
        private readonly string _localStorageKey = "favorites";

        #region RETRIEVE FAVORITES LIST(GET FROM LOCAL STORAGE
        /// <summary>
        /// Returns a list of favorite movies from local storage.
        /// </summary>
        /// <returns>List<movies>Favorites</movies></returns>
        public async Task<List<Movie>> GetFavoritesAsync()
        {
            List<Movie> movies = [];
            try
            {
                var favoritesJson = await Js.InvokeAsync<string>("localStorage.getItem", _localStorageKey);
                movies = JsonSerializer.Deserialize<List<Movie>>(favoritesJson) ?? [];

            }
            catch (Exception)
            {

                Console.WriteLine("Error retrieving favorites from local storage.");
            }
            return movies;
        } 
        #endregion

        #region SAVE (SET TO LOCAL STORAGE)FAVORITES 
        /// <summary>
        /// Saves a list of favorite movies to local storage.
        /// </summary>
        /// <param name="movies">An object of type movies</param>
        /// <returns></returns> void no return
        public async Task SaveFavoritesAsync(List<Movie> movies)
        {
            try
            {
                var favoritesJson = JsonSerializer.Serialize(movies);
                // Save the serialized list to local storage-- setting to storage does not return anything
                await Js.InvokeVoidAsync("localStorage.setItem", _localStorageKey, favoritesJson);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error saving favorites to local storage: {ex.Message}");
            }
        } 
        #endregion
        #region UPDATES FAVORITES LIST
        /// <summary>
        /// Gets list, add to list, and saves entire list each time to local storage.
        /// List is recreated each time a change is made to it 
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        public async Task AddFavoriteAsync(Movie movie)
        {
            var currentMovies = await GetFavoritesAsync();
            if (currentMovies.All(m => m.Id != movie.Id))
            {
                currentMovies.Add(movie);
                await SaveFavoritesAsync(currentMovies);
            }
        } 
        #endregion
    }
}
