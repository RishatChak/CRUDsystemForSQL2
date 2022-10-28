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

                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("1) Добавить");
                    Console.WriteLine("2) Удалить");
                    Console.WriteLine("3) Изменить");
                    Console.WriteLine("4) Показать");
                    Console.WriteLine("5) Почистить консоль\n");

                    try
                    {
                        int? option = Convert.ToInt32(Console.ReadLine());

                        switch (option)
                        {

                            case 1:
                                try
                                {
                                    Console.Write("Введите имя ");

                                    string name = Console.ReadLine();

                                    Console.Write("Ведите возраст ");

                                    int age = Convert.ToInt32(Console.ReadLine());

                                    CreateUser(age, name);

                                    Console.WriteLine();
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Не верный формат\n");
                                }
                                break;
                            case 2:
                                Console.Write("Выберете id");
                                int id = Convert.ToInt32(Console.ReadLine());

                                DelateUser(id);
                                break;
                            case 3:
                                Console.Write("Выберете id ");
                                int id2 = Convert.ToInt32(Console.ReadLine());

                                ChangeUser(id2);
                                break;
                            case 4:
                                ShowUser();
                                break;
                            case 5:
                                Console.Clear();
                                break;
                            default:
                                Console.WriteLine("Выберете существующий варинат\n");
                                break;
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Выберете существующий варинат\n");
                    }
                }
            }



            static void ShowUser()
            {
                using ApplicationContext db = new ApplicationContext();

                var users = db.Users.ToList();

                Console.WriteLine("Users list:");

                foreach (User u in users)
                {
                    Console.WriteLine($"Id: {u.Id}. Имя: {u.Name}. Возраст: {u.Age}");
                }
                Console.WriteLine();
            }

            static void CreateUser(int age, string name)
            {
                using ApplicationContext db = new ApplicationContext();
                User user = new User();
                user.Name = name;
                user.Age = age;

                db.Users.Add(user);
                db.SaveChanges();
                Console.WriteLine();

            }


            static void DelateUser(int id)
            {
                using ApplicationContext db = new ApplicationContext();

                User user = db.Users.Find(id);

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

            static void ChangeUser(int id)
            {
                using ApplicationContext db = new ApplicationContext();
                User user1 = db.Users.Find(id);

                if (user1 != null)
                {
                    try
                    {
                        Console.Write("Изменение имени на: ");
                        string newName = Console.ReadLine();

                        Console.Write("Изменение возраста на: ");

                        int newAge = Convert.ToInt32(Console.ReadLine());

                        user1.Name = newName;
                        user1.Age = newAge;
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