using EVUP.Entrevista.Core.Entidades;
using EVUP.Entrevista.FMK;
using EVUP.Entrevista.FMK.Infraestrutura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVUP.Entrevista.Core.Infraestrutura
{
    public class BancoDadosManager
    {
        public BancoDadosManager()
        {
            var repositorio = DependencyManager.Resolve<IRepository<MigrationInfo>>();

            var existMigrationTable = repositorio
                .CreateSQLQuery(@"SELECT count(*)
                FROM sys.objects 
                WHERE object_id = OBJECT_ID(N'[dbo].[MigrationInfo]') 
                AND type in (N'U')")
                .UniqueResult<int>();

            if (existMigrationTable == 0)
            {
                repositorio
                    .CreateSQLQuery(
                    @"create table MigrationInfo (id bigint identity, name varchar(100) not null)")
                    .ExecuteUpdate();
            }

            var migrations = repositorio.Table.Select(m => m.Name).ToList();

            #region MIGRAÇÃO 1 - NÃO ALTERAR ESSE BLOCO

            var name = "inicialização";
            if (!migrations.Contains(name))
            {
                repositorio
                    .CreateSQLQuery(
                    @"create table Cliente (
                        id bigint identity, 
                        nome varchar(100) not null,
                        telefone varchar(100),
                        endereco varchar(100))")
                    .ExecuteUpdate();

                using (var trans = repositorio.BeginTransaction())
                {
                    repositorio.Insert(new MigrationInfo() { Name = name });

                    if (trans != null) repositorio.Commit();
                }
            }

            #endregion

            #region MIGRAÇÃO 2

            //ESCREVA AQUI MIGRAÇÃO

            #endregion
        }
    }
}
