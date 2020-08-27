using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVUP.Entrevista.FMK.Infraestrutura
{
    public interface IDependencyRegister
    {
        /// <summary>
        /// A classe que implementar, registra as dependencias no container
        /// </summary>
        /// <param name="builder"></param>
        void RegisterDependency(ContainerBuilder builder);
    }
}
