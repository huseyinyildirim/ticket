

// This file was automatically generated.
// Do not make changes directly to this file - edit the template instead.
// 
// The following connection settings were used to generate this file
// 
//     Configuration file:     "Web\Web.config"
//     Connection String Name: "DBDataContext"
//     Connection String:      "data source=DEVELOPER\SQLEXPRESS;initial catalog=DB_TICKET;persist security info=True;user id=sa;password=**zapped**;App=EntityFramework"

// ReSharper disable RedundantUsingDirective
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
//using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.DatabaseGeneratedOption;

namespace Data.Model
{
    // ************************************************************************
    // Unit of work
    public interface IDBDataContext : IDisposable
    {
        IDbSet<REF_KEYVALUE> REF_KEYVALUE { get; set; } // REF_KEYVALUE
        IDbSet<TBL_CITY> TBL_CITY { get; set; } // TBL_CITY
        IDbSet<TBL_PERMISSION> TBL_PERMISSION { get; set; } // TBL_PERMISSION
        IDbSet<TBL_USER> TBL_USER { get; set; } // TBL_USER
        IDbSet<TBL_USER_PERMISSION> TBL_USER_PERMISSION { get; set; } // TBL_USER_PERMISSION
        IDbSet<TBL_USERGROUP> TBL_USERGROUP { get; set; } // TBL_USERGROUP
        IDbSet<TBL_USERGROUP_PERMISSION> TBL_USERGROUP_PERMISSION { get; set; } // TBL_USERGROUP_PERMISSION

        int SaveChanges();
    }

    // ************************************************************************
    // Database context
    public class DBDataContext : DbContext, IDBDataContext
    {
        public IDbSet<REF_KEYVALUE> REF_KEYVALUE { get; set; } // REF_KEYVALUE
        public IDbSet<TBL_CITY> TBL_CITY { get; set; } // TBL_CITY
        public IDbSet<TBL_PERMISSION> TBL_PERMISSION { get; set; } // TBL_PERMISSION
        public IDbSet<TBL_USER> TBL_USER { get; set; } // TBL_USER
        public IDbSet<TBL_USER_PERMISSION> TBL_USER_PERMISSION { get; set; } // TBL_USER_PERMISSION
        public IDbSet<TBL_USERGROUP> TBL_USERGROUP { get; set; } // TBL_USERGROUP
        public IDbSet<TBL_USERGROUP_PERMISSION> TBL_USERGROUP_PERMISSION { get; set; } // TBL_USERGROUP_PERMISSION

        static DBDataContext()
        {
            Database.SetInitializer<DBDataContext>(null);
        }

        public DBDataContext()
            : base("Name=DBDataContext")
        {
        }

        public DBDataContext(string connectionString) : base(connectionString)
        {
        }

        public DBDataContext(string connectionString, System.Data.Entity.Infrastructure.DbCompiledModel model) : base(connectionString, model)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new REF_KEYVALUEConfiguration());
            modelBuilder.Configurations.Add(new TBL_CITYConfiguration());
            modelBuilder.Configurations.Add(new TBL_PERMISSIONConfiguration());
            modelBuilder.Configurations.Add(new TBL_USERConfiguration());
            modelBuilder.Configurations.Add(new TBL_USER_PERMISSIONConfiguration());
            modelBuilder.Configurations.Add(new TBL_USERGROUPConfiguration());
            modelBuilder.Configurations.Add(new TBL_USERGROUP_PERMISSIONConfiguration());
        }

        public static DbModelBuilder CreateModel(DbModelBuilder modelBuilder, string schema)
        {
            modelBuilder.Configurations.Add(new REF_KEYVALUEConfiguration(schema));
            modelBuilder.Configurations.Add(new TBL_CITYConfiguration(schema));
            modelBuilder.Configurations.Add(new TBL_PERMISSIONConfiguration(schema));
            modelBuilder.Configurations.Add(new TBL_USERConfiguration(schema));
            modelBuilder.Configurations.Add(new TBL_USER_PERMISSIONConfiguration(schema));
            modelBuilder.Configurations.Add(new TBL_USERGROUPConfiguration(schema));
            modelBuilder.Configurations.Add(new TBL_USERGROUP_PERMISSIONConfiguration(schema));
            return modelBuilder;
        }
    }

    // ************************************************************************
    // POCO classes

    // REF_KEYVALUE
    public class REF_KEYVALUE
    {

        public int ID { get; set; } // ID (Primary key)

        [Required(ErrorMessage = "*")]
        [Display(Name = "Başlık")]
        [StringLength(50, ErrorMessage = "En fazla 50 karakter.")]
        public string NAME { get; set; } // NAME

        [Display(Name = "Üst Başlık")]
        public int? PARENT_ID { get; set; } // PARENT_ID

        public bool STATUS { get; set; } // STATUS

        // Reverse navigation
        public virtual ICollection<REF_KEYVALUE> REF_KEYVALUE2 { get; set; } // REF_KEYVALUE.FK__REF_KEYVA__PAREN__5070F446

        // Foreign keys
        public virtual REF_KEYVALUE REF_KEYVALUE1 { get; set; } // FK__REF_KEYVA__PAREN__5070F446

        public REF_KEYVALUE()
        {
            REF_KEYVALUE2 = new List<REF_KEYVALUE>();
        }
    }

    // TBL_CITY
    public class TBL_CITY
    {

        public int ID { get; set; } // ID (Primary key)

        [Display(Name = "Ad")]
        [StringLength(50, ErrorMessage = "En fazla 50 karakter.")]
        public string NAME { get; set; } // NAME

        public int? PARENT_ID { get; set; } // PARENT_ID

        public bool STATUS { get; set; } // STATUS

        public double? LAT { get; set; } // LAT

        public double? LONG { get; set; } // LONG
    }

    // TBL_PERMISSION
    public class TBL_PERMISSION
    {

        public int ID { get; set; } // ID (Primary key)

        [Required(ErrorMessage = "*")]
        [Display(Name = "Yetki Adı")]
        [StringLength(150, ErrorMessage = "En fazla 150 karakter.")]
        public string NAME { get; set; } // NAME

        [Display(Name = "Açıklama")]
        [StringLength(250, ErrorMessage = "En fazla 250 karakter.")]
        public string DESCRIPTION { get; set; } // DESCRIPTION

        [Display(Name = "Üst Yetki")]
        public int? PARENT_ID { get; set; } // PARENT_ID

        public bool? ISMENU { get; set; } // ISMENU

        public string ISMENU_LINK { get; set; } // ISMENU_LINK

        public int ORDER { get; set; } // ORDER

        // Reverse navigation
        public virtual ICollection<TBL_PERMISSION> TBL_PERMISSION2 { get; set; } // TBL_PERMISSION.FK__TBL_PERMI__PAREN__5165187F
        public virtual ICollection<TBL_USER_PERMISSION> TBL_USER_PERMISSION { get; set; } // TBL_USER_PERMISSION.FK__TBL_USER___PERMI__6C190EBB
        public virtual ICollection<TBL_USERGROUP_PERMISSION> TBL_USERGROUP_PERMISSION { get; set; } // TBL_USERGROUP_PERMISSION.FK__TBL_USERG__PERMI__6EF57B66

        // Foreign keys
        public virtual TBL_PERMISSION TBL_PERMISSION1 { get; set; } // FK__TBL_PERMI__PAREN__5165187F

        public TBL_PERMISSION()
        {
            ORDER = 999;
            TBL_PERMISSION2 = new List<TBL_PERMISSION>();
            TBL_USER_PERMISSION = new List<TBL_USER_PERMISSION>();
            TBL_USERGROUP_PERMISSION = new List<TBL_USERGROUP_PERMISSION>();
        }
    }

    // TBL_USER
    public class TBL_USER
    {

        public int ID { get; set; } // ID (Primary key)

        [Required(ErrorMessage = "*")]
        [Display(Name = "Kullanıcı Adı")]
        [StringLength(50, ErrorMessage = "En fazla 50 karakter.")]
        public string USERNAME { get; set; } // USERNAME

        [Required(ErrorMessage = "*")]
        [Display(Name = "Şifre")]
        [StringLength(50, ErrorMessage = "En fazla 50 karakter.")]
[DataType(DataType.Password)]
        public string PASSWORD { get; set; } // PASSWORD

        [Required(ErrorMessage = "*")]
        [Display(Name = "Ad")]
        [StringLength(50, ErrorMessage = "En fazla 50 karakter.")]
        public string NAME { get; set; } // NAME

        [Required(ErrorMessage = "*")]
        [Display(Name = "Soyad")]
        [StringLength(50, ErrorMessage = "En fazla 50 karakter.")]
        public string SURNAME { get; set; } // SURNAME

        [Required(ErrorMessage = "*")]
        [Display(Name = "E-Posta")]
        [StringLength(75, ErrorMessage = "En fazla 75 karakter.")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "E-Posta adresi ge�ersiz.")]
        public string EMAIL { get; set; } // EMAIL

        [Display(Name = "Resim")]
        [StringLength(200, ErrorMessage = "En fazla 200 karakter.")]
        public string IMAGE { get; set; } // IMAGE

        [Required(ErrorMessage = "*")]
        [Display(Name = "Kayıt Tarihi")]
        public DateTime ADDED_DATETIME { get; set; } // ADDED_DATETIME

        [Required(ErrorMessage = "*")]
        [Display(Name = "Giriş İzni")]
        public bool ACTIVE { get; set; } // ACTIVE

        public bool STATUS { get; set; } // STATUS

        // Reverse navigation
        public virtual ICollection<TBL_USER_PERMISSION> TBL_USER_PERMISSION { get; set; } // TBL_USER_PERMISSION.FK__TBL_USER___USER___6D0D32F4

        public TBL_USER()
        {
            TBL_USER_PERMISSION = new List<TBL_USER_PERMISSION>();
        }
    }

    // TBL_USER_PERMISSION
    public class TBL_USER_PERMISSION
    {

        public int ID { get; set; } // ID (Primary key)

        [Required(ErrorMessage = "*")]
        [Display(Name = "Kullanıcı")]
        public int USER_ID { get; set; } // USER_ID

        [Display(Name = "Grubu")]
        public int? USERGROUP_ID { get; set; } // USERGROUP_ID

        public int? PERMISSION_ID { get; set; } // PERMISSION_ID

        // Foreign keys
        public virtual TBL_PERMISSION TBL_PERMISSION { get; set; } // FK__TBL_USER___PERMI__6C190EBB
        public virtual TBL_USER TBL_USER { get; set; } // FK__TBL_USER___USER___6D0D32F4
        public virtual TBL_USERGROUP TBL_USERGROUP { get; set; } // FK__TBL_USER___USERG__6E01572D
    }

    // TBL_USERGROUP
    public class TBL_USERGROUP
    {

        public int ID { get; set; } // ID (Primary key)

        [Required(ErrorMessage = "*")]
        [Display(Name = "Grup Adı")]
        [StringLength(25, ErrorMessage = "En fazla 25 karakter.")]
        public string NAME { get; set; } // NAME

        [Required(ErrorMessage = "*")]
        [Display(Name = "Kod")]
        [StringLength(2, ErrorMessage = "En fazla 2 karakter.")]
        public string CODE { get; set; } // CODE

        // Reverse navigation
        public virtual ICollection<TBL_USER_PERMISSION> TBL_USER_PERMISSION { get; set; } // TBL_USER_PERMISSION.FK__TBL_USER___USERG__6E01572D
        public virtual ICollection<TBL_USERGROUP_PERMISSION> TBL_USERGROUP_PERMISSION { get; set; } // TBL_USERGROUP_PERMISSION.FK__TBL_USERG__USERG__6FE99F9F

        public TBL_USERGROUP()
        {
            TBL_USER_PERMISSION = new List<TBL_USER_PERMISSION>();
            TBL_USERGROUP_PERMISSION = new List<TBL_USERGROUP_PERMISSION>();
        }
    }

    // TBL_USERGROUP_PERMISSION
    public class TBL_USERGROUP_PERMISSION
    {

        public int ID { get; set; } // ID (Primary key)

        [Required(ErrorMessage = "*")]
        [Display(Name = "Kullanıcı Grubu")]
        public int USERGROUP_ID { get; set; } // USERGROUP_ID

        [Required(ErrorMessage = "*")]
        [Display(Name = "Yetki")]
        public int PERMISSION_ID { get; set; } // PERMISSION_ID

        // Foreign keys
        public virtual TBL_PERMISSION TBL_PERMISSION { get; set; } // FK__TBL_USERG__PERMI__6EF57B66
        public virtual TBL_USERGROUP TBL_USERGROUP { get; set; } // FK__TBL_USERG__USERG__6FE99F9F
    }


    // ************************************************************************
    // POCO Configuration

    // REF_KEYVALUE
    internal class REF_KEYVALUEConfiguration : EntityTypeConfiguration<REF_KEYVALUE>
    {
        public REF_KEYVALUEConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".REF_KEYVALUE");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.NAME).HasColumnName("NAME").IsRequired().HasMaxLength(50);
            Property(x => x.PARENT_ID).HasColumnName("PARENT_ID").IsOptional();
            Property(x => x.STATUS).HasColumnName("STATUS").IsRequired();

            // Foreign keys
            HasOptional(a => a.REF_KEYVALUE1).WithMany(b => b.REF_KEYVALUE2).HasForeignKey(c => c.PARENT_ID); // FK__REF_KEYVA__PAREN__5070F446
        }
    }

    // TBL_CITY
    internal class TBL_CITYConfiguration : EntityTypeConfiguration<TBL_CITY>
    {
        public TBL_CITYConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".TBL_CITY");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.NAME).HasColumnName("NAME").IsOptional().HasMaxLength(50);
            Property(x => x.PARENT_ID).HasColumnName("PARENT_ID").IsOptional();
            Property(x => x.STATUS).HasColumnName("STATUS").IsRequired();
            Property(x => x.LAT).HasColumnName("LAT").IsOptional();
            Property(x => x.LONG).HasColumnName("LONG").IsOptional();
        }
    }

    // TBL_PERMISSION
    internal class TBL_PERMISSIONConfiguration : EntityTypeConfiguration<TBL_PERMISSION>
    {
        public TBL_PERMISSIONConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".TBL_PERMISSION");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.NAME).HasColumnName("NAME").IsRequired().HasMaxLength(150);
            Property(x => x.DESCRIPTION).HasColumnName("DESCRIPTION").IsOptional().HasMaxLength(250);
            Property(x => x.PARENT_ID).HasColumnName("PARENT_ID").IsOptional();
            Property(x => x.ISMENU).HasColumnName("ISMENU").IsOptional();
            Property(x => x.ISMENU_LINK).HasColumnName("ISMENU_LINK").IsOptional().HasMaxLength(100);
            Property(x => x.ORDER).HasColumnName("ORDER").IsRequired();

            // Foreign keys
            HasOptional(a => a.TBL_PERMISSION1).WithMany(b => b.TBL_PERMISSION2).HasForeignKey(c => c.PARENT_ID); // FK__TBL_PERMI__PAREN__5165187F
        }
    }

    // TBL_USER
    internal class TBL_USERConfiguration : EntityTypeConfiguration<TBL_USER>
    {
        public TBL_USERConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".TBL_USER");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.USERNAME).HasColumnName("USERNAME").IsRequired().HasMaxLength(50);
            Property(x => x.PASSWORD).HasColumnName("PASSWORD").IsRequired().HasMaxLength(50);
            Property(x => x.NAME).HasColumnName("NAME").IsRequired().HasMaxLength(50);
            Property(x => x.SURNAME).HasColumnName("SURNAME").IsRequired().HasMaxLength(50);
            Property(x => x.EMAIL).HasColumnName("EMAIL").IsRequired().HasMaxLength(75);
            Property(x => x.IMAGE).HasColumnName("IMAGE").IsOptional().HasMaxLength(200);
            Property(x => x.ADDED_DATETIME).HasColumnName("ADDED_DATETIME").IsRequired();
            Property(x => x.ACTIVE).HasColumnName("ACTIVE").IsRequired();
            Property(x => x.STATUS).HasColumnName("STATUS").IsRequired();
        }
    }

    // TBL_USER_PERMISSION
    internal class TBL_USER_PERMISSIONConfiguration : EntityTypeConfiguration<TBL_USER_PERMISSION>
    {
        public TBL_USER_PERMISSIONConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".TBL_USER_PERMISSION");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.USER_ID).HasColumnName("USER_ID").IsRequired();
            Property(x => x.USERGROUP_ID).HasColumnName("USERGROUP_ID").IsOptional();
            Property(x => x.PERMISSION_ID).HasColumnName("PERMISSION_ID").IsOptional();

            // Foreign keys
            HasRequired(a => a.TBL_USER).WithMany(b => b.TBL_USER_PERMISSION).HasForeignKey(c => c.USER_ID); // FK__TBL_USER___USER___6D0D32F4
            HasOptional(a => a.TBL_USERGROUP).WithMany(b => b.TBL_USER_PERMISSION).HasForeignKey(c => c.USERGROUP_ID); // FK__TBL_USER___USERG__6E01572D
            HasOptional(a => a.TBL_PERMISSION).WithMany(b => b.TBL_USER_PERMISSION).HasForeignKey(c => c.PERMISSION_ID); // FK__TBL_USER___PERMI__6C190EBB
        }
    }

    // TBL_USERGROUP
    internal class TBL_USERGROUPConfiguration : EntityTypeConfiguration<TBL_USERGROUP>
    {
        public TBL_USERGROUPConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".TBL_USERGROUP");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.NAME).HasColumnName("NAME").IsRequired().HasMaxLength(25);
            Property(x => x.CODE).HasColumnName("CODE").IsRequired().HasMaxLength(2);
        }
    }

    // TBL_USERGROUP_PERMISSION
    internal class TBL_USERGROUP_PERMISSIONConfiguration : EntityTypeConfiguration<TBL_USERGROUP_PERMISSION>
    {
        public TBL_USERGROUP_PERMISSIONConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".TBL_USERGROUP_PERMISSION");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.USERGROUP_ID).HasColumnName("USERGROUP_ID").IsRequired();
            Property(x => x.PERMISSION_ID).HasColumnName("PERMISSION_ID").IsRequired();

            // Foreign keys
            HasRequired(a => a.TBL_USERGROUP).WithMany(b => b.TBL_USERGROUP_PERMISSION).HasForeignKey(c => c.USERGROUP_ID); // FK__TBL_USERG__USERG__6FE99F9F
            HasRequired(a => a.TBL_PERMISSION).WithMany(b => b.TBL_USERGROUP_PERMISSION).HasForeignKey(c => c.PERMISSION_ID); // FK__TBL_USERG__PERMI__6EF57B66
        }
    }

}

