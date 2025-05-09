﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Xml.Linq;
using System.Configuration;

namespace ADO.NET
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//1) Берем строку подключения:
			const int PADDING = 30;
			//const string CONNECTION_STRING =
			//	"Data Source=(localdb)\\MSSQLLocalDB;" +
			//	"Initial Catalog=Movies;" +
			//	"Integrated Security=True;" +
			//	"Connect Timeout=30;" +
			//	"Encrypt=False;" +
			//	"TrustServerCertificate=False;" +
			//	"ApplicationIntent=ReadWrite;" +
			//	"MultiSubnetFailover=False";
			string CONNECTION_STRING = ConfigurationManager.ConnectionStrings["Movies"].ConnectionString;
			Console.WriteLine(CONNECTION_STRING);

			//2) Создаем подключение к серверу:
			SqlConnection connection = new SqlConnection(CONNECTION_STRING);
			//На данный момент подкл закрытое 

			//3) Создаем команлу ,которую нужно выполнить насервере:
			string cmd = "SELECT title, release_date, FORMATMESSAGE(N'%s %s', first_name, last_name) FROM Movies, Directors  WHERE director = director_id";
			SqlCommand command = new SqlCommand(cmd, connection);

			//4) Получаем результаты выполнения команды
			connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			

			//5) Обрабатываем результаты запроса
			if (reader.HasRows)
			{
				Console.WriteLine("=============================================================================");
				for (int i = 0; i < reader.FieldCount; i++)
					Console.Write(reader.GetName(i).PadRight(15));
				Console.WriteLine();
				Console.WriteLine("=============================================================================");
				while (reader.Read())
				{
					//Console.WriteLine($"{reader[0].ToString().PadRight(5)}{reader[2].ToString().PadRight(15)}{reader[1].ToString().PadRight(15)}");
					for (int i =  0; i < reader.FieldCount; i++)
					{
						Console.Write(reader[i].ToString().PadRight(PADDING));
					}
					Console.WriteLine();
				}
			}

			//6) Закрываем SqlDataReader and  connection
			reader.Close();
			connection.Close();

			Console.Read();
		}
	}
}
