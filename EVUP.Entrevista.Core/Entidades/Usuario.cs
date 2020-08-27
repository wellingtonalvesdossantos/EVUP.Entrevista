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
    public class Usuario : BaseEntity
    {
        [Id(0, Name = "Id", Type = "Int64")]
        [Generator(1, Class = "native")]
        public override long Id { get; set; }

        [Property]
        public virtual string Nome { get; set; }

        [Property]
        public virtual string Login { get; set; }

        [Property]
        public virtual bool IsAdmin { get; set; }
    }
}
