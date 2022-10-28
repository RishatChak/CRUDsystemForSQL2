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
                    Console.WriteLine("1) ��������");
                    Console.WriteLine("2) �������");
                    Console.WriteLine("3) ��������");
                    Console.WriteLine("4) ��������");
                    Console.WriteLine("5) ��������� �������\n");

                    try
                    {
                        int? option = Convert.ToInt32(Console.ReadLine());

                        switch (option)
                        {

                            case 1:
                                try
                                {
                                    Console.Write("������� ��� ");

                                    string name = Console.ReadLine();

                                    Console.Write("������ ������� ");

                                    int age = Convert.ToInt32(Console.ReadLine());

                                    CreateUser(age, name);

                                    Console.WriteLine();
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("�� ������ ������\n");
                                }
                                break;
                            case 2:
                                Console.Write("�������� id");
                                int id = Convert.ToInt32(Console.ReadLine());

                                DelateUser(id);
                                break;
                            case 3:
                                Console.Write("�������� id ");
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
                                Console.WriteLine("�������� ������������ �������\n");
                                break;
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("�������� ������������ �������\n");
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
                    Console.WriteLine($"Id: {u.Id}. ���: {u.Name}. �������: {u.Age}");
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
                    Console.WriteLine($"��� ������ ��� Id: {id}\n");
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
                        Console.Write("��������� ����� ��: ");
                        string newName = Console.ReadLine();

                        Console.Write("��������� �������� ��: ");

                        int newAge = Convert.ToInt32(Console.ReadLine());

                        user1.Name = newName;
                        user1.Age = newAge;
                        //��������� ������
                        //db.Users.Update(user);
                        db.SaveChanges();

                    }
                    catch (Exception)
                    {
                        Console.WriteLine("�� ������ ������ ������!\n");
                    }
                }
                else
                {
                        Console.WriteLine($"��� ������ �� Id: {id}\n");
                }
            }
        }
    }
}