using QLSV.Configurations;
using Microsoft.EntityFrameworkCore;
using QLSV.Models;
using QLSV.ModelViews;
using QLSV.OtpModels;
using QLSV.Extension;

namespace QLSV
{
    public class GameStoreDbContext : DbContext
    {
        public GameStoreDbContext(DbContextOptions op) : base(op)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new KhoaHocConfiguration());
            modelBuilder.ApplyConfiguration(new GiaoVienConfiguration());
            modelBuilder.ApplyConfiguration(new HocSinhConfiguration());
            modelBuilder.ApplyConfiguration(new DiemHocSinhConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
            modelBuilder.ApplyConfiguration(new RefundConfiguration());
            modelBuilder.ApplyConfiguration(new AdminConfiguration());
            modelBuilder.ApplyConfiguration(new FundConfiguration());
            modelBuilder.ApplyConfiguration(new BlogConfigurationcs());
            modelBuilder.ApplyConfiguration(new LichHocConfiguration());
            modelBuilder.ApplyConfiguration(new AddFundTransactionConfiguration());
            modelBuilder.ApplyConfiguration(new FileConfiguration());
            modelBuilder.Entity<OrderDetail>().Property(e => e.Price).HasPrecision(18, 2);
            modelBuilder.Entity<HocSinh>().Property(e => e.Balance).HasPrecision(18, 2);
            modelBuilder.Entity<Order>().Property(e => e.TotalPrice).HasPrecision(18, 2);
            modelBuilder.Entity<Refund>().Property(e => e.Price).HasPrecision(18, 2);
            modelBuilder.Entity<Admin>().HasData(new Admin()
            {
                HoTen = "Admin",
                Password = "123456".ToMD5(),
                IsActive = true,
                CreateDate = System.DateTime.Now,
                TaiKhoan = "admin@admin.com"
            });
        }

        public DbSet<HocSinh> HocSinhs { get; set; }
        public DbSet<DiemHocSinh> DiemHocSinhs { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<GiaoVien> GiaoViens { get; set; }
        public DbSet<KhoaHoc> KhoaHocs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Refund> Refunds { get; set; }
        public DbSet<LichHoc> LichHocs { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<ThongBao> ThongBaos { get; set; }
        public DbSet<ThongBaoKhoaHoc> ThongBaoKhoaHocs { get; set; }
        public DbSet<FileKhoaHoc> FileKhoaHocs { get; set; }
        public DbSet<LichKhoaHoc> LichKhoaHocs { get; set; }
    }
}