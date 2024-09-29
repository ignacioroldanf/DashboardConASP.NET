using Microsoft.EntityFrameworkCore;
using WebHeladeria.Models;



namespace WebHeladeria.Servicios.Contrato
{
    public interface IUsuarioService
    {
        Task<Usuario> GetUsuario(string nombre, string contra);

        Task<Usuario> SaveUsuario(Usuario modelo);


    }
}
