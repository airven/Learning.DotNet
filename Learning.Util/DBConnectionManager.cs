using Common.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace Learning.Util
{
    /// <summary>
    /// Manages a collection of IDbProviders, and provides transparent access
    /// to their database.
    /// </summary>
    /// <seealso cref="IDbProvider" />
    /// <author>James House</author>
    /// <author>Sharada Jambula</author>
    /// <author>Mohammad Rezaei</author>
    /// <author>Marko Lahma (.NET)</author>
    public class DBConnectionManager : IDbConnectionManager
	{        
        private static readonly DBConnectionManager instance = new DBConnectionManager();
	    private static readonly ILog log = LogManager.GetLogger(typeof (DBConnectionManager));

        private readonly Dictionary<string, IDbProvider> providers = new Dictionary<string, IDbProvider>();

		/// <summary> 
		/// Get the class instance.
		/// </summary>
		/// <returns> an instance of this class
		/// </returns>
		public static IDbConnectionManager Instance
		{
			get
			{
				// since the instance variable is initialized at class loading time,
				// it's not necessary to synchronize this method */
				return instance;
			}
		}


		/// <summary> 
		/// Private constructor
		/// </summary>
		private DBConnectionManager()
		{
		}

        /// <summary>
        /// Adds the connection provider.
        /// </summary>
        /// <param name="dataSourceName">Name of the data source.</param>
        /// <param name="provider">The provider.</param>
        public virtual void AddConnectionProvider(string dataSourceName, IDbProvider provider)
		{
            log.Info(string.Format("Registering datasource '{0}' with db provider: '{1}'", dataSourceName, provider));
			providers[dataSourceName] = provider;
		}

		/// <summary>
		/// Get a database connection from the DataSource with the given name.
		/// </summary>
		/// <returns> a database connection </returns>
        public virtual IDbConnection GetConnection(string dataSourceName)
		{
            IDbProvider provider = GetDbProvider(dataSourceName);

			return provider.CreateConnection();
		}

		/// <summary> 
		/// Shuts down database connections from the DataSource with the given name,
		/// if applicable for the underlying provider.
		/// </summary>
		public virtual void Shutdown(string dsName)
		{
		    IDbProvider provider = GetDbProvider(dsName);
			provider.Shutdown();
		}

	    public DbMetadata GetDbMetadata(string dsName)
	    {
            return GetDbProvider(dsName).Metadata;
        }

        /// <summary>
        /// Gets the db provider.
        /// </summary>
        /// <param name="dsName">Name of the ds.</param>
        /// <returns></returns>
	    public IDbProvider GetDbProvider(string dsName)
	    {
            if (String.IsNullOrEmpty(dsName))
            {
                throw new ArgumentException("DataSource name cannot be null or empty", "dsName");
            }

            IDbProvider provider;
            providers.TryGetValue(dsName, out provider);
            if (provider == null)
            {
                throw new Exception(string.Format(CultureInfo.InvariantCulture, "There is no DataSource named '{0}'", dsName));
            }
            return provider;
        }
	}

    public interface IDbProvider
    {
        /// <summary>
        /// Initializes the db provider implementation.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Returns a new command object for executing SQL statments/Stored Procedures
        /// against the database.
        /// </summary>
        /// <returns>An new <see cref="IDbCommand"/></returns>
        IDbCommand CreateCommand();

        /// <summary>
        /// Returns a new instance of the providers CommandBuilder class.
        /// </summary>
        /// <remarks>In .NET 1.1 there was no common base class or interface
        /// for command builders, hence the return signature is object to
        /// be portable (but more loosely typed) across .NET 1.1/2.0</remarks>
        /// <returns>A new Command Builder</returns>
        object CreateCommandBuilder();

        /// <summary>
        /// Returns a new connection object to communicate with the database.
        /// </summary>
        /// <returns>A new <see cref="IDbConnection"/></returns>
        IDbConnection CreateConnection();

        /// <summary>
        /// Returns a new parameter object for binding values to parameter
        /// placeholders in SQL statements or Stored Procedure variables.
        /// </summary> 
        /// <returns>A new <see cref="IDbDataParameter"/></returns>
        IDbDataParameter CreateParameter();

        /// <summary>
        /// Connection string used to create connections.
        /// </summary>
        string ConnectionString { set; get; }

        DbMetadata Metadata { get; }

        /// <summary>
        /// Shutdowns this instance.
        /// </summary>
        void Shutdown();
    }
}
