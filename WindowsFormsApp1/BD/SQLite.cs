using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.BD
{
    internal class SQLite
    {
        public static SQLiteConnection BD;

        static SQLite()
        {
            SQLite.BD = new SQLiteConnection("Data Source=C:/Users/Dan/source/repos/WindowsFormsApp1/WindowsFormsApp1/BD; Version=3");
            BD.Open();
        }

    }

}
