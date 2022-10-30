using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;


public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}
public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=FirstStep;Username=postgres;Password=12345678");
    }
}

namespace HelloApp
{
    namespace HelloApp
    {
        class Program
        {
            static void Main(string[] args)
            {
                bool exitProgram = true;
                while (exitProgram)
                {
                    try
                    {
                        ShowMenu();

                        int? option = Convert.ToInt32(Console.ReadLine());

                        switch (option)
                        {
                            case 1:
                                CreateUser();

                                Console.WriteLine();
                                break;

                            case 2:
                                DeleteUser();
                                break;

                            case 3:
                                ChangeUser();
                                break;

                            case 4:
                                ShowUsers();
                                break;
                                                            
                            case 5:
                                exitProgram = false;
                                break;

                            default:
                                Console.WriteLine("Выберете существующий варинат\n");
                                break;
                        }
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine($"Произошла ошибка: {ex.Message}");
                    }
                }
            }

            static void ShowMenu()
            {
                Console.WriteLine("1) Добавить");
                Console.WriteLine("2) Удалить");
                Console.WriteLine("3) Изменить");
                Console.WriteLine("4) Показать");
                Console.WriteLine("5) Выйти из программы\n");
            }

            static void ShowUsers()
            {
                Console.Clear();

                using ApplicationContext db = new ApplicationContext();

                var users = db.Users.ToList();

                Console.WriteLine("Users list:");

                foreach (User u in users)
                {
                    Console.WriteLine($"Id: {u.Id}. Имя: {u.Name}. Возраст: {u.Age}");
                }
                Console.WriteLine();
            }

            static void CreateUser()
            {
                Console.Clear();
                Console.Write("Введите имя: ");

                string name = Console.ReadLine();

                Console.Write("Ведите возраст: ");

                int age = Convert.ToInt32(Console.ReadLine());

                using ApplicationContext db = new ApplicationContext();
                User user = new User();
                user.Name = name;
                user.Age = age;

                db.Users.Add(user);
                db.SaveChanges();
                Console.WriteLine();
            }


            static void DeleteUser()
            {
                Console.Clear();
                Console.Write("Выберете id: ");
                int id = Convert.ToInt32(Console.ReadLine());

                using ApplicationContext db = new ApplicationContext();

                User? user = db.Users.FirstOrDefault(p => p.Id == id);

                if (user != null)
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                }
                else
                {
                    Console.WriteLine($"Нет данных под Id: {id}\n");
                }
            }

            static void ChangeUser()
            {
                Console.Clear();
                Console.Write("Выберете id: ");
                int id = Convert.ToInt32(Console.ReadLine());

                using ApplicationContext db = new ApplicationContext();
                User? user = db.Users.FirstOrDefault(p => p.Id == id);


                if (user != null)
                {
                    try
                    {
                        Console.Write("Изменение имени на: ");
                        string newName = Console.ReadLine();

                        Console.Write("Изменение возраста на: ");

                        int newAge = Convert.ToInt32(Console.ReadLine());

                        user.Name = newName;
                        user.Age = newAge;
                        //обновляем объект
                        //db.Users.Update(user);
                        db.SaveChanges();

                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Не верный формат данных!\n");
                    }
                }
                else
                {
                        Console.WriteLine($"Нет данных по Id: {id}\n");
                }
            }
        }
    }
}