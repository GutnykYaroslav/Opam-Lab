using System;
using System.Collections.Generic;

namespace opam_lab2
{
    class Program
    {
        struct Product
        {
            public int Id;
            public string Name;
            public double Price;
            public int Quantity;
        }

        struct Client
        {
            public int Id;
            public string Name;
            public string Phone;
        }

        static List<Product> products = new List<Product>();
        static List<Client> clients = new List<Client>();
        static string login = "admin";
        static string password = "12345";

        public static void Main(string[] args)
        {
            // Система входу
            int attempts = 3;
            do
            {
                Console.Clear();
                Console.WriteLine("=== СИСТЕМА ВХОДУ ===");
                Console.Write("Логін: ");
                string l = Console.ReadLine();
                Console.Write("Пароль: ");
                string p = Console.ReadLine();

                if (l == login && p == password) break;

                attempts--;
                Console.WriteLine($"Невірний логін або пароль. Залишилось спроб: {attempts}");
                if (attempts == 0) return;
                Console.ReadKey();
            } while (true);

            // Ініціалізація даних
            string[] productNames = { "Нурофен", "Йод", "Едем", "Аспірин", "Вітамін С" };
            double[] prices = { 120, 40, 150, 60, 200 };
            int[] quantities = { 50, 100, 30, 80, 40 };

            for (int i = 0; i < 5; i++)
            {
                products.Add(new Product
                {
                    Id = i + 1,
                    Name = productNames[i],
                    Price = prices[i],
                    Quantity = quantities[i]
                });
                clients.Add(new Client
                {
                    Id = i + 1,
                    Name = $"Клієнт {i + 1}",
                    Phone = "+38067123456"
                });
            }

            RenderIntro();
            ShowMainMenu();
        }

        public static void RenderIntro()
        {
            Console.Clear();
            Console.WriteLine("===========================================");
            Console.WriteLine("==== Ласкаво просимо до Аптеки Здоров'я ====");
            Console.WriteLine("===========================================");
        }

        public static double GetUserInput(string prompt = "Введіть число:")
        {
            Console.Write(prompt + " ");
            bool isNumber = Double.TryParse(Console.ReadLine(), out double choice);
            if (!isNumber)
            {
                Console.WriteLine("Ви ввели не число!");
                return GetUserInput(prompt);
            }
            return choice;
        }

        public static void ShowMainMenu()
        {
            while (true)
            {
                Console.WriteLine("\nГоловне меню:");
                Console.WriteLine("1. Товари");
                Console.WriteLine("2. Клієнти");
                Console.WriteLine("3. Замовлення");
                Console.WriteLine("4. Пошук");
                Console.WriteLine("5. Статистика");
                Console.WriteLine("6. Вихід");

                double choice = GetUserInput("Виберіть пункт меню:");
                switch (choice)
                {
                    case 1: ShowProductMenu(); break;
                    case 2: ShowClientsMenu(); break;
                    case 3: ShowOrderMenu(); break;
                    case 4: ShowSearchMenu(); break;
                    case 5: ShowStatistics(); break;
                    case 6: Environment.Exit(0); break;
                    default: Console.WriteLine("Неправильний вибір."); break;
                }
            }
        }

        private static void ShowOrderMenu()
        {
            Console.Clear();
            Console.WriteLine("=== ЗАМОВЛЕННЯ ===");

            Console.WriteLine("\nТовари:");
            for (int i = 0; i < products.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {products[i].Name} - {products[i].Price} грн");
            }

            double priceNurofen = 120;
            double priceIodine = 40;
            double priceEdem = 150;

            double Nurofen = GetUserInput("Кількість Нурофену:");
            double Iodine = GetUserInput("Кількість Йоду:");
            double Edem = GetUserInput("Кількість Едему:");

            double totalPrice = Nurofen * priceNurofen + Iodine * priceIodine + Edem * priceEdem;
            double discount = totalPrice > 1000 ? 15 : 5;
            double discountTotal = totalPrice * discount / 100;

            Console.WriteLine($"\nЗагальна вартість: {totalPrice} грн");
            Console.WriteLine($"Знижка: {discount}%");
            Console.WriteLine($"До сплати: {totalPrice - discountTotal} грн");
            Console.WriteLine("Дякуємо за покупку!");
            Console.ReadKey();
        }

        private static void ShowClientsMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== КЛІЄНТИ ===");
                Console.WriteLine("1. Перегляд клієнтів");
                Console.WriteLine("2. Додати клієнта");
                Console.WriteLine("3. Назад");

                double choice = GetUserInput("Виберіть дію:");
                switch (choice)
                {
                    case 1: DisplayClients(); break;
                    case 2: AddClient(); break;
                    case 3: return;
                    default: Console.WriteLine("Невірний вибір!"); break;
                }
            }
        }

        private static void DisplayClients()
        {
            Console.WriteLine("\nСписок клієнтів:");
            for (int i = 0; i < clients.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {clients[i].Name} - {clients[i].Phone}");
            }
            Console.ReadKey();
        }

        private static void AddClient()
        {
            Console.Write("Ім'я: ");
            string name = Console.ReadLine();
            Console.Write("Телефон: ");
            string phone = Console.ReadLine();

            clients.Add(new Client
            {
                Id = clients.Count + 1,
                Name = name,
                Phone = phone
            });
            Console.WriteLine("Клієнта додано!");
            Console.ReadKey();
        }

        private static void ShowProductMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== ТОВАРИ ===");
                Console.WriteLine("1. Перегляд товарів");
                Console.WriteLine("2. Додати товар");
                Console.WriteLine("3. Пошук товару");
                Console.WriteLine("4. Назад");

                double choice = GetUserInput("Виберіть дію:");
                switch (choice)
                {
                    case 1: DisplayProducts(); break;
                    case 2: AddProduct(); break;
                    case 3: SearchProductByName(); break;
                    case 4: return;
                    default: Console.WriteLine("Невірний вибір!"); break;
                }
            }
        }

        private static void DisplayProducts()
        {
            Console.WriteLine("\nСписок товарів:");
            for (int i = 0; i < products.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {products[i].Name} - {products[i].Price} грн ({products[i].Quantity} шт)");
            }
            Console.ReadKey();
        }

        private static void AddProduct()
        {
            Console.Write("Назва: ");
            string name = Console.ReadLine();
            double price = GetUserInput("Ціна:");
            int quantity = (int)GetUserInput("Кількість:");

            products.Add(new Product
            {
                Id = products.Count + 1,
                Name = name,
                Price = price,
                Quantity = quantity
            });
            Console.WriteLine("Товар додано!");
            Console.ReadKey();
        }

        private static void SearchProductByName()
        {
            Console.Write("Назва для пошуку: ");
            string search = Console.ReadLine().ToLower();

            bool found = false;
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].Name.ToLower().Contains(search))
                {
                    if (!found) Console.WriteLine("\nРезультати пошуку:");
                    Console.WriteLine($"{products[i].Name} - {products[i].Price} грн");
                    found = true;
                }
            }
            if (!found) Console.WriteLine("Не знайдено");
            Console.ReadKey();
        }

        private static void ShowSearchMenu()
        {
            Console.Clear();
            Console.WriteLine("=== ПОШУК ===");
            double minPrice = GetUserInput("Мінімальна ціна:");

            bool found = false;
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].Price >= minPrice)
                {
                    if (!found) Console.WriteLine("\nТовари:");
                    Console.WriteLine($"{products[i].Name} - {products[i].Price} грн");
                    found = true;
                }
            }
            if (!found) Console.WriteLine("Не знайдено");
            Console.ReadKey();
        }

        private static void ShowStatistics()
        {
            Console.Clear();
            Console.WriteLine("=== СТАТИСТИКА ===");

            if (products.Count == 0)
            {
                Console.WriteLine("Немає товарів");
                return;
            }

            double totalValue = 0;
            double maxPrice = 0;
            double minPrice = double.MaxValue;
            int totalQuantity = 0;
            int expensiveCount = 0;

            for (int i = 0; i < products.Count; i++)
            {
                totalValue += products[i].Price * products[i].Quantity;
                totalQuantity += products[i].Quantity;
                if (products[i].Price > maxPrice) maxPrice = products[i].Price;
                if (products[i].Price < minPrice) minPrice = products[i].Price;
                if (products[i].Price > 100) expensiveCount++;
            }

            Console.WriteLine($"Загальна вартість: {totalValue:F2} грн");
            Console.WriteLine($"Середня ціна: {totalValue / totalQuantity:F2} грн");
            Console.WriteLine($"Макс. ціна: {maxPrice} грн");
            Console.WriteLine($"Мін. ціна: {minPrice} грн");
            Console.WriteLine($"Товарів >100 грн: {expensiveCount} шт");
            Console.WriteLine($"Всього товарів: {products.Count} шт");
            Console.ReadKey();
        }
    }
}