﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EVUP.Entrevista.Web.Models
{
    public class ClienteVM
    {
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name ="Nome")]
        public string Nome { get; set; }

        [StringLength(20)]
        [Display(Name = "Telefone")]
        public string Telefone { get; set; }

        [StringLength(100)]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [StringLength(100)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [StringLength(100)]
        [Display(Name = "Cidade")]
        public string Cidade { get; set; }

        [Required]
        [Display(Name = "Gênero")]
        public string Genero { get; set; }

        [Display(Name = "Usuário criador")]
        public long? UsuarioId { get; set; }
    }
}