using Gimnasio.Dates;
using Gimnasio.Dominio.IRepository;
using Gimnasio.Transporte;
using Gimnasio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Gimnasio.Persistence
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;
        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<UsuarioDTO> ConsultarUsuario(int id)
        {
            UsuarioDTO? usuario = await (from t in _context.Usuario
                                         where t.Id == id
                                         select new UsuarioDTO
                                         {
                                             Id = t.Id,
                                             Nombre = t.Nombre,
                                             Apellidos = t.Apellidos,
                                             DNI = t.DNI,
                                             FechaNacimiento = t.FechaNacimiento,
                                             Direccion = t.Direccion,
                                             Poblacion = t.Poblacion,
                                             Telefono = t.Telefono,
                                             Email = t.Email,
                                             Foto = t.Foto
                                         }).FirstOrDefaultAsync();
            return usuario;
        }

        public async Task<bool> ModificarUsuario(UsuarioDTO usuario)
        {
            try
            {
                Usuario user = await (from t in _context.Usuario
                                      where t.Id == usuario.Id
                                      select t).FirstAsync();
                if (user == null) { return false; }
                user.Nombre = usuario.Nombre;
                user.Apellidos = usuario.Apellidos;
                user.DNI = usuario.DNI;
                user.FechaNacimiento = usuario.FechaNacimiento;
                user.Direccion = usuario.Direccion;
                user.Poblacion = usuario.Poblacion;
                user.Telefono = usuario.Telefono;
                user.Email = usuario.Email;
                if(user.Foto != null && user.Foto.Length>0)
                {
                    user.Foto = usuario.Foto;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RegistrarUsuario(UsuarioDTO usuarioFront)
        {
            try
            {
                Usuario usuario = new Usuario
                {
                    Nombre = usuarioFront.Nombre,
                    Apellidos = usuarioFront.Apellidos,
                    DNI = usuarioFront.DNI,
                    Direccion = usuarioFront.Direccion,
                    FechaNacimiento = usuarioFront.FechaNacimiento,
                    Poblacion = usuarioFront.Poblacion,
                    Telefono = usuarioFront.Telefono,
                    Email= usuarioFront.Email,
                    Foto= usuarioFront.Foto,
                    Password = usuarioFront.Password
                };
                usuario.Password = new PasswordHasher<Usuario>().HashPassword(usuario, usuario.Password);
                _context.Usuario.Add(usuario);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
