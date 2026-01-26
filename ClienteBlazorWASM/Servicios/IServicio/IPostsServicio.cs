using ClienteBlazorWASM.Models;
using ClienteBlazorWASM.Models.ViewModels;

namespace ClienteBlazorWASM.Servicios.IServicio
{
    public interface IPostsServicio
    {
        public Task<IEnumerable<Post>> GetPosts();
        public Task<PostActualizarVM> GetPost(int postId);
        public Task<PostVM> CrearPost(PostVM post);
        public Task<PostActualizarVM> ActualizarPost(int postId, PostActualizarVM post);
        public Task<bool> EliminarPost(int postId);
        public Task<string> SubidaImagen(MultipartFormDataContent content);
    }
}
