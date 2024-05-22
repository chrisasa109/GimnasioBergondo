using Gimnasio.Dates;
using Gimnasio.Dominio.IRepository;
using Gimnasio.Transporte;
using Gimnasio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Gimnasio.Service;

namespace Gimnasio.Persistence
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly SessionService _usuarioService;
        public UsuarioRepository(ApplicationDbContext context, SessionService iUsuarioService)
        {
            _context = context;
            _usuarioService = iUsuarioService;
        }

        private int AsignarID(int? id)
        {
            if (id == null || _usuarioService.EsCliente())
            {
                return _usuarioService.ObtenerUsuario().Id;
            }
            return (int)id;
        }

        public async Task<bool> CambiarRol(int idUsuario, string rolFronted)
        {
            try
            {
                Usuario? usuario = await(from t in _context.Usuario
                                         where t.Id == idUsuario
                                         select t).FirstOrDefaultAsync();
                if (Enum.TryParse(rolFronted, true, out Usuario.Role rolFormateado))
                {
                    usuario.Rol = rolFormateado;
                    _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<UsuarioDTO> ConsultarUsuario(int? id)
        {
            int idDef = AsignarID(id);
            UsuarioDTO? usuario = await (from t in _context.Usuario
                                         where t.Id == idDef
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

        public async Task<bool> EliminarUsuario(int? v)
        {
            int id = AsignarID(v);
            try
            {
                Usuario? usuario = await(from t in _context.Usuario
                                        where t.Id == id
                                        select t).FirstOrDefaultAsync();
                if(usuario == null)
                {
                    return false;
                }
                else
                {
                    _context.Usuario.Remove(usuario);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
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
                if(usuario.Foto != null && usuario.Foto.Length>0)
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

        public async Task<List<UsuarioDTO>> ObtenerListaUsuarios()
        {
            var query = _context.Usuario.AsQueryable();

            if (!_usuarioService.EsAdministrador())
            {
                query = query.Where(t => t.Rol == Usuario.Role.CLIENTE);
            }

            List<UsuarioDTO> lista = await query.Select(t => new UsuarioDTO
            {
                Id = t.Id,
                Nombre = t.Nombre,
                Apellidos = t.Apellidos,
                DNI = t.DNI,
                FechaNacimiento = t.FechaNacimiento,
                Direccion = t.Direccion,
                Poblacion = t.Poblacion,
                Telefono = t.Telefono,
                Rol = (UsuarioDTO.Role)t.Rol,
                Email = t.Email,
            }).ToListAsync();

            return lista;
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
