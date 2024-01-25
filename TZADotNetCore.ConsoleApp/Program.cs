using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks.Dataflow;
using TZADotNetCore.ConsoleApp.AdoDotNetExamples;
using TZADotNetCore.ConsoleApp.DapperExamples;

Console.WriteLine("Hello, World!");

//Ctrl + .  
//Ctrl + D              => duplicate
//Alt + Up key,Down key => move
//F10                   => summary
//F11                   => detail
//Ctrl + m , h          => create region
//Ctrl + k ,d           => Format 

/*SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
{
    DataSource = ".",
    InitialCatalog = "TZADotNetCore",
    UserID = "sa",
    Password = "sa@123",
};

SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
connection.Open();
Console.WriteLine("Connection Open");

String query = @"SELECT Blog_Id
      ,Blog_Title
      ,[Blog_Author]
      ,Blog_Content
  FROM Tbl_Blog";
SqlCommand command = new SqlCommand(query, connection);
SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
DataTable dataTable = new DataTable();
sqlDataAdapter.Fill(dataTable);

connection.Close();
Console.WriteLine("Connection Close");

foreach (DataRow row in dataTable.Rows) 
{ 
    Console.WriteLine($"Id      => {row["Blog_Id"]}");
    Console.WriteLine($"Title   => {row["Blog_Title"]}");
    Console.WriteLine($"Author  => {row["Blog_Author"]}");
    Console.WriteLine($"Content => {row["Blog_Content"]}");
    Console.WriteLine("--------------------------------------");

}*/

/*AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
adoDotNetExample.Run();*/

DapperExample dapperExample = new DapperExample();
dapperExample.Run();

Console.ReadKey();