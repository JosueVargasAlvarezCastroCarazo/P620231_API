using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P620231_API.Attributes;
using P620231_API.Models;
using P620231_API.ModelsDTOs;
using P620231_API.Tools;

namespace P620231_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class UsersController : ControllerBase
    {
        private readonly P620231_AutoAppoContext _context;

        public UsersController(P620231_AutoAppoContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // GET: api/Users/GetUserByEmail
        [HttpGet("GetUserByEmail/{email}")]
        public ActionResult<IEnumerable<UserDTO>> GetUserByEmail(string email)
        {

            //consultas linq

            var query = (
                from u in _context.Users
                join ur in _context.UserRoles on u.UserRoleId equals ur.UserRoleId
                join us in _context.UserStatuses on u.UserStatusId equals us.UserStatusId
                where u.Email == email && us.UserStatusId == 1
                select new UserDTO (
                    u.UserId,
                    u.Name,
                    u.Email,
                    u.PhoneNumber,
                    u.LoginPassword,
                    u.CardId,
                    u.Address,
                    ur.UserRoleId,
                    us.UserStatusId,
                    ur.UserRoleDescription,
                    us.UserStatuDescription)
               
                /*
                {
                    idusuario = u.UserId,
                    nombre = u.Name,
                    correo = u.Email,
                    telefono = u.PhoneNumber,
                    contrasennia = u.LoginPassword,
                    cedula = u.CardId,
                    direccion = u.Address,
                    idrol = ur.UserRoleId,
                    rol = ur.UserRoleDescription,
                    idestado = us.UserStatusId,
                    estado = us.UserStatuDescription

                }*/


                ).ToList();

            /*
            List<UserDTO> list = new List<UserDTO>();

            foreach (var item in query)
            {
                UserDTO newItem = new UserDTO();

                newItem.IDUsuario = item.idusuario;
                newItem.Nombre = item.nombre;
                newItem.IDUsuario = item.idusuario;
                newItem.Correo = item.correo;
                newItem.NumeroTelefono = item.telefono;
                newItem.Contrasennia = item.contrasennia;
                newItem.Cedula = item.cedula;
                newItem.IDRole = item.idrol;
                newItem.Role = item.rol;
                newItem.IDEstado = item.idestado;
                newItem.Estado = item.estado;

                list.Add(newItem);
            }
            
             if(list == null){
                return NotFound();
             }
             
             */


            return query;
        }


        [HttpGet("ValidateUserLogin")]
        //this use query string
        public async Task<ActionResult<User>> ValidateUserLogin(string email, string password)
        {
           
            String EncriptedPassword = new Crypto().EncriptarEnUnSentido(password);
            
            var user = await _context.Users.SingleOrDefaultAsync(e => e.Email == email && e.LoginPassword == EncriptedPassword);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {

            user.LoginPassword = new Crypto().EncriptarEnUnSentido(user.LoginPassword);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
