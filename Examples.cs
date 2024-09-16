/// <summary>
/// Mapping configuration for a specific sql table to a specific class.
/// </summary>
/// <param name="Accessor">Used to access the target class properties.</param>
/// <param name="PropToRowIdxDict">Target class property name -> database reader row idx dictionary.</param>
internal record RowMapper(TypeAccessor Accessor, IDictionary<string, int> PropToRowIdxDict);

public class RawSqlHelperService
{
  /// <summary>
  /// Create a new mapper for the conversion of a <see cref="DbDataReader"/> row -> <typeparamref name="T"/>.
  /// </summary>
  /// <typeparam name="T">Target class to use.</typeparam>
  /// <param name="reader">Data reader to obtain column information from.</param>
  /// <returns>Row mapper object for <see cref="DbDataReader"/> row -> <typeparamref name="T"/>.</returns>
  private RowMapper GetRowMapper<T>(DbDataReader reader) where T : class, new()
  {
    var accessor = TypeAccessor.Create(typeof(T));
    var members = accessor.GetMembers();

    // Column name -> column idx dict
    var columnIdxDict = Enumerable.Range(0, reader.FieldCount).ToDictionary(idx => reader.GetName(idx), idx => idx);
    var propToRowIdxDict = members
      .Where(m => m.GetAttribute(typeof(NotMappedAttribute), false) == null)
      .Select(m =>
      {
        var columnAttr = m.GetAttribute(typeof(ColumnAttribute), false) as ColumnAttribute;
        var columnName = columnAttr == null
          ? m.Name
          : columnAttr.Name;

        return (PropertyName: m.Name, ColumnName: columnName);
      })
      .ToDictionary(x => x.PropertyName, x => columnIdxDict[x.ColumnName]);

    return new RowMapper(accessor, propToRowIdxDict);
  }

  /// <summary>
  /// Read <see cref="DbDataReader"/> current row as object <typeparamref name="T"/>.
  /// </summary>
  /// <typeparam name="T">The class to map to.</typeparam>
  /// <param name="reader">Data reader to read the current row from.</param>
  /// <param name="mapper">Mapping configuration to use to perform the mapping operation.</param>
  /// <returns>Resulting object of the mapping operation.</returns>
  private T ReadRowAsObject<T>(DbDataReader reader, RowMapper mapper) where T : class, new()
  {
    var (accessor, propToRowIdxDict) = mapper;
    var t = new T();

    foreach (var (propertyName, columnIdx) in propToRowIdxDict)
      accessor[t, propertyName] = reader.GetValue(columnIdx);

    return t;
  }

  /// <summary>
  /// Execute the specified <paramref name="sql"/> query and automatically map the resulting rows to <typeparamref name="T"/>.
  /// </summary>
  /// <typeparam name="T">Target class to map to.</typeparam>
  /// <param name="dbContext">Database context to perform the operation on.</param>
  /// <param name="sql">SQL query to execute.</param>
  /// <param name="parameters">Additional list of parameters to use for the query.</param>
  /// <returns>Result of the SQL query mapped to a list of <typeparamref name="T"/>.</returns>
  public async Task<IEnumerable<T>> ExecuteSql<T>(DbContext dbContext, string sql, IEnumerable<DbParameter> parameters = null) where T : class, new()
  {
    var con = dbContext.Database.GetDbConnection();
    await con.OpenAsync();

    var cmd = con.CreateCommand() as OracleCommand;
    cmd.BindByName = true;
    cmd.CommandText = sql;
    cmd.Parameters.AddRange(parameters?.ToArray() ?? new DbParameter[0]);

    var reader = await cmd.ExecuteReaderAsync();
    var records = new List<T>();
    var mapper = GetRowMapper<T>(reader);

    while (await reader.ReadAsync())
    {
      records.Add(ReadRowAsObject<T>(reader, mapper));
    }

    await con.CloseAsync();

    return records;
  }
}
Map


public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}

public static T MapDataRowToClass<T>(DataRow row) where T : new()
{
    T obj = new T();
    foreach (var prop in typeof(T).GetProperties())
    {
        if (row.Table.Columns.Contains(prop.Name) && row[prop.Name] != DBNull.Value)
        {
            prop.SetValue(obj, row[prop.Name]);
        }
    }
    return obj;
}
// https://www.romacode.com/blog/c-helper-functions-to-map-a-datatable-or-datarow-to-a-class-object


 public static void MapDataToObject<T>(this SqlDataReader dataReader, T newObject)
    {
        if (newObject == null) throw new ArgumentNullException(nameof(newObject));

        // Fast Member Usage
        var objectMemberAccessor = TypeAccessor.Create(newObject.GetType());
        var propertiesHashSet =
                objectMemberAccessor
                .GetMembers()
                .Select(mp => mp.Name)
                .ToHashSet(StringComparer.InvariantCultureIgnoreCase);

        for (int i = 0; i < dataReader.FieldCount; i++)
        {
            var name = propertiesHashSet.FirstOrDefault(a => a.Equals(dataReader.GetName(i), StringComparison.InvariantCultureIgnoreCase));
            if (!String.IsNullOrEmpty(name))
            {
                //Attention! if you are getting errors here, then double check that your model and sql have matching types for the field name.
                //Check api.log for error message!
                objectMemberAccessor[newObject, name]
                    = dataReader.IsDBNull(i) ? null : dataReader.GetValue(i);
            }
        }
    }
	
	
	
	https://stackoverflow.com/questions/11270999/how-can-i-map-the-results-of-a-sql-query-onto-objects
	
	db command creator
	https://gist.github.com/dibley1973/bf4391ee08554e46dcd3
	
	
	// Create Class instance
	
using System;
using System.Reflection;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessagingStandards.Entities.SWIFT.MT.Tags
{
    public interface ITag
    {
        string TagName { get; set; }
        string Qualifier { get; set; }
        string Type { get; set; }
        string Code { get; set; }
        string Value { get; set; }
        string Description { get; set; }


        ITag GetTagValues(string resultText);
    }
}		

 private void LoadIBusinessClass()
        {
            _mappings = new Dictionary<string, Type>();

            Type[] mappingTypes = Assembly.GetExecutingAssembly().GetTypes();

            foreach (Type type in mappingTypes)
            {
                if (type.GetInterface(typeof(ITag).ToString()) != null)
                {
                    _mappings.Add(type.Name.ToUpper(), type);
                }

            }
        }


   public List<ITag> CreateInstance(string parsedSwiftTag, List<ITag> listOfITags)
        {
            string tagID = parsedSwiftTag.Substring(1, 3);
            Type t = GetITagToCreate(tagID.TrimColon());

            if (t != null)
            {
                ITag t1 = Activator.CreateInstance(t) as ITag;
                t1.GetTagValues(parsedSwiftTag);
                listOfITags.Add(t1);
            }

            return listOfITags;
        }
		
		

namespace MyApplication
{
    class Application
    {
        static void Main()
        {
            Type type = Type.GetType("MyApplication.Action");
            if (type == null)
            {
                throw new Exception("Type not found.");
            }
            var instance = Activator.CreateInstance(type);
            //or
            var newClass = System.Reflection.Assembly.GetAssembly(type).CreateInstance("MyApplication.Action");
        }
    }

    public class Action
    {
        public string key { get; set; }
        public string Value { get; set; }
    }
}

https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/covariance-contravariance/creating-variant-generic-interfaces

private void LoadITagBusinessClass()
        {
            _mappings = new Dictionary<string, Type>();

            Type[] mappingTypes = Assembly.GetExecutingAssembly().GetTypes();

            foreach (Type type in mappingTypes)
            {
                if (type.GetInterface(typeof(ITagBusinessClass).ToString()) != null)
                {
                    _mappings.Add(type.Name.ToUpper(), type);
                }

            }
        }

namespace MessagingStandards.Entities.SWIFT.MT.Tags
{
    public interface ITagBusinessClass
    {
        string TagName { get; set; }
        string Qualifier { get; set; }
        string Type { get; set; }
        string Code { get; set; }
        string Value { get; set; }
        string Description { get; set; }


        T MapSwiftDataToObject<T>(string resultText) where T : class, new();
    }
}		

interface ICovariant<out R>
{
    R GetSomething();
}
class SampleImplementation<R> : ICovariant<R>
{
    public R GetSomething()
    {
        // Some code.
        return default(R);
    }
}interface ICovariant<out R>
{
    R GetSomething();
}
class SampleImplementation<R> : ICovariant<R>
{
    public R GetSomething()
    {
        // Some code.
        return default(R);
    }
}

https://dotnetteach.com/blog/generic-interfaces-in-csharp
public interface IRepository<T>
{
    void Add(T item);
    T FindById(int id);
    IEnumerable<T> GetAll();
}
public class ProductRepository : IRepository<Product>
{
    private List<Product> _products = new List<Product>();

    public void Add(Product item)
    {
        _products.Add(item);
    }

    public Product FindById(int id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }

    public IEnumerable<Product> GetAll()
    {
        return _products;
    }
}
public class CustomerRepository : IRepository<Customer>
{
    private List<Customer> _customers = new List<Customer>();

    public void Add(Customer item)
    {
        _customers.Add(item);
    }

    public Customer FindById(int id)
    {
        return _customers.FirstOrDefault(c => c.Id == id);
    }

    public IEnumerable<Customer> GetAll()
    {
        return _customers;
    }
}
public interface IRepository<T> where T : IEntity
{
    void Add(T item);
    T FindById(int id);
    IEnumerable<T> GetAll();
}
namespace AspNetCoreWebApi6.Models
{
    public interface InterfaceTest
    {
        T Mytest<T>(string param);
        T Mytest<T>(string param, T Myobjet);
    }
}
namespace AspNetCoreWebApi6.Models
{
    public class TestInterface : InterfaceTest
    {
        string test1 { get; set; }
        Movie movie= new Movie();
       


        public T Mytest<T>(string param, T movie)
        {
            this.movie.Title = "AA";
            this.movie.ReleaseDate = DateTime.Now;

            return movie;
        }

        T InterfaceTest.Mytest<T>(string param)
        {
            throw new NotImplementedException();
        }
    }
}
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWebApi6.Models
{
    public class MovieContext : DbContext
    {
        //private Movie movie;

        public MovieContext(DbContextOptions<MovieContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; } = null!;
        public void mytest()
        {
            TestInterface test1= new TestInterface();
            Movie m = new();
            Movie movie = test1.Mytest<Movie>("testinG",m);
        }
    }
}
