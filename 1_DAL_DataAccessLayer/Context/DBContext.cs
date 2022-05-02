using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using _1_DAL_DataAccessLayer.Models;

namespace _1_DAL_DataAccessLayer.Context
{
    class DBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //Khi cấu hình đổi tên DataBase mà các bạn muốn tạo ra
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-MTHHT25V\\SQLEXPRESS;Initial Catalog=da28;Persist Security Info=True;User ID=kien2810;Password=kien2810");
            }
        }
        //2.tiến hành tạo bảng
        public DbSet<NhanVien> NhanViens { get; set; }//Mỗi DbSet sẽ ánh xạ tới một bảng trong cơ sở dữ liệu. 
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<Size> Sizes  { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public DbSet<LoaiCoGiay> LoaiCoGiays { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<HoaDonChiTiet> HoaDonChiTiets { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<DanhMuc> DanhMucs { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<ChucVu> ChucVus { get; set; }
        public DbSet<ChiTietSanPham> ChiTietSanPhams { get; set; }
        public DbSet<ChatLieu> ChatLieus { get; set; }
        public DbSet<ChiTietGiamGia> ChiTietGiamGias { get; set; }
        public DbSet<KhuyenMai> KhuyenMais { get; set; }
        public DbSet<ProductBack> ProductBacks { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)// haskey bảng có quan hệ yếu
        {
            builder.Entity<HoaDonChiTiet>().HasKey(table => new {
                table.MaCTSP,
                table.MaHd
            });
        }

    }
}
