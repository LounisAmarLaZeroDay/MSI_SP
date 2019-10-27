namespace Data.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using FirebirdSql.Data.FirebirdClient;
    using LaZeroDayCore.Config;
    using Data.Utils;

    public partial class Entities : DbContext
    {
        //********************************************************
        #region init
        private Entities() // Entities = Constructor of calss model
               : base(new FbConnection(C_Setting_DB.get_db_url()), true) //C_Setting.get_db_url()== function get database string
        { }
        public Entities(string connString)
                : base(new FbConnection(connString), true)
        { }
        private static Entities Instance;
        public static Entities GetInstance()
        {
            if (Instance == null)
            {
                Instance = new Entities();
                if (!Instance.Database.Exists()) { DB_Access.GetInstatce().CreateNew(); Instance = new Entities(); }
            }
            initDatabase();
            return Instance;
        }
        public static void recreate()
        {
            Instance.Dispose();
            Instance = null;
        }
        static void initDatabase()
        {
            try
            {
                if (Instance.USERS.Count() == 0)
                {
                    USER t = new USER
                    {
                        ID = 1,
                        NAME = "admin",
                        PASSWORD = ""
                    };
                    Instance.USERS.Add(t);
                    Instance.SaveChanges();
                }
                if (Instance.CUSTOMERS.Count() == 0)
                {
                    CUSTOMER t = new CUSTOMER
                    {
                        ID = 1,
                        NAME = "default",
                    };
                    Instance.CUSTOMERS.Add(t);
                    Instance.SaveChanges();
                }
                if (Instance.WHOLESALERS.Count() == 0)
                {
                    WHOLESALER t = new WHOLESALER
                    {
                        ID = 1,
                        NAME = "default",
                    };
                    Instance.WHOLESALERS.Add(t);
                    Instance.SaveChanges();
                }
            }
            catch
            {

            }
        }
        #endregion
        //********************************************************


        public virtual DbSet<CUSTOMER> CUSTOMERS { get; set; }
        public virtual DbSet<INVOICES_PURCHASES> INVOICES_PURCHASES { get; set; }
        public virtual DbSet<INVOICES_SELLS> INVOICES_SELLS { get; set; }
        public virtual DbSet<PRODUCT> PRODUCTS { get; set; }
        public virtual DbSet<PRODUCTS_PURCHASES> PRODUCTS_PURCHASES { get; set; }
        public virtual DbSet<PRODUCTS_SELLS> PRODUCTS_SELLS { get; set; }
        public virtual DbSet<USER> USERS { get; set; }
        public virtual DbSet<WHOLESALER> WHOLESALERS { get; set; }
        public virtual DbSet<CUSTOMERS_TYPE> CUSTOMERS_TYPE { get; set; }
        public virtual DbSet<PRODUCTS_TYPE> PRODUCTS_TYPE { get; set; }
        public virtual DbSet<PRODUCTS_UNITE_TYPE> PRODUCTS_UNITE_TYPE { get; set; }
        public virtual DbSet<USERS_TYPE> USERS_TYPE { get; set; }
        public virtual DbSet<WHOLESALERS_TYPE> WHOLESALERS_TYPE { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CUSTOMER>()
                .HasMany(e => e.INVOICES_SELLS)
                .WithOptional(e => e.CUSTOMER)
                .HasForeignKey(e => e.ID_CUSTOMERS)
                .WillCascadeOnDelete();

            modelBuilder.Entity<INVOICES_PURCHASES>()
                .HasMany(e => e.PRODUCTS_PURCHASES)
                .WithOptional(e => e.INVOICES_PURCHASES)
                .HasForeignKey(e => e.ID_INVOICES_PURCHASES)
                .WillCascadeOnDelete();

            modelBuilder.Entity<INVOICES_SELLS>()
                .HasMany(e => e.PRODUCTS_SELLS)
                .WithOptional(e => e.INVOICES_SELLS)
                .HasForeignKey(e => e.ID_INVOICES_SELLS)
                .WillCascadeOnDelete();

            modelBuilder.Entity<PRODUCT>()
                .HasMany(e => e.PRODUCTS_PURCHASES)
                .WithOptional(e => e.PRODUCT)
                .HasForeignKey(e => e.ID_PRODUCTS)
                .WillCascadeOnDelete();

            modelBuilder.Entity<PRODUCT>()
                .HasMany(e => e.PRODUCTS_SELLS)
                .WithOptional(e => e.PRODUCT)
                .HasForeignKey(e => e.ID_PRODUCTS)
                .WillCascadeOnDelete();

            modelBuilder.Entity<USER>()
                .HasMany(e => e.INVOICES_PURCHASES)
                .WithOptional(e => e.USER)
                .HasForeignKey(e => e.ID_USERS)
                .WillCascadeOnDelete();

            modelBuilder.Entity<USER>()
                .HasMany(e => e.INVOICES_SELLS)
                .WithOptional(e => e.USER)
                .HasForeignKey(e => e.ID_USERS)
                .WillCascadeOnDelete();

            modelBuilder.Entity<WHOLESALER>()
                .HasMany(e => e.INVOICES_PURCHASES)
                .WithOptional(e => e.WHOLESALER)
                .HasForeignKey(e => e.ID_WHOLESALERS)
                .WillCascadeOnDelete();
        }
    }
}
