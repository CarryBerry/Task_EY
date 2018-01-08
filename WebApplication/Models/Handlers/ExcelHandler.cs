using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Models.Test
{
    public class ExcelHandler
    {
        private int id = 0;
        private int accountGroupSumId = 1;
        private int accountClassId = 1;
        private bool flag = false;

        private string GetConnectionString(FileInfo _file)
        {
            Dictionary<string, string> properties = new Dictionary<string, string>();
            
            // XLSX - Excel 2007, 2010, 2012, 2013
            if (_file.Extension == ".xlsx")
            {
                properties["Provider"] = "Microsoft.ACE.OLEDB.12.0;";
                properties["Extended Properties"] = "Excel 12.0 XML";
                properties["Data Source"] = _file.FullName;
            }
            // XLS - Excel 2003 and older
            else if (_file.Extension == ".xls")
            {
                properties["Provider"] = "Microsoft.Jet.OLEDB.4.0";
                properties["Extended Properties"] = "Excel 8.0";
                properties["Data Source"] = _file.FullName;
            }
            else
            {
                throw new Exception("The file you are trying to open, is in a different format. Only '.xls' or '.xlsx' formats are allowed");
            }

            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, string> property in properties)
            {
                sb.Append(property.Key);
                sb.Append('=');
                sb.Append(property.Value);
                sb.Append(';');
            }

            return sb.ToString();
        }

        private DataSet File_Selected_New(FileInfo _file)
        {
            DataSet dataSet = new DataSet();
            string connectionString = GetConnectionString(_file);

            using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(connectionString))
            {
                connection.Open();
                System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand();
                command.Connection = connection;

                // Get all Sheets in Excel File
                DataTable dataTableSheet = connection.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, null);
                
                // Loop through all Sheets to get data
                foreach (DataRow dataRow in dataTableSheet.Rows)
                {
                    string sheetName = dataRow["TABLE_NAME"].ToString();

                    if (!sheetName.EndsWith("$'"))
                        continue;

                    // Get all rows from the Sheet
                    command.CommandText = "SELECT * FROM [" + sheetName + "]";

                    command.Connection = connection;

                    DataTable dataTable = new DataTable();
                    dataTable.TableName = sheetName;

                    System.Data.OleDb.OleDbDataAdapter dataAdapter = new System.Data.OleDb.OleDbDataAdapter(command);
                    dataAdapter.Fill(dataTable);

                    dataSet.Tables.Add(dataTable);
                }
                connection.Close();

                return dataSet;
            }
        }

        public void SelectDataFromFile(FileInfo _file, int fileId)
        {
            DataSet dataSet = File_Selected_New(_file);

            List<string> list_row = new List<string>();

            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                string aa = null;
                for (int j = 0; j <= 6; j++)
                {
                    aa += dataSet.Tables[0].Rows[i].ItemArray[j].ToString() + "_";
                    if (j == 6 && (aa.Contains("КЛАСС ") || flag == true))
                    {
                            flag = true;

                            if (aa.Contains("КЛАСС "))
                            {
                                continue;
                            }
                            else
                            {
                                var currentRow = aa.Split('_').ToList();
                                
                                if (currentRow[0].Count() == 4)
                                {
                                AddIncomingBalanceToDatabase(currentRow);
                                AddCurrentAssetToDatabase(currentRow);
                                AddOutgoingBalanceToDatabase(currentRow);
                                AddFieldToDatabase(currentRow, fileId);
                            }
                                else if (currentRow[0].Count() == 2)
                                {
                                    AddUnitGroupSumToDatabase(currentRow);
                                }
                                else if (currentRow[0].Contains("ПО КЛАССУ"))
                                {
                                    AddAccountClassSumToDatabase(currentRow);
                                }
                                else if (currentRow[0].Contains("БАЛАНС"))
                                {
                                    flag = false;
                                }
                            }
                        aa = null;
                    }
                }
            }
        }

        private void AddUnitGroupSumToDatabase(List<string> row)
        {
            accountGroupSumId++;
            
            string conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(conn);
            string query = "Insert into AccountGroupSums(IncomingBalanceAccountGroupAsset,IncomingBalanceAccountGroupLiability,CurrentAssetsAccountGroupAsset,CurrentAssetsAccountGroupLiability,OutgoingBalanceAccountGroupAsset,OutgoingBalanceAccountGroupLiability) Values('" + row[1] + "','" + row[2] + "','" + row[3] + "','" + row[4] + "','" + row[5] + "','" + row[6] + "')";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void AddCurrentAssetToDatabase(List<string> row)
        {
            string conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(conn);
            string query = "Insert into CurrentAssets(AccountGroup,AccountUnit,Asset,Liability) Values('" + Convert.ToInt32(row[0]) + "','" + Convert.ToInt32((row[0].First().ToString() + row[0].ElementAt(1).ToString())) + "','" + row[3] + "','" + row[4] + "')";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void AddIncomingBalanceToDatabase(List<string> row)
        {
            id++;
            string conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(conn);
            string query = "Insert into IncomingBalances(AccountUnit,AccountGroup,Asset,Liability) Values('" + Convert.ToInt32(row[0]) + "','" + Convert.ToInt32((row[0].First().ToString() + row[0].ElementAt(1).ToString())) + "','" + row[1] + "','" + row[2] + "')";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void AddOutgoingBalanceToDatabase(List<string> row)
        {
            string conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(conn);
            string query = "Insert into OutgoingBalances(AccountUnit,AccountGroup,Asset,Liability) Values('" + Convert.ToInt32(row[0]) + "','" + Convert.ToInt32((row[0].First().ToString() + row[0].ElementAt(1).ToString())) + "','" + row[5] + "','" + row[6] + "')";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void AddFieldToDatabase(List<string> row, int fileId)
        {
            string conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(conn);
            string query = "Insert into AccountFields(IncomingBalanceFieldAsset,IncomingBalanceFieldLiability,CurrentAssetsFieldAsset,CurrentAssetsFieldLiability,OutgoingBalanceFieldAsset,OutgoingBalanceFieldLiability,AccountGroupSumId,UploadedFileInfoId,IncomingBalanceId,CurrentAssetsId,OutgoingBalanceId,AccountClassId,FieldAccountUnit) Values('" + row[1] + "','" + row[2] + "','" + row[3] + "','" + row[4] + "','" + row[5] + "','" + row[6] + "','" + accountGroupSumId + "','" + fileId + "','" + id + "','" + id + "','" + id + "','" + accountClassId + "','" + row[0] + "')";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void AddAccountClassSumToDatabase(List<string> row)
        {
            accountClassId++;
            
            string conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(conn);
            string query = "Insert into AccountClasses(IncomingBalanceClassAsset,IncomingBalanceClassLiability,CurrentAssetsClassAsset,CurrentAssetsClassLiability,OutgoingBalanceClassAsset,OutgoingBalanceClassLiability) Values('" + row[1] + "','" + row[2] + "','" + row[3] + "','" + row[4] + "','" + row[5] + "','" + row[6] + "')";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
