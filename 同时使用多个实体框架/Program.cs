using System;
using System.Linq;
using 同时使用多个实体框架.CodeFirst;

namespace 同时使用多个实体框架
{
    class Program
    {
        static void Main(string[] args)
        {
            var myEntity = new MyEntity { Name = "Test Name" };
            using (var model = new SqliteModel())
            {
                model.MyEntities.Add(myEntity);
                model.SaveChanges();
                myEntity = model.MyEntities.First();
                Console.WriteLine($"Sqlite Data: myEntity[Id:{myEntity.Id},Name:{myEntity.Name}]");
            }
            using (var model = new SqlServerModel())
            {
                model.MyEntities.Add(myEntity);
                model.SaveChanges();
                myEntity = model.MyEntities.First();
                Console.WriteLine($"SqlServer Data: myEntity[Id:{myEntity.Id},Name:{myEntity.Name}]");
            }
            Console.WriteLine("Test Completed!");
            Console.ReadKey();
        }
    }
}
