using QuanLyThuVien.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;
        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO();return instance; }
            private set { instance = value; }
        }
        private AccountDAO() { }
        public bool Login(string userName, string passWord)
        {
            string query = "USP_Login @userName , @passWord";
            DataTable result = DataProvider.Instance.ExcuteQuery(query, new object[] { userName, passWord});
            return result.Rows.Count > 0;
        }
        public bool InsertAccount(string name, string displayname, int type)
        {
            string query = string.Format("Insert Account ( UserName ,DisplayName, Type) values (N'{0}', {1}, {2}) ", name, displayname, type);
            int result = DataProvider.Instance.ExcuteNonQuery(query);
            return result > 0;
        }


        public bool UpdateAccount(int id, string name, string displayName, int type)
        {
            string query = string.Format("Update Account SET UserName = N'{0}', DisplayName={1}, Type = {2}, WHERE ID = {3}", name, displayName, type, id);
            int result = DataProvider.Instance.ExcuteNonQuery(query);
            return result > 0;        
        }
        public bool DeleteAccount(int id)
        {
            string query = string.Format("Delete Account where ID = {0}", id);
            int result = DataProvider.Instance.ExcuteNonQuery(query);
            return result > 0;
        }
        public Account GetAccountByUserName(string userName)
        {
            DataTable data =  DataProvider.Instance.ExcuteQuery("Select * from account where UserName = '" + userName + "'");
            foreach(DataRow item in data.Rows)
            {
                return new Account(item);
            }
            return null;
        }
    }
}
