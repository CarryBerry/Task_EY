using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace Task_EY.EntityModel
{
    public class DbHandler 
    {
        private int i = 0;
        ProgressBar progress = new ProgressBar();
        private object _locker = new object();

        public void DbAdd(string path, int lineCount = 100000)
        {
            lock (_locker)
            {
                string[] records;

                using (DataContext context = new DataContext())
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        lineCount = File.ReadLines(path).Count(); //Needs if we don't know exactly 
                                                                  //how many rows this file contains
                                                                  //else comment this field
                        while (!sr.EndOfStream)
                        {
                            records = sr.ReadLine().Split(' ');
                           
                            i++;

                            string conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                            SqlConnection con = new SqlConnection(conn);
                            string query = "Insert into Contents(DateLine,LatinLine,CyrillicLine,IntLine,DoubleLine) Values('" + Convert.ToDateTime(records[0]) + "','" + records[1] + "','" + records[2] + "','" + Convert.ToInt32(records[3]) + "','" + records[4] + "')";
                            con.Open();
                            SqlCommand cmd = new SqlCommand(query, con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            
                            progress.UpdateMessage(i, lineCount);
                        }
                    }
                }
                progress.CompleteMessage();
            }
        }

        public void DbAddCreated(int amount)
        {
            for (var i = 1; i <= amount; i++)
            {
                DbAdd("file" + i.ToString() + ".txt");
            }
        }
    }
}
