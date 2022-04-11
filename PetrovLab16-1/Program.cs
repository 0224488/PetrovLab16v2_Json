using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;


namespace PetrovLab16_1
{
	class Program
	{
		// Необходимо разработать программу для записи информации о товаре в текстовый файл в формате json.
		// Разработать класс для моделирования объекта «Товар». 
		// Предусмотреть члены класса «Код товара» (целое число), «Название товара» (строка), «Цена товара» (вещественное число).
		// Создать массив из 5-ти товаров, значения должны вводиться пользователем с клавиатуры.
		// Сериализовать массив в json-строку, сохранить ее программно в файл «Products.json».

        static void Main(string[] args)
		{
			var products = new Product[5]; // Количество товаров
			for (int i = 0; i < products.Length; i++)
			{
				products[i] = new Product();
			}

			// Вводим значения. Проверку вводимых значений не делаю.
			for (int i = 0; i < products.Length; i++)
			{
				Console.Write($"Введите код товара {i + 1}: ");
				products[i].Code = int.Parse(Console.ReadLine());

				Console.Write($"Введите название товара {i + 1}: ");
				products[i].Name = Console.ReadLine();

				Console.Write($"Введите цену товара {i + 1}: ");
				products[i].Price = double.Parse(Console.ReadLine());
			}

			// Сериализация в строку, используя System.Text.Json
			var options = new JsonSerializerOptions
			{
				Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
				WriteIndented = true
			};
			var json = System.Text.Json.JsonSerializer.Serialize(products, options);

            try
            {
				Console.WriteLine("Введите путь сохранения файла");
				var path = Console.ReadLine() + "/Products.json";
				using (var file = new StreamWriter(path))
				{
					file.WriteLine(json);
					Console.WriteLine("Файл успешно сохранен");
				}

			}
			catch
            {
				Console.WriteLine("Ошибка доступа или неверный путь. Файл не сохранен");
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
