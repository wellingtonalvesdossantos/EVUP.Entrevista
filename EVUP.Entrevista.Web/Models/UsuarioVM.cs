using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace EVUP.Entrevista.Web.Models
{
    public class UsuarioVM
    {
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Display(Name = "É Administrador?")]
        public bool IsAdmin { get; set; }

    }
}