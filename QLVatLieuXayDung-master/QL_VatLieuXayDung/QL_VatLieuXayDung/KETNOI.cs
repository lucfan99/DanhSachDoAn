using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_VatLieuXayDung
{
    class KETNOI
    {
        private string str;
        private string ServerName, strUser, strPass, dataBase;

        public string DataBase
        {
            get { return dataBase; }
            set { dataBase = value; }
        }

        public string StrPass
        {
            get { return strPass; }
            set { strPass = value; }
        }

        public string StrUser
        {
            get { return strUser; }
            set { strUser = value; }
        }

        public string ServerName1
        {
            get { return ServerName; }
            set { ServerName = value; }
        }
        private DataSet dset;

        public DataSet Dset
        {
            get { return dset; }
            set { dset = value; }
        }
        public string Str
        {
            get { return str; }
            set { str = value; }
        }
        private SqlConnection Con;

        public SqlConnection Con1
        {
            get { return Con; }
            set { Con = value; }
        }
        public KETNOI(string Server, string User, string Pass)
        {
            Server = frmKetNoiHeThong.Luu.server;
            str = @"Data Source=" + Server + ";Initial Catalog=QL_VATLIEUXAYDUNG;User ID=" + User + ";Password=" + Pass + "";
            Con = new SqlConnection(str);
            dset = new DataSet();

        }
        //public KETNOI()
        //{
        //    string user = "sa";
        //    string pass = "sql2012";
        //    string server = @"DESKTOP-HEBRIUH\SQLEXPRESS";
        //    str = @"Data Source=" + server + ";Initial Catalog =QL_VATLIEUXAYDUNG;User ID=" + user + ";Password=" + pass + "";
        //    Con = new SqlConnection(str);
        //    dset = new DataSet();
        //}
        public KETNOI()
        {
            ServerName1 = frmKetNoiHeThong.Luu.server;
            StrUser = frmKetNoiHeThong.Luu.user;
            StrPass = frmKetNoiHeThong.Luu.pass;
            DataBase = "QL_VATLIEUXAYDUNG";
            str = @"Data Source=" + ServerName1 + ";Initial Catalog ="+DataBase+";User ID=" + StrUser + ";Password=" + StrPass + "";
            Con = new SqlConnection(str);
            dset = new DataSet();
        }
        //public DBConnect(string st)
        //{
        //    str = st;
        //    Con = new SqlConnection(str);
        //    dset = new DataSet("QL_SinhVien");
        //}
        public void OpenConnection()
        {
            if (Con.State == ConnectionState.Closed)
                Con.Open();

        }
        public void ClosedConnection()
        {
            if (Con.State == ConnectionState.Open)
                Con.Close();
        }
        public void disposeConnection()
        {
            if (Con.State == ConnectionState.Open)
                Con.Close();
            Con.Dispose();
        }
        public void updateToDB(string strSQL)
        {
            OpenConnection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = strSQL;
            cmd.Connection = Con;
            cmd.ExecuteNonQuery();

            ClosedConnection();
        }
        public SqlDataReader getReader(string strSQL)
        {
            OpenConnection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = strSQL;
            cmd.Connection = Con;
            return cmd.ExecuteReader();

        }
        public int getCount(string strSQL)
        {
            OpenConnection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = strSQL;
            cmd.Connection = Con;
            int count = (int)cmd.ExecuteScalar();

            ClosedConnection();
            return count;
        }
        public bool checkForExistence(string strSQL)
        {
            int count = getCount(strSQL);
            if (count > 0)
                return true;
            return false;
        }
        public DataTable getDataTable(string strSQL, string tableName)
        {
            OpenConnection();

            SqlDataAdapter ada = new SqlDataAdapter(strSQL, Con);
            ada.Fill(Dset, tableName);

            ClosedConnection();
            return Dset.Tables[tableName];

        }
        public DataSet GrdSource(string select)
        {
            OpenConnection();
            SqlDataAdapter da = new SqlDataAdapter(select, Con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            ClosedConnection();
            return ds;
        }
        public SqlDataAdapter getDataAdapter(string strSQL, string tableName)
        {
            OpenConnection();

            SqlDataAdapter ada = new SqlDataAdapter(strSQL, Con);
            ada.Fill(Dset, tableName);

            ClosedConnection();
            return ada;

        }
        public bool checkKey(string sql)
        {
            SqlDataAdapter MyData = new SqlDataAdapter(sql, Con);
            DataTable table = new DataTable();
            MyData.Fill(table);
            if (table.Rows.Count > 0)
                return true;
            else return false;
        }
        

    }
}
