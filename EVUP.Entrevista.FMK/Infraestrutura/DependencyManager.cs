using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVUP.Entrevista.FMK.Infraestrutura
{
    public class DependencyManager
    {
        #region Singleton

        /// <summary>
        /// Implementação do pattern Singleton
        /// </summary>

        private static DependencyManager instance = null;
        private static readonly object padlock = new object();

        public static DependencyManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new DependencyManager();
                        }

                    }
                }
                return instance;
            }
        }

        #endregion

        private List<IDependencyRegister> ActionRegisters;

        private static IContainer container;

        /// <summary>
        /// método que recebe por parametro uma ação, esta deve conter os itens a serem registrados no injetor de dependência
        /// </summary>
        /// <param name="action">Ação com itens para registro</param>
        public void AddDependencyRegister(IDependencyRegister action)
        {
            if (ActionRegisters == null) ActionRegisters = new List<IDependencyRegister>();

            ActionRegisters.Add(action);
        }

        /// <summary>
        /// Método que executa o registro de todas as dependências do sistema
        /// </summary>
        public void RegisterAllDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterGeneric(typeof(NhRepository<>)).As(typeof(IRepository<>));

            foreach (var item in ActionRegisters)
            {
                item.RegisterDependency(builder);
            }

            container = builder.Build();
        }

        public static T Resolve<T>()
        {
            return container.Resolve<T>();
        }
    }
}
