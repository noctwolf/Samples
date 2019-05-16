namespace 同时使用多个实体框架.CodeFirst
{
    using System.Data.Entity;

    public class SqlServerModel : DbContext
    {
        public SqlServerModel() : base("name=SqlServerModel") =>
            Database.SetInitializer(new DropCreateDatabaseAlways<SqlServerModel>());

        public virtual DbSet<MyEntity> MyEntities { get; set; }
    }
}