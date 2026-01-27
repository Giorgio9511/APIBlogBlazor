using ClienteBlazorWASM.Helpers;
using ClienteBlazorWASM.Models;
using ClienteBlazorWASM.Models.ViewModels;
using ClienteBlazorWASM.Servicios.IServicio;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace ClienteBlazorWASM.Servicios
{
    public class PostsServicio : IPostsServicio
    {
        private readonly HttpClient _cliente;

        public PostsServicio(HttpClient cliente)
        {
            _cliente = cliente;
        }

        public async Task<Post> ActualizarPost(int postId, Post post)
        {
            var content = JsonConvert.SerializeObject(post);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _cliente.PatchAsync($"{Inicializar.UrlBaseAPI}api/posts/{postId}", bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Post>(contentTemp);
                return result;
            }
            else
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ModelError>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<PostVM> CrearPost(PostVM post)
        {
            var content = JsonConvert.SerializeObject(post);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _cliente.PostAsync($"{Inicializar.UrlBaseAPI}api/posts", bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<PostVM>(contentTemp);
                return result;
            }
            else
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ModelError>(contentTemp);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<bool> EliminarPost(int postId)
        {
            var response = await _cliente.GetAsync($"{Inicializar.UrlBaseAPI}api/posts/{postId}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ModelError>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<Post> GetPost(int postId)
        {
            var response = await _cliente.GetAsync($"{Inicializar.UrlBaseAPI}api/posts/{postId}");
            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var post = JsonConvert.DeserializeObject<Post>(content);
                return post;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ModelError>(content);
                throw new  Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            var response = await _cliente.GetAsync($"{Inicializar.UrlBaseAPI}api/posts");
            var content = await response.Content.ReadAsStringAsync();
            var posts = JsonConvert.DeserializeObject<IEnumerable<Post>>(content);
            return posts;
        }

        public async Task<string> SubidaImagen(MultipartFormDataContent content)
        {
            var response = await _cliente.PostAsync($"{Inicializar.UrlBaseAPI}api/upload", content);
            var result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(result);

                return $"{Inicializar.UrlBaseAPI}{result}";
        }
    }
}
