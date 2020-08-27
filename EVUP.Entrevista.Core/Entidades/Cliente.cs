﻿using EVUP.Entrevista.FMK;
using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
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
    }
}
