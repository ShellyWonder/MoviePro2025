using MoviePro2025.Models.Credits;
//using MoviePro2025.Models.Providers;
//using MoviePro2025.Models.Search;
//using MoviePro2025.Models.TMDBMovieLists;
using MoviePro2025.Models;
using MoviePro2025.Models.MovieModels;
using System.Net.Http.Json;
using System.Web;


namespace MoviePro2025.Services
{
    public class TMDBClientService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseImageUrl;
        private const string _defaultPosterPath = "/img/PosterPlaceHolder.png"; // Default poster path if none is provided
        private const string _defaultBackdropPath = "/img/1920_backdrop.jpg"; // Default backdrop path if none is provided

        #region CONSTRUCTOR/CONFIG
        public TMDBClientService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _baseImageUrl = config["TMDBBaseImageUrl"] ?? _defaultPosterPath; // Provide a default if needed

            _httpClient.BaseAddress = new Uri("https://api.themoviedb.org/3/");

            _httpClient.DefaultRequestHeaders.Accept.Add(new("application/json"));

            string apiKey = config["TMDBKey"] ?? throw new Exception("TMDBKey not found.");
            _httpClient.DefaultRequestHeaders.Authorization = new("Bearer", apiKey);
        }
        #endregion

        #region NowPlaying 
        public async Task<MovieListResponse> GetNowPlayingAsync(int page = 1)
        {
            page = MovieCount(page);

            var response = await _httpClient.GetFromJsonAsync<MovieListResponse>($"movie/now_playing?page={page}&region=US&language=en-US")
                                                    ?? throw new Exception("No movie data returned");

            ProcessMoviePosters(response.Results);
            return response;

        }
        #endregion
        #region PopularMovies 
        public async Task<MovieListResponse> GetPopularMoviesAsync(int page = 1)
        {
            page = MovieCount(page);

            var response = await _httpClient.GetFromJsonAsync<MovieListResponse>($"movie/popular?page={page}&region=US&language=en-US")
                                                                                                               ?? throw new Exception("No movie data returned");
            ProcessMoviePosters(response.Results);
            return response;
        }
        #endregion

        #region GET Movie Search
        /// <summary>
        /// Searches for movies by title.
        /// </summary>
        /// <param name="title">User supplied via search input("Query") passed into call as encoded title.</param>
        /// <param name="page">Used for pagination</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<MovieListResponse?>SearchMoviesByTitleAsync(string title, int page = 1)
        {
            page = MovieCount(page);
            // encode the title to make it URL safe
            string encodedTitle = HttpUtility.UrlEncode(title);
            var adult = false;
            var region = "US";
            var language = "en-US";
            var response = await _httpClient.GetFromJsonAsync<MovieListResponse>($"search/movie?query={encodedTitle}&include_adult={adult}&region={region}&language={language}&page={page}")
                                                                       ?? throw new Exception("No search data returned");
            ProcessMoviePosters(response.Results);
            return response;
        }
        #endregion


        //#region TopRated
        //public  async Task<PageResponse<TopRated>?> GetTopRatedAsync(int page = 1)
        //{
        //    page = MovieCount(page);

        //    var response = await _httpClient.GetFromJsonAsync<PageResponse<TopRated>>($"movie/top_rated?page={page}") ?? throw new Exception("No movie data returned"); 
        //    return response;
        //}
        //#endregion

        #region MovieDetails
        public  async Task<MovieDetails> GetMovieDetailsAsync(int movieId)
        {
            var movie = await _httpClient.GetFromJsonAsync<MovieDetails>($"movie/{movieId}")
                                                                         ?? throw new Exception("No movie data returned");
            //not using ProcessMoviePosters here because we are not returning a list of movies just a single movie
            movie.PosterPath = string.IsNullOrWhiteSpace(movie.PosterPath) ? _defaultPosterPath
                    : $"{_baseImageUrl}{movie.PosterPath}";

            movie.BackdropPath = string.IsNullOrEmpty(movie.BackdropPath) ? _defaultBackdropPath
                    : $"{_baseImageUrl}{movie.BackdropPath}";
            return movie;
        }
        #endregion

        #region GET MOVIE VIDEOS
        public async Task<Video> GetMovieTrailerAsync(int movieId)
        {
            var videos = await _httpClient.GetFromJsonAsync<MovieVideosResponse>($"movie/{movieId}/videos?language=en-US")
                                                                                                               ?? throw new Exception("No video data returned");
            //restricts results to trailer type and youtube site only
            Video movieTrailer = videos.Results.FirstOrDefault(v => v.Type.Contains("Trailer", StringComparison.OrdinalIgnoreCase)
                                                                 && v.Site.Contains("YouTube", StringComparison.OrdinalIgnoreCase))
                                                                 ?? throw new Exception("No video data returned");
            return movieTrailer;
        }
        #endregion

        //#region GET PERSON SEARCH
        //public Task<PageResponse<PersonSearchResult>?> SearchMoviesByPerson(string name, int page = 1)
        //{
        //    page = MovieCount(page);
        //    // encode the title to make it URL safe
        //    string encodedName = HttpUtility.UrlEncode(name);
        //    var adult = false;
        //    var language = "en-US";
        //    var response = _httpClient.GetFromJsonAsync<PageResponse<PersonSearchResult>?>($"search/person?query={encodedName}&include_adult={adult}&language={language}&page={page}")
        //                                                               ?? throw new Exception("No search data returned");
        //    return response;
        //}
        //#endregion

        //#region WATCH PROVIDERS(ATTRIBUTION TO WATCH PROVIDERS REQUIRED)
        // public async Task<ProviderDetail<ProviderOption, ProviderOption, ProviderOption>?> GetProvidersByMovieIdAsync(int id)
        // {
        //        var response =  await _httpClient.GetFromJsonAsync<ProviderDetail<ProviderOption, 
        //                                                                                                                          ProviderOption, 
        //                                                                                                                          ProviderOption>>($"/movie/{id}/watch/providers") 
        //                                                                                                                          ?? throw new Exception("No provider data returned");   
        //     return response;
        // }


        // #endregion

        //#region ACTOR(PERSON) DETAILS
        //public async Task<PersonDetails>GetPersonDetailsById(int personId)
        //{
        //    var response =  await _httpClient.GetFromJsonAsync<PersonDetails?>($"person/{personId}?language=en-US")
        //                                           ?? throw new Exception("No data returned"); ;
        //    return response;
        //}

        //#endregion

        //#region GET CAST/CREW CREDITS
        //public async Task<Credit?>GetCreditsByMovieIdAsync(int id)
        //{
        //   var response = await _httpClient.GetFromJsonAsync<Credit?>($"movie/{id}/credits?language=en-US") 
        //                                              ?? throw new Exception("No cast data returned");
        //    return response;
        //}
        //#endregion

        //#region GET MOVIE CREDITS BY PERSON ID
        //public async Task<Credit?> GetMovieCreditsByPersonIdAsync(int personId)
        //{
        //    var response = await _httpClient.GetFromJsonAsync<Credit?>($"person/{personId}/movie_credits?language=en-US")
        //                                              ?? throw new Exception("No movie data returned");
        //    return response;
        //}
        //#endregion

        //#region GET PERSON DETAILS WITH CREDITS BY ID
        //public async Task<PersonDetailsWithCredits> GetPersonDetailsWithCreditsById(int personId)
        //{
        //    var response = await _httpClient.GetFromJsonAsync<PersonDetailsWithCredits>($"person/{personId}?append_to_response=movie_credits&language=en-US")
        //                                              ?? throw new Exception("No data returned");

        //    return response;
        //}
        //#endregion

        //#region SEARCH ASYNC
        ////EXTRACTION OF SEARCH DUPE CODE FOR FUTURE IMPLEMENTATION
        //private async Task<PageResponse<T>?> SearchAsync<T>(string endpointType, string query, int page = 1)
        //{
        //    // Validate page range
        //    page = MovieCount(page);

        //    // Encode the query to make it URL-safe
        //    string encodedQuery = HttpUtility.UrlEncode(query);

        //    bool adult = false;
        //    string language = "en-US";

        //    var response = await _httpClient.GetFromJsonAsync<PageResponse<T>?>(
        //        $"search/{endpointType}?query={encodedQuery}&include_adult={adult}&language={language}&page={page}"
        //    ) ?? throw new Exception("No search data returned");

        //    return response;
        //}
        //#endregion

        #region UTILITY METHODS
        private static int MovieCount(int page)
        {
            if (page < 1) page = 1;
            if (page > 500) page = 500;
            return page;
        }
        #endregion

        #region HELPER METHODS
        private void ProcessMoviePosters(List<Movie> movies)
        {
            foreach (var movie in movies)
            {
                movie.PosterPath = string.IsNullOrWhiteSpace(movie.PosterPath) ? _defaultPosterPath
                    : $"{_baseImageUrl}{movie.PosterPath}";

            }
        }
        #endregion
    }
}
