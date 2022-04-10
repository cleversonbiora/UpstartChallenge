using AutoMapper;
using System;
using System.Collections.Generic;

namespace UpStart.Domain.AutoMapper
{
    public class Mapper
    {
        private const string InvalidOperationMessage = "Mapper not initialized. Call Initialize with appropriate configuration. If you are trying to use mapper instances through a container or otherwise, make sure you do not have any calls to the static Mapper.Map methods, and if you're using ProjectTo or UseAsDataSource extension methods, make sure you pass in the appropriate IConfigurationProvider instance.";
        private const string AlreadyInitialized = "Mapper already initialized. You must call Initialize once per application domain/process.";

        private static IConfigurationProvider _configuration;
        private static IMapper _instance;

        /// <summary>
        /// Configuration provider for performing maps
        /// </summary>
        public static IConfigurationProvider Configuration
        {
            get => _configuration ?? throw new InvalidOperationException(InvalidOperationMessage);
            private set => _configuration = (_configuration == null) ? value : throw new InvalidOperationException(AlreadyInitialized);
        }

        /// <summary>
        /// Static mapper instance. You can also create a <see cref="Mapper"/> instance directly using the <see cref="Configuration"/> instance.
        /// </summary>
        public static IMapper Instance
        {
            get => _instance ?? throw new InvalidOperationException(InvalidOperationMessage);
            private set => _instance = value;
        }

        /// <summary>
        /// Initialize static configuration instance
        /// </summary>
        /// <param name="config">Configuration action</param>
        public static void Initialize()
        {
            Configuration = new MapperConfiguration(cfg =>
            {
                AutoMapperConfiguration.RegisterMappings(cfg);
            });
            Instance = Configuration.CreateMapper();
        }

        /// <summary>
        /// Resets the mapper configuration. Not intended for production use, but for testing scenarios.
        /// </summary>
        public static void Reset()
        {
            _configuration = null;
            _instance = null;
        }

        /// <summary>
        /// Execute a mapping from the source object to a new destination object.
        /// The source type is inferred from the source object.
        /// </summary>
        /// <typeparam name="TDestination">Destination type to create</typeparam>
        /// <param name="source">Source object to map from</param>
        /// <returns>Mapped destination object</returns>
        public static TDestination Map<TDestination>(object source) => Instance.Map<TDestination>(source);

        /// <summary>
        /// Execute a mapping from the source object to a new destination object with supplied mapping options.
        /// </summary>
        /// <typeparam name="TDestination">Destination type to create</typeparam>
        /// <param name="source">Source object to map from</param>
        /// <param name="opts">Mapping options</param>
        /// <returns>Mapped destination object</returns>
        public static TDestination Map<TDestination>(object source, Action<IMappingOperationOptions> opts)
            => Instance.Map<TDestination>(source, opts);

        /// <summary>
        /// Execute a mapping from the source object to a new destination object.
        /// </summary>
        /// <typeparam name="TSource">Source type to use, regardless of the runtime type</typeparam>
        /// <typeparam name="TDestination">Destination type to create</typeparam>
        /// <param name="source">Source object to map from</param>
        /// <returns>Mapped destination object</returns>
        public static TDestination Map<TSource, TDestination>(TSource source)
            => Instance.Map<TSource, TDestination>(source);

        /// <summary>
        /// Execute a mapping from the source object to a new destination object with supplied mapping options.
        /// </summary>
        /// <typeparam name="TSource">Source type to use</typeparam>
        /// <typeparam name="TDestination">Destination type to create</typeparam>
        /// <param name="source">Source object to map from</param>
        /// <param name="opts">Mapping options</param>
        /// <returns>Mapped destination object</returns>
        public static TDestination Map<TSource, TDestination>(TSource source, Action<IMappingOperationOptions<TSource, TDestination>> opts)
            => Instance.Map(source, opts);

        /// <summary>
        /// Execute a mapping from the source object to the existing destination object.
        /// </summary>
        /// <typeparam name="TSource">Source type to use</typeparam>
        /// <typeparam name="TDestination">Dsetination type</typeparam>
        /// <param name="source">Source object to map from</param>
        /// <param name="destination">Destination object to map into</param>
        /// <returns>The mapped destination object, same instance as the <paramref name="destination"/> object</returns>
        public static TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
            => Instance.Map(source, destination);

        /// <summary>
        /// Execute a mapping from the source object to the existing destination object with supplied mapping options.
        /// </summary>
        /// <typeparam name="TSource">Source type to use</typeparam>
        /// <typeparam name="TDestination">Destination type</typeparam>
        /// <param name="source">Source object to map from</param>
        /// <param name="destination">Destination object to map into</param>
        /// <param name="opts">Mapping options</param>
        /// <returns>The mapped destination object, same instance as the <paramref name="destination"/> object</returns>
        public static TDestination Map<TSource, TDestination>(TSource source, TDestination destination, Action<IMappingOperationOptions<TSource, TDestination>> opts)
            => Instance.Map(source, destination, opts);

        /// <summary>
        /// Execute a mapping from the source object to a new destination object with explicit <see cref="System.Type"/> objects
        /// </summary>
        /// <param name="source">Source object to map from</param>
        /// <param name="sourceType">Source type to use</param>
        /// <param name="destinationType">Destination type to create</param>
        /// <returns>Mapped destination object</returns>
        public static object Map(object source, Type sourceType, Type destinationType)
            => Instance.Map(source, sourceType, destinationType);

        /// <summary>
        /// Execute a mapping from the source object to a new destination object with explicit <see cref="System.Type"/> objects and supplied mapping options.
        /// </summary>
        /// <param name="source">Source object to map from</param>
        /// <param name="sourceType">Source type to use</param>
        /// <param name="destinationType">Destination type to create</param>
        /// <param name="opts">Mapping options</param>
        /// <returns>Mapped destination object</returns>
        public static object Map(object source, Type sourceType, Type destinationType, Action<IMappingOperationOptions> opts)
            => Instance.Map(source, sourceType, destinationType, opts);

        /// <summary>
        /// Execute a mapping from the source object to existing destination object with explicit <see cref="System.Type"/> objects
        /// </summary>
        /// <param name="source">Source object to map from</param>
        /// <param name="destination">Destination object to map into</param>
        /// <param name="sourceType">Source type to use</param>
        /// <param name="destinationType">Destination type to use</param>
        /// <returns>Mapped destination object, same instance as the <paramref name="destination"/> object</returns>
        public static object Map(object source, object destination, Type sourceType, Type destinationType)
            => Instance.Map(source, destination, sourceType, destinationType);

        /// <summary>
        /// Execute a mapping from the source object to existing destination object with supplied mapping options and explicit <see cref="System.Type"/> objects
        /// </summary>
        /// <param name="source">Source object to map from</param>
        /// <param name="destination">Destination object to map into</param>
        /// <param name="sourceType">Source type to use</param>
        /// <param name="destinationType">Destination type to use</param>
        /// <param name="opts">Mapping options</param>
        /// <returns>Mapped destination object, same instance as the <paramref name="destination"/> object</returns>
        public static object Map(object source, object destination, Type sourceType, Type destinationType, Action<IMappingOperationOptions> opts)
            => Instance.Map(source, destination, sourceType, destinationType, opts);

        /// <summary>
        /// Dry run all configured type maps and throw <see cref="AutoMapperConfigurationException"/> for each problem
        /// </summary>
        public static void AssertConfigurationIsValid() => Configuration.AssertConfigurationIsValid();
    }

    public static class MapperListExtension
    {
        public static IEnumerable<TDestination> ToVMList<TDestination, TSource>(this IEnumerable<TSource> list)
        {
            return Mapper.Map<IEnumerable<TDestination>>(list);
        }

        public static List<TDestination> ToVMAsList<TDestination, TSource>(this IEnumerable<TSource> list)
        {
            return Mapper.Map<List<TDestination>>(list);
        }

        public static IEnumerable<TDestination> ToVMList<TDestination>(this object list)
        {
            return Mapper.Map<IEnumerable<TDestination>>(list);
        }
    }
}
