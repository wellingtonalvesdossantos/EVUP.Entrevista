using EVUP.Entrevista.FMK;
using EVUP.Entrevista.FMK.Infraestrutura;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using EVUP.Entrevista.Core.Infraestrutura;

namespace EVUP.Entrevista.Web
{
    public class Startup
    {
        /// <summary>
        /// Configurações gerais da aplicação
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            DependencyManager.Instance.AddDependencyRegister(new EVUP.Entrevista.Core.Infraestrutura.DependencyRegister());
            DependencyManager.Instance.AddDependencyRegister(new EVUP.Entrevista.Web.Infraestrutura.DependencyRegister());
            DependencyManager.Instance.RegisterAllDependencies();
            ConfigurarBanco();
        }

        void ConfigurarBanco()
        {
            string entitiesAssemblyPath = System.Reflection.Assembly.GetAssembly(typeof(EVUP.Entrevista.Core.Infraestrutura.DependencyRegister)).Location;

            new NhConfiguration(
                ConfigurationManager.ConnectionStrings["default"].ConnectionString,
                new List<string>() { entitiesAssemblyPath });

            new BancoDadosManager();
        }
    }
}