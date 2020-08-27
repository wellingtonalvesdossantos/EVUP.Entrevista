using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EVUP.Entrevista.FMK
{
    public class NhConfiguration : NHibernate.Cfg.Configuration
    {
        public static NhConfiguration Instance { get; set; }

        public NhConfiguration(string connectionString, List<string> assemblieNames)
        {
            SetProperty(NHibernate.Cfg.Environment.ConnectionString, connectionString);
            SetProperty(NHibernate.Cfg.Environment.ConnectionProvider, "NHibernate.Connection.DriverConnectionProvider");
            SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, "NHibernate.Driver.SqlClientDriver");
            SetProperty(NHibernate.Cfg.Environment.Dialect, "NHibernate.Dialect.MsSql2008Dialect");
            SetProperty(NHibernate.Cfg.Environment.CurrentSessionContextClass, "call");

            RegisterAssemblies(assemblieNames);

            NhConfiguration.Instance = this;
        }

        void RegisterAssemblies(List<string> assemblieNames)
        {
            HbmSerializer.Default.Validate = true;

            HbmSerializer.Default.WriteDateComment = false;

            foreach (var item in assemblieNames)
            {
                var stream = new MemoryStream();

                HbmSerializer.Default.Serialize(stream, Assembly.LoadFrom(item));

                var buffer = new byte[stream.Length];

                stream.Seek(0, SeekOrigin.Begin);

                stream.Read(buffer, 0, buffer.Length);

                AddXmlString(Encoding.Default.GetString(buffer).Substring(3));
            }
        }
    }
}
