using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace Shared.Common
{
    internal interface ITypeConfig { }

    /// <summary>
    ///
    ///      public class MyStronglyTypedConfig : TypeConfig<MyStronglyTypedConfig>
    ///      {
    ///          protected override string SectionName => "ConfigSectionNameInMyJsonConfigFile";
    ///          public string ConfigValue { get; set; } = "DefaultValueIfConfigNotPresent";
    ///      }    
    ///      ...
    ///      Create, bind and use from IConfiguration:
    ///      var myConfig = MyStronglyTypedConfig.Create(configuration);
    ///      ..
    ///      var value = myConfig.ConfigValue
    ///      ...     
    ///      Create instance of thread safe singleton:
    ///      var myConfig = MyStronglyTypedConfig.New(configuration);
    ///      ...
    ///      var value = MyStronglyTypedConfig.Current.ConfigValue
    /// 
    /// </summary>
    ///     
    public abstract class TypeConfig<T> : ITypeConfig where T : TypeConfig<T>, new()
    {
        protected abstract string SectionName { get; }
        protected internal TypeConfig() { }

        public TypeConfig<T> Bind(IConfiguration config)
        {
            config.Bind(SectionName, this);
            return this;
        }

        public TypeConfig(IConfiguration config) => Bind(config);

        private static Lazy<TypeConfig<T>> lazy;

        public static T Current => lazy?.Value as T;
        public static T CurrentOrNew => lazy?.Value as T ?? new T();

        public static T Create(IConfiguration config) => new T().Bind(config) as T;

        public static T New(IConfiguration config)
        {
            var instance = Create(config);
            lazy = new Lazy<TypeConfig<T>>(() => instance, true);
            return instance;
        }
    }

    /// <summary>
    /// 
    /// Create new instances and bind configuration to all types of TypeConfig<T> in project
    /// TypeConfig.CreateAll(configuration)
    /// 
    /// </summary>
    public static class TypeConfig
    {
        private static readonly IEnumerable<TypeInfo> types;

        public static void CreateAll(IConfiguration config)
        {
            foreach(var type in types)
            {
                var method = type.GetMethod("New", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
                if (method == null)
                {
                    continue;
                }
                method.Invoke(null, new object[] { config });
            }
        }

        static TypeConfig()
        {
            var entry = Assembly.GetEntryAssembly();
            var assemblies = new List<AssemblyName>(entry
                .GetReferencedAssemblies()
                .Where(a => a.GetPublicKeyToken().Length == 0)) { entry.GetName() };
            types = assemblies
                .Select(Assembly.Load)
                .SelectMany(x => x.DefinedTypes)
                .Where(c => c != typeof(TypeConfig<>) && !c.IsAbstract && !c.IsInterface && typeof(ITypeConfig).IsAssignableFrom(c));
        }
    }
}
