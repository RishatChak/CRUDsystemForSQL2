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
                IDataProcessor dataProcessor = new ConsoleProccesor();
                dataProcessor.ShowMenuCradAndStartProgram(new ShowMenuCRUD());
            }
            
            interface IDatatProvider
            {
                void CRUDSystem();
            }

            interface IDataProcessor
            {
                void ShowMenuCradAndStartProgram(IDatatProvider forCRUD);
            }

            class ShowMenuCRUD : IDataProcessor, IDatatProvider
            {
                IDataProcessor dataProcessor = new ConsoleProccesor();

                bool exitProgram = true;

                public void CRUDSystem()
                {
                    while (exitProgram)
                    {
                        try
                        {
                            Console.WriteLine("1) Добавить\n2) Удалить\n3) Изменить\n4) Показать\n5) Выйти из программы\n");

                            int? option = Convert.ToInt32(Console.ReadLine());

                            Console.Clear();

                            switch (option)
                            {
                                case 1:
                                    dataProcessor.ShowMenuCradAndStartProgram(new CreateUserSQL());

                                    Console.WriteLine();
                                    break;

                                case 2:
                                    dataProcessor.ShowMenuCradAndStartProgram(new DeleteUserSql());
                                    break;

                                case 3:
                                    dataProcessor.ShowMenuCradAndStartProgram(new ChangeUserSql());
                                    break;

                                case 4:
                                    dataProcessor.ShowMenuCradAndStartProgram(new ShowUsersForSQL());
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

                public void ShowMenuCradAndStartProgram(IDatatProvider forCRUD)
                {
                    throw new NotImplementedException();
                }
            }

            class ConsoleProccesor : IDataProcessor
            {
                public void ShowMenuCradAndStartProgram(IDatatProvider forCRUD)
                {
                    forCRUD.CRUDSystem();
                }
            }            

            class ShowUsersForSQL : IDatatProvider
            {
                public void CRUDSystem()
                {
                    using ApplicationContext db = new ApplicationContext();
                    var users = db.Users.ToList();

                    foreach (User u in users)
                    {
                        Console.WriteLine($"Id: {u.Id}. Имя: {u.Name}. Возраст: {u.Age}");
                    }
                    Console.WriteLine();
                }
            }

            class CreateUserSQL : IDatatProvider
            {
                public void CRUDSystem()
                {
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
            }

            class DeleteUserSql : IDatatProvider
            {
                public void CRUDSystem()
                {
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
            }

            class ChangeUserSql : IDatatProvider
            {
                public void CRUDSystem()
                {
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
}