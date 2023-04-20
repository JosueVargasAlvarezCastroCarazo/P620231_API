using P620231_API.Models;

namespace P620231_API.ModelsDTOs
{
    public class UserDTO
    {

        public UserDTO()
        {
      
        }

        public UserDTO(int iDUsuario, string nombre, string correo, string numeroTelefono, string contrasennia, string? cedula, string? direccion, int iDRole, int iDEstado, string role, string estado)
        {
            IDUsuario = iDUsuario;
            Nombre = nombre;
            Correo = correo;
            NumeroTelefono = numeroTelefono;
            Contrasennia = contrasennia;
            Cedula = cedula;
            Direccion = direccion;
            IDRole = iDRole;
            IDEstado = iDEstado;
            Role = role;
            Estado = estado;
        }

        public int IDUsuario { get; set; }
        public string Nombre { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string NumeroTelefono { get; set; } = null!;
        public string Contrasennia { get; set; } = null!;
        public string? Cedula { get; set; }
        public string? Direccion { get; set; }
        public int IDRole { get; set; }
        public int IDEstado { get; set; }

        public string? Role { get; set; } = null!;
        public string? Estado { get; set; } = null!;



    }
}
