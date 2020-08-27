using EVUP.Entrevista.Core.ExtensionMethods;
using EVUP.Entrevista.FMK;
using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVUP.Entrevista.Core.Entidades
{
    [Class]
    public class Cliente : BaseEntity
    {
        [Id(0, Name = "Id", Type = "Int64")]
        [Generator(1, Class = "native")]
        public override long Id { get; set; }

        [Property]
        public virtual string Nome { get; set; }

        [Property]
        public virtual string Endereco { get; set; }

        [Property]
        public virtual string Telefone { get; set; }

        [Property]
        [EmailAddress]
        public virtual string Email { get; set; }

        [Property]
        public virtual string Cidade { get; set; }

        [Property]
        [GeneroValidation]
        public virtual string Genero { get; set; }

        [Property]
        public virtual long? UsuarioId { get; set; }
    }

    public class GeneroValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var obj = (Cliente)validationContext.ObjectInstance;
            if (obj.Genero.IsInList("MASCULINO", "FEMININO"))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Gênero inválido");
            }
        }
    }
}
