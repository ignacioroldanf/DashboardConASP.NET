using Microsoft.EntityFrameworkCore;
using WebHeladeria.Models;
using WebHeladeria.Servicios.Contrato;


namespace WebHeladeria.Servicios.Implementacion
{
    public class UsuarioService : IUsuarioService
    {
        private readonly HeladeriaContext _dbContext;
        
        public UsuarioService(HeladeriaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Usuario> GetUsuario(string nombre, string contra)
        {
            Usuario usuario_encontrado = await _dbContext.Usuarios.Where(x => x.NombreUsuario == nombre
            && x.ContraUsuario == contra).FirstOrDefaultAsync();

            return usuario_encontrado;
        }

        public async Task<Usuario> SaveUsuario(Usuario modelo)
        {
            _dbContext.Usuarios.Add(modelo);
            await _dbContext.SaveChangesAsync();

            return modelo;
        }
    }
}
