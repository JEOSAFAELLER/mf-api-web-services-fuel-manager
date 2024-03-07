using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mf_api_web_services_fuel_manager.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public string Password { get; set; }

        public Perfil Perfil { get; set; }

        //um usuario possui varios veiculos
        public ICollection<VeiculoUsuarios> Veiculos { get; set; }

    }

    public enum Perfil
    {
        [Display(Name ="Administrador")]
        Administrador,
        [Display(Name = "Usuario")]
        Usuario
    }

}
