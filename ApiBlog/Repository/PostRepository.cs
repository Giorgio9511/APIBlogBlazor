using ApiBlog.Data;
using ApiBlog.Models;
using ApiBlog.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ApiBlog.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _bd;

        public PostRepository(ApplicationDbContext bd)
        {
            _bd = bd;
        }
        public bool ActualizarPost(Post post)
        {
            post.FechaActualizacion = DateTime.Now;
            var imagenDesdeBd = _bd.Post.AsNoTracking().FirstOrDefault(c => c.Id == post.Id);

            if (post.RutaImagen == null)
                post.RutaImagen = imagenDesdeBd.RutaImagen;

            _bd.Post.Update(post);
            return Guardar();
        }

        public bool BorrarPost(Post post)
        {
            _bd.Post.Remove(post);
            return Guardar();
        }

        public bool CrearPost(Post post)
        {
            post.FechaCreacion = DateTime.Now;

            _bd.Post.Add(post);
            return Guardar();
        }

        public bool ExistePost(string nombre, int? id = null)
        {
            bool valor = _bd.Post.Any(c => c.Titulo.ToLower().Trim() == nombre.ToLower().Trim() 
             && c.Id != id);
            return valor;
        }

        public bool ExistePost(int id)
        {
            return _bd.Post.Any(c => c.Id == id);
        }

        public Post GetPost(int id)
        {
            return _bd.Post.FirstOrDefault(c => c.Id == id);
        }

        public ICollection<Post> GetPosts()
        {
            return _bd.Post.OrderBy(c => c.Titulo).ToList();
        }

        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true : false;
        }
    }
}
