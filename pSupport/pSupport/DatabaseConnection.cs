using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data; 

namespace pSupport
{
    class DatabaseConnection
    {
        private string sql_string;
        private string strCon;
        System.Data.SqlClient.SqlDataAdapter da_1; 

        public string Sql
        {
            set { sql_string = value;  }
        }

        public string connection_String
        {
            set { strCon = value;  }
        }

        public DataSet GetConnection
        {
            get { return MyDataSet(); }

        }

        private DataSet MyDataSet()
        {
            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(strCon);
            con.Open();
            da_1 = new System.Data.SqlClient.SqlDataAdapter(sql_string, con);

            DataSet dat_set = new DataSet();
            da_1.Fill(dat_set, "Table_Data_1");
            con.Close();

            return dat_set; 

        }


        public void UpdateDatabse(DataSet ds)
        {
            System.Data.SqlClient.SqlCommandBuilder cb = new System.Data.SqlClient.SqlCommandBuilder(da_1);
            cb.DataAdapter.Update(ds.Tables[0]); 

        }





    } // End Class












} //End Namespace 
