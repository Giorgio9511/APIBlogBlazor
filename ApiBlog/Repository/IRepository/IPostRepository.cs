using ApiBlog.Models;

namespace ApiBlog.Repository.IRepository
{
    public interface IPostRepository
    {
        ICollection<Post> GetPosts();
        Post GetPost(int id);
        bool ExistePost(string nombre, int? id = null);
        bool ExistePost(int id);
        bool CrearPost(Post post);
        bool ActualizarPost(Post post);
        bool BorrarPost(Post post);
        bool Guardar();
    }
}
