﻿using System.ComponentModel.DataAnnotations.Schema;

namespace mf_api_web_services_fuel_manager.Models
{
    [Table("VeiculoUsuarios")]
    public class VeiculoUsuarios
    {
        public int VeiculoId { get; set; }
        public Veiculo Veiculo {  set; get; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { set; get; }

        
    }
}
