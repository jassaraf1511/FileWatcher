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
	


https://stackoverflow.com/questions/1383406/using-stored-procedures-in-c-sharp
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
-------

namespace AspNetCoreWebApi6.Models
{
    public interface InterfaceTest<T>   
    {
        public T MyTest<T>(string param);
        T Mytest<T>(string param, T Myobjet);
    }
}

using System.Data.Common;

namespace AspNetCoreWebApi6.Models
{
    public class TestInterface : InterfaceTest<Movie>
    {
        Movie ? movie;
        

        public Movie Mytest<T>(string param, T movie)
        {
            this.movie=new Movie();
            this.movie.Title = param;   
            this.movie.Title = "AA";
            this.movie.ReleaseDate = DateTime.Now;

            return this.movie;
        }

        public string MyTest<T>(string param)
        {
            return "";
        }

        
    }
}


using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System;
namespace AspNetCoreWebApi6.Models
{
    public class MovieContext : DbContext
    {
        private const string TypeName = "AspNetCoreWebApi6";

        public MovieContext(DbContextOptions<MovieContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; } = null!;

        public void test()
        {
            TestInterface testa= new TestInterface();
            Movie movie = testa.Mytest("hello", new Movie());
        }
   
    }
    
}

https://theburningmonk.com/2010/09/net-tips-how-to-determine-if-a-type-implements-a-generic-interface-type/
https://spin.atomicobject.com/generics-type-constraints/
----------------------------


	using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using Cfsb.Incoming.FedWires.Utils;
using Cfsb.LoggerWriter.Services;
using Cfsb.Incoming.FedWires.DataEntities;
using Cfsb.Incoming.FedWires.MessageStructure;
using System.Linq;
namespace Cfsb.Incoming.FedWires.Services
{
    public class ProcessResults
    {
        private string outputResultDataFile;

        private LogWriter log;
        private InterfaceDetail appSettings;
        private string csvHeader;
        private StreamWriter outPutFileResult;
        private FedWireMessage fedMessage;
        private string wireDataFile;
        private string inOutFile;
        private DateTime fileReceiveDate;

        OutputResultMessage resultMessage;
        public ProcessResults(LogWriter log, InterfaceDetail appSettings, string wireDataFile, string inOutFile, DateTime fileReceiveDate)
        {
            this.log = log;
            this.appSettings = appSettings;

            this.wireDataFile = wireDataFile;
            this.inOutFile = inOutFile;
            this.fileReceiveDate= fileReceiveDate;
            initialize();

        }

        public string OutputResultDataFile { get => outputResultDataFile; set => outputResultDataFile = value; }
        public LogWriter Log { get => log; set => log = value; }
        public InterfaceDetail AppSettings { get => appSettings; set => appSettings = value; }
        public string WireDataFile { get => wireDataFile; set => wireDataFile = value; }
        public string InOutFile { get => inOutFile; set => inOutFile = value; }
        public DateTime FileReceiveDate { get => fileReceiveDate; set => fileReceiveDate = value; }

        public int storeMessage(FedWireMessage fedMessage)
        {
            this.fedMessage = fedMessage;
            resultMessage = new OutputResultMessage();
            try
            {


                resultMessage.OMAD = !string.IsNullOrEmpty(fedMessage.Omad) ? fedMessage.Omad : "";
                resultMessage.IMAD = !string.IsNullOrEmpty(fedMessage.Imad) ? fedMessage.Imad : "";
                resultMessage.MessageDuplication = !string.IsNullOrEmpty(fedMessage.MessageDisposition.MessageDuplicationCode) ? fedMessage.MessageDisposition.MessageDuplicationCode : "";
                resultMessage.MessageStatus = !string.IsNullOrEmpty(fedMessage.MessageDisposition.MessageStatusStr) ? fedMessage.MessageDisposition.MessageDuplicationCode : "";
                resultMessage.InputCycleDate = !string.IsNullOrEmpty(fedMessage.InputMessageAccountabilityData.InputCycleDate_CCYYMMDD) ? fedMessage.InputMessageAccountabilityData.InputCycleDate_CCYYMMDD : "";

                resultMessage.OutputCycleDateDateReceived = !string.IsNullOrEmpty(fedMessage.OutputMessageAccountabilityData.InputCycleDate_CCYYMMDD) ? fedMessage.OutputMessageAccountabilityData.InputCycleDate_CCYYMMDD : "";

                resultMessage.WIREINOUT = fedMessage.IncomingOutGoing;
                resultMessage.TypeSubtype = !string.IsNullOrEmpty(fedMessage.TypeSubType.MessageTypeSubType) ? fedMessage.TypeSubType.MessageTypeSubType : "";

                resultMessage.TypeCode = !string.IsNullOrEmpty(fedMessage.TypeSubType.TransactionTypeName) ?  fedMessage.TypeSubType.TransactionTypeName : "";
                resultMessage.TypeSubCode = !string.IsNullOrEmpty(fedMessage.TypeSubType.TransactionSubTypeName) ? fedMessage.TypeSubType.TransactionSubTypeName : "";

                resultMessage.SenderABA = !string.IsNullOrEmpty(fedMessage.SenderDI.SenderReceiverABA) ? fedMessage.SenderDI.SenderReceiverABA : "";
                resultMessage.SenderName = !string.IsNullOrEmpty(fedMessage.SenderDI.SenderReceiverShortName) ? fedMessage.SenderDI.SenderReceiverShortName : "";
                resultMessage.SenderReference = !string.IsNullOrEmpty(fedMessage.SenderReference) ? fedMessage.SenderReference : "";
                resultMessage.ReceiverABA = !string.IsNullOrEmpty(fedMessage.ReceiverDI.SenderReceiverABA) ? fedMessage.ReceiverDI.SenderReceiverABA : "";
                resultMessage.ReceiverName = !string.IsNullOrEmpty(fedMessage.ReceiverDI.SenderReceiverShortName) ? fedMessage.ReceiverDI.SenderReceiverShortName : "";
                resultMessage.BusinessFunction = !string.IsNullOrEmpty(fedMessage.BusinessFunctionCode) ? fedMessage.BusinessFunctionCode : "";
                resultMessage.TransactionCode = "";
                resultMessage.PaymentNotificationContactName = fedMessage.Paymentnotification != null && !string.IsNullOrEmpty(fedMessage.Paymentnotification.ContactName) ? fedMessage.Paymentnotification.ContactName : "";
                resultMessage.TransactionAmount = String.Format("{0:###,###,###,###.00}", fedMessage.Amount.TransactionAmount);
                resultMessage.InstructedAmountCurrency = fedMessage.InstructedAmount != null && !string.IsNullOrEmpty(fedMessage.InstructedAmount.Currency) ? fedMessage.InstructedAmount.Currency : ""; ;
                resultMessage.InstructedAmount = fedMessage.InstructedAmount != null && fedMessage.InstructedAmount != null ? String.Format("{0:###,###,###,###.00}", fedMessage.InstructedAmount.Amount) : "";
                resultMessage.IntemediaryFIIdentifier = !string.IsNullOrEmpty(fedMessage.IntermediaryFI.AccountIdentifier) ? fedMessage.IntermediaryFI.AccountIdentifier : "";
                resultMessage.IntermediaryFIName = !string.IsNullOrEmpty(fedMessage.IntermediaryFI.AccountName) ? fedMessage.IntermediaryFI.AccountName : "";
                resultMessage.BeneficiaryFIIdentifierAccount = !string.IsNullOrEmpty(fedMessage.BeneficiaryFI.AccountIdentifier) ? fedMessage.BeneficiaryFI.AccountIdentifier : "";
                resultMessage.BeneficiaryFIName = !string.IsNullOrEmpty(fedMessage.BeneficiaryFI.AccountName) ? fedMessage.BeneficiaryFI.AccountName : "";
                resultMessage.BeneficiaryIdentifierAccount = !string.IsNullOrEmpty(fedMessage.Beneficiary.AccountIdentifier) ? fedMessage.Beneficiary.AccountIdentifier : "";
                resultMessage.BeneficiaryName = !string.IsNullOrEmpty(fedMessage.Beneficiary.AccountName) ? fedMessage.Beneficiary.AccountName : "";
                resultMessage.AccountDebitInDrawdownIdentifier = !string.IsNullOrEmpty(fedMessage.AccountDebitedinDrawdown.AccountIdentifier) ? fedMessage.AccountDebitedinDrawdown.AccountIdentifier : "";
                resultMessage.AccountDebitInDrawdownName = !string.IsNullOrEmpty(fedMessage.AccountDebitedinDrawdown.AccountName) ? fedMessage.AccountDebitedinDrawdown.AccountName : "";
                resultMessage.OriginatorIdentifier = !string.IsNullOrEmpty(fedMessage.Originator.AccountIdentifier) ? fedMessage.Originator.AccountIdentifier : "";
                resultMessage.OriginatorName = !string.IsNullOrEmpty(fedMessage.Originator.AccountName) ? fedMessage.Originator.AccountName : "";
                resultMessage.OriginatorOptionFPartyIdentifier = !string.IsNullOrEmpty(fedMessage.OriginatorOptionF.PartyUniqueIdentifier) ? fedMessage.OriginatorOptionF.PartyUniqueIdentifier : "";
                resultMessage.OriginatorOptionFPartyName = !string.IsNullOrEmpty(fedMessage.OriginatorOptionF.PartyDetails.Name) ? fedMessage.OriginatorOptionF.PartyDetails.Name : "";
                resultMessage.OriginatorFIIdentifier = !string.IsNullOrEmpty(fedMessage.OriginatorFI.AccountIdentifier) ? fedMessage.OriginatorFI.AccountIdentifier : "";
                resultMessage.OriginatorFIName = !string.IsNullOrEmpty(fedMessage.OriginatorFI.AccountName) ? fedMessage.OriginatorFI.AccountName : "";
                resultMessage.InstructingFIIdentifier = !string.IsNullOrEmpty(fedMessage.InstructingFI.AccountIdentifier) ? fedMessage.InstructingFI.AccountIdentifier : ""; 
                resultMessage.InstructingFIName = !string.IsNullOrEmpty(fedMessage.InstructingFI.AccountName) ? fedMessage.InstructingFI.AccountName : ""; 
                resultMessage.DrawDownCreditAccountNumber = !string.IsNullOrEmpty(fedMessage.AccountCreditedinDrawdown) ? fedMessage.AccountCreditedinDrawdown : ""; 
                resultMessage.OriginatortoBeneficiaryInformation = fedMessage.OriginatortoBeneficiaryInformation !=null && !string.IsNullOrEmpty(fedMessage.OriginatortoBeneficiaryInformation[0]) ? fedMessage.OriginatortoBeneficiaryInformation[0] : ""; 
                try
                {
                    resultMessage.SwiftInstructedCurrency = !string.IsNullOrEmpty(fedMessage.SwiftB33BCurrencyInstructedAmount.InstructedCurrency) ? fedMessage.SwiftB33BCurrencyInstructedAmount.InstructedCurrency : "";
                    resultMessage.SwiftInstructedAmount = !string.IsNullOrEmpty(fedMessage.SwiftB33BCurrencyInstructedAmount.InstructedAmountFmt) ? fedMessage.SwiftB33BCurrencyInstructedAmount.InstructedAmountFmt : "";
                   
                    if (fedMessage.SwiftB52AOrderingInstitution !=null && fedMessage.SwiftB52AOrderingInstitution.SwiftDetails != null)
                    {
                        resultMessage.SwiftOrderingInstitution = !string.IsNullOrEmpty(fedMessage.SwiftB52AOrderingInstitution.SwiftDetails[0]) ? fedMessage.SwiftB52AOrderingInstitution.SwiftDetails[0] : "";
                    }

                    if (fedMessage.SwiftB50AOrderingCustomer != null && fedMessage.SwiftB50AOrderingCustomer.SwiftDetails != null)
                    {
                        resultMessage.SwiftOrderingCustomer = !string.IsNullOrEmpty(fedMessage.SwiftB50AOrderingCustomer.SwiftDetails[0]) ? fedMessage.SwiftB50AOrderingCustomer.SwiftDetails[0] : "";
                    }

                    if (fedMessage.SwiftB56AIntermediaryInstitution != null && fedMessage.SwiftB56AIntermediaryInstitution.SwiftDetails != null)
                    {
                        resultMessage.SwiftIntermiediaryInstitution = !string.IsNullOrEmpty(fedMessage.SwiftB56AIntermediaryInstitution.SwiftDetails[0]) ? fedMessage.SwiftB56AIntermediaryInstitution.SwiftDetails[0] : "";
                    }

                    if (fedMessage.SwiftB57AAccountwithInstitution != null && fedMessage.SwiftB57AAccountwithInstitution.SwiftDetails != null)
                    {
                        resultMessage.SwiftAccountWithInstitution = !string.IsNullOrEmpty(fedMessage.SwiftB57AAccountwithInstitution.SwiftDetails[0]) ? fedMessage.SwiftB57AAccountwithInstitution.SwiftDetails[0] : "";
                    }


                    if (fedMessage.SwiftB59ABeneficiaryCustomer != null && fedMessage.SwiftB59ABeneficiaryCustomer.SwiftDetails != null)
                    {
                        resultMessage.SwiftBeneficiaryCustomer = !string.IsNullOrEmpty(fedMessage.SwiftB59ABeneficiaryCustomer.SwiftDetails[0]) ? fedMessage.SwiftB59ABeneficiaryCustomer.SwiftDetails[0] : "";
                    }

                    if (fedMessage.SwiftB70RemittanceInformation != null && fedMessage.SwiftB70RemittanceInformation.SwiftDetails != null)
                    {
                        resultMessage.SwiftRemittanceInformation = !string.IsNullOrEmpty(fedMessage.SwiftB70RemittanceInformation.SwiftDetails[0]) ? fedMessage.SwiftB70RemittanceInformation.SwiftDetails[0] : "";
                    }

                    if (fedMessage.SwiftB72SendertoReceiverInformation != null && fedMessage.SwiftB72SendertoReceiverInformation.SwiftDetails != null)
                    {
                        resultMessage.SwiftSenderToReceiverInformation = !string.IsNullOrEmpty(fedMessage.SwiftB72SendertoReceiverInformation.SwiftDetails[0]) ? fedMessage.SwiftB72SendertoReceiverInformation.SwiftDetails[0] : "";
                    }
                    if (fedMessage.UnstructuredAddendaInformation != null && fedMessage.UnstructuredAddendaInformation.AddendaDetail != null)
                    {
                        resultMessage.AddendaInformation = !string.IsNullOrEmpty(fedMessage.UnstructuredAddendaInformation.AddendaDetail) ? fedMessage.UnstructuredAddendaInformation.AddendaDetail : "";
                    }


                    //  

                }
                catch (Exception ex )
                {
                    log.WriteToLog(ex.Message);
                    log.WriteToLog(ex.StackTrace);
                }
              
                resultMessage.previousMessageIdentifier = !string.IsNullOrEmpty(fedMessage.PreviousMessageId) ? fedMessage.PreviousMessageId : ""; ;
                resultMessage.NonProcessedInformation = !string.IsNullOrEmpty(fedMessage.Miscellaneous) ? fedMessage.Miscellaneous : ""; ;
                resultMessage.FileName = Path.Combine(fedMessage.MessageFilePath, fedMessage.OriginName);
                resultMessage.FileName = fedMessage.MessageFilePath;
                resultMessage.RecordNumber = fedMessage.MessageId;
              //  string record = PropertyExtensions.SetCsvRecord(resultMessage);

              //  outPutFileResult.WriteLine(record);
              //  outPutFileResult.Flush();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
           
            return 1;
        }
       
        public void WriteResults()
        {
            try
            {
                string record = PropertyExtensions.SetCsvRecord(resultMessage);

                outPutFileResult.WriteLine(record);
                outPutFileResult.Flush();
                outPutFileResult.WriteLine(record);
                outPutFileResult.Flush();
            }
            catch { }
           
        }

        private void initialize()
        {

          //  string dateyyyyMM = String.Format("yyyyMM", fileReceiveDate);
          string dateyyyyMM= fileReceiveDate.ToString("yyyyMM");
            string resultFileName = $"FedWireDataResults_{inOutFile}_{dateyyyyMM}.txt";
            csvHeader = loadCsvFileHeader();

           
            outputResultDataFile = Path.Combine(appSettings.CommonDetail.OutputReportDirectory, resultFileName);
            bool fileExist = !File.Exists(outputResultDataFile) ? false : true;

            if (fileExist)
            {
              while (isFileLocked(outputResultDataFile))
                {
                    log.WriteToLog("Fileis Being Locked by ANother Process Waiting");
                }
            }
            outPutFileResult = new StreamWriter(outputResultDataFile, true);

            try
            {
                if (!fileExist)
                {
                    outPutFileResult.WriteLine(csvHeader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                outPutFileResult.Flush();
            }
        }

        public void closeFile()
        {


            try
            {
                outPutFileResult.Flush();
                outPutFileResult.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        private string loadCsvFileHeader()
        {
            int count = 0;
            var fileHeaderName = appSettings.CommonDetail.OutputResultDataFileHeader;
            List<string> headerList = new List<string>();
            StringBuilder sbHeader = new StringBuilder();
            try
            {
                //StreamReader fedIncoming = new StreamReader(inputFile);
                using (var reader = new StreamReader(fileHeaderName))
                {
                    while (!reader.EndOfStream)
                    {

                        string header = reader.ReadLine();
                        if (!string.IsNullOrEmpty(header))
                        {
                            if (count > 0) sbHeader.Append("\t");
                            sbHeader.Append('"' + header + '"');
                            count++;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                log.WriteToLog(ex.StackTrace);
            }

            // log.WriteToLog(sbHeader.ToString());
            // Console.WriteLine(log.StrLogPath);
            return sbHeader.ToString();

        }

        // Return true if the file is locked for the indicated access.
        private bool isFileLocked(string filename, FileAccess file_access = FileAccess.ReadWrite)
        {
            // Try to open the file with the indicated access.
            
            try
            {
                FileStream fs =
                    new FileStream(filename, FileMode.Open, file_access);
                fs.Close();
                return false;
            }
            catch (IOException)
            {
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
-------------------------
