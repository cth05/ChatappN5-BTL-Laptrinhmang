using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_app
{
    internal class InitDb
    {
        public static void CreateTable()
        {
            if (!File.Exists("chat.db"))
                SQLiteConnection.CreateFile("chat.db");

            using (var conn = new SQLiteConnection("Data Source=chat.db"))
            {
                conn.Open();

                string sql = @"
    CREATE TABLE IF NOT EXISTS messages (
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        fromId INTEGER,
        toId INTEGER,
        type TEXT,
        message TEXT,
        timestamp INTEGER
    );";

                new SQLiteCommand(sql, conn).ExecuteNonQuery();
            }
        }
    }
}
