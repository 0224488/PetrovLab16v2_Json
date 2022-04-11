using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace PetrovLab16_2
{
	// Необходимо разработать программу для получения информации о товаре из json-файла.
	// Десериализовать файл «Products.json» из задачи 1. Определить название самого дорогого товара.

	class Program
	{
		static void Main(string[] args)
		{
            try // Для проверки правильности пути и наличия файла ограничился исключением...
            {
				// Чтение строки из файла
				Console.WriteLine("Введите путь к сохраненному файлу Products.json");

				var path = Console.ReadLine() + "/Products.json";

				using (StreamReader reader = new StreamReader(path))
				{
					string text = reader.ReadToEnd();

					// Десериализация с помощью библиотеки System.Text.Json
					var options = new JsonSerializerOptions
					{
						Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
						WriteIndented = true
					};
					var products = System.Text.Json.JsonSerializer.Deserialize(text, typeof(Product[]), options) as Product[];

					string name = products[0].Name;
					double maxPrice = products[0].Price;
					for (int i = 1; i < products.Length; i++)
					{
						if (products[i].Price > maxPrice)
						{
							maxPrice = products[i].Price;
							name = products[i].Name;
						}
					}
					Console.WriteLine($"Самый дорогой товар: {name}");
				}

			}
			catch
            {
				Console.WriteLine("Ошибка доступа или отсутствует файл по указанному пути");
			}
			Console.ReadKey();
		}

		public class Product
		{
			// Код товара
			public int Code { get; set; }

			// Название товара
			public string Name { get; set; }

			// Цена товара
			public double Price { get; set; }
		}
	}
}
