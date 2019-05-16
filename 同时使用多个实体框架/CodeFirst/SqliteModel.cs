namespace 同时使用多个实体框架.CodeFirst
{
    using System.Data.Entity;

    public class SqliteModel : DbContext
    {
        public SqliteModel() : base("name=SqliteModel") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) =>
            Database.SetInitializer(new SQLite.CodeFirst.SqliteDropCreateDatabaseAlways<SqliteModel>(modelBuilder));

        public virtual DbSet<MyEntity> MyEntities { get; set; }
    }
}