// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using _1_DAL_DataAccessLayer.Context;

namespace _1_DAL_DataAccessLayer.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20220420091057_ss1.3")]
    partial class ss13
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("_1_DAL_DataAccessLayer.Models.ChatLieu", b =>
                {
                    b.Property<string>("MaChatLieu")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ChatLieuSP")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("MaChatLieu");

                    b.ToTable("ChatLieu");
                });

            modelBuilder.Entity("_1_DAL_DataAccessLayer.Models.ChiTietGiamGia", b =>
                {
                    b.Property<string>("MaCTGG")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MaDanhMuc")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MaKM")
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("MaCTGG");

                    b.HasIndex("MaDanhMuc");

                    b.HasIndex("MaKM");

                    b.ToTable("ChiTietGiamGia");
                });

            modelBuilder.Entity("_1_DAL_DataAccessLayer.Models.ChiTietSanPham", b =>
                {
                    b.Property<string>("MaCTSP")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("GhiChu")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("GiaNhap")
                        .HasColumnType("int");

                    b.Property<string>("MaCLR")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MaChatLieu")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MaCo")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MaPB")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MaQR")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MaSP")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MaSize")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Mota")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("TrangThai")
                        .HasColumnType("int");

                    b.Property<int?>("giaban")
                        .HasColumnType("int");

                    b.Property<int?>("soluong")
                        .HasColumnType("int");

                    b.HasKey("MaCTSP");

                    b.HasIndex("MaCLR");

                    b.HasIndex("MaChatLieu");

                    b.HasIndex("MaCo");

                    b.HasIndex("MaPB");

                    b.HasIndex("MaSP");

                    b.HasIndex("MaSize");

                    b.ToTable("ChiTietSanPham");
                });

            modelBuilder.Entity("_1_DAL_DataAccessLayer.Models.ChucVu", b =>
                {
                    b.Property<string>("MaChucVu")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Roles")
                        .HasColumnType("int");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("MaChucVu");

                    b.ToTable("ChucVu");
                });

            modelBuilder.Entity("_1_DAL_DataAccessLayer.Models.Color", b =>
                {
                    b.Property<string>("MaClr")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ColorSP")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("MaClr");

                    b.ToTable("Color");
                });

            modelBuilder.Entity("_1_DAL_DataAccessLayer.Models.DanhMuc", b =>
                {
                    b.Property<string>("MaDanhMuc")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("GhiChu")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TenDanhMuc")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("MaDanhMuc");

                    b.ToTable("DanhMuc");
                });

            modelBuilder.Entity("_1_DAL_DataAccessLayer.Models.History", b =>
                {
                    b.Property<string>("MaHis")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MaCTSP")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MaNV")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("NgayNhap")
                        .HasColumnType("datetime2");

                    b.HasKey("MaHis");

                    b.HasIndex("MaCTSP");

                    b.HasIndex("MaNV");

                    b.ToTable("history");
                });

            modelBuilder.Entity("_1_DAL_DataAccessLayer.Models.HoaDon", b =>
                {
                    b.Property<string>("MaHd")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("GhiChu")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MaKH")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MaNV")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MaVouCher")
                        .HasColumnType("nvarchar(100)");

                    b.Property<double?>("TienNhan")
                        .HasColumnType("float");

                    b.Property<double?>("Tongtien")
                        .HasColumnType("float");

                    b.Property<int?>("TrangThaiHd")
                        .HasColumnType("int");

                    b.Property<DateTime>("thoigian")
                        .HasColumnType("datetime2");

                    b.HasKey("MaHd");

                    b.HasIndex("MaKH");

                    b.HasIndex("MaNV");

                    b.HasIndex("MaVouCher");

                    b.ToTable("HoaDon");
                });

            modelBuilder.Entity("_1_DAL_DataAccessLayer.Models.HoaDonChiTiet", b =>
                {
                    b.Property<string>("MaCTSP")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MaHd")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<double?>("DonGia")
                        .HasColumnType("float");

                    b.Property<double?>("Thanhtien")
                        .HasColumnType("float");

                    b.Property<int?>("TrangThai")
                        .HasColumnType("int");

                    b.Property<int?>("soluong")
                        .HasColumnType("int");

                    b.HasKey("MaCTSP", "MaHd");

                    b.HasIndex("MaHd");

                    b.ToTable("HoaDonChiTiet");
                });

            modelBuilder.Entity("_1_DAL_DataAccessLayer.Models.Image", b =>
                {
                    b.Property<string>("MaImage")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Images")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MaCTSP")
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("MaImage");

                    b.HasIndex("MaCTSP");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("_1_DAL_DataAccessLayer.Models.KhachHang", b =>
                {
                    b.Property<string>("MaKh")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Diachi")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Sdt")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TenKh")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("MaKh");

                    b.ToTable("KhachHang");
                });

            modelBuilder.Entity("_1_DAL_DataAccessLayer.Models.KhuyenMai", b =>
                {
                    b.Property<string>("MaKM")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("GhiChu")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("GiamGia")
                        .HasColumnType("int");

                    b.Property<DateTime?>("NgayDau")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayHet")
                        .HasColumnType("datetime2");

                    b.Property<string>("TenChuongTrinh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("MaKM");

                    b.ToTable("KhuyenMai");
                });

            modelBuilder.Entity("_1_DAL_DataAccessLayer.Models.LoaiCoGiay", b =>
                {
                    b.Property<string>("MaCo")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LoaiCoGiaySP")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("MaCo");

                    b.ToTable("LoaiCoGiay");
                });

            modelBuilder.Entity("_1_DAL_DataAccessLayer.Models.NhaCungCap", b =>
                {
                    b.Property<string>("MaNcc")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CongTyCc")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("DiaChi")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("GhiChu")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NguoiCc")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Sdt")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("MaNcc");

                    b.ToTable("NhaCungCap");
                });

            modelBuilder.Entity("_1_DAL_DataAccessLayer.Models.NhanVien", b =>
                {
                    b.Property<string>("MaNv")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("DiaChi")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("GioiTinh")
                        .HasColumnType("int");

                    b.Property<string>("HinhAnh")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MaChucVu")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MatKhau")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("NamSinh")
                        .HasColumnType("int");

                    b.Property<string>("Sdt")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TenNv")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("MaNv");

                    b.HasIndex("MaChucVu");

                    b.ToTable("NhanVien");
                });

            modelBuilder.Entity("_1_DAL_DataAccessLayer.Models.ProductBack", b =>
                {
                    b.Property<string>("MaPB")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("ProductStatus")
                        .HasColumnType("int");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("MaPB");

                    b.ToTable("ProductBack");
                });

            modelBuilder.Entity("_1_DAL_DataAccessLayer.Models.SanPham", b =>
                {
                    b.Property<string>("MaSp")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MaDanhMuc")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MaNCC")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TenSp")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ThuongHieu")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("MaSp");

                    b.HasIndex("MaDanhMuc");

                    b.HasIndex("MaNCC");

                    b.ToTable("SanPham");
                });

            modelBuilder.Entity("_1_DAL_DataAccessLayer.Models.Size", b =>
                {
                    b.Property<string>("MaSize")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<double>("ChieuDaiChan")
                        .HasColumnType("float");

                    b.Property<int>("SizeSp")
                        .HasColumnType("int");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("MaSize");

                    b.ToTable("Size");
                });

            modelBuilder.Entity("_1_DAL_DataAccessLayer.Models.Voucher", b =>
                {
                    b.Property<string>("MaVouCher")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("GhiChu")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("HSD")
                        .HasColumnType("datetime2");

                    b.Property<float>("MenhGia")
                        .HasColumnType("real");

                    b.Property<DateTime>("NSD")
                        .HasColumnType("datetime2");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.Property<int>("SoLuongDSD")
                        .HasColumnType("int");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.Property<string>("Vouchers")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("MaVouCher");

                    b.ToTable("Voucher");
                });

            modelBuilder.Entity("_1_DAL_DataAccessLayer.Models.ChiTietGiamGia", b =>
                {
                    b.HasOne("_1_DAL_DataAccessLayer.Models.DanhMuc", "danhMuc")
                        .WithMany()
                        .HasForeignKey("MaDanhMuc");

                    b.HasOne("_1_DAL_DataAccessLayer.Models.KhuyenMai", "khuyenMai")
                        .WithMany()
                        .HasForeignKey("MaKM");

                    b.Navigation("danhMuc");

                    b.Navigation("khuyenMai");
                });

            modelBuilder.Entity("_1_DAL_DataAccessLayer.Models.ChiTietSanPham", b =>
                {
                    b.HasOne("_1_DAL_DataAccessLayer.Models.Color", "color")
                        .WithMany()
                        .HasForeignKey("MaCLR");

                    b.HasOne("_1_DAL_DataAccessLayer.Models.ChatLieu", "chatLieu")
                        .WithMany()
                        .HasForeignKey("MaChatLieu");

                    b.HasOne("_1_DAL_DataAccessLayer.Models.LoaiCoGiay", "loaiCoGiay")
                        .WithMany()
                        .HasForeignKey("MaCo");

                    b.HasOne("_1_DAL_DataAccessLayer.Models.ProductBack", "productBack")
                        .WithMany()
                        .HasForeignKey("MaPB");

                    b.HasOne("_1_DAL_DataAccessLayer.Models.SanPham", "sanPham")
                        .WithMany()
                        .HasForeignKey("MaSP");

                    b.HasOne("_1_DAL_DataAccessLayer.Models.Size", "size")
                        .WithMany()
                        .HasForeignKey("MaSize");

                    b.Navigation("chatLieu");

                    b.Navigation("color");

                    b.Navigation("loaiCoGiay");

                    b.Navigation("productBack");

                    b.Navigation("sanPham");

                    b.Navigation("size");
                });

            modelBuilder.Entity("_1_DAL_DataAccessLayer.Models.History", b =>
                {
                    b.HasOne("_1_DAL_DataAccessLayer.Models.ChiTietSanPham", "chiTietSanPham")
                        .WithMany()
                        .HasForeignKey("MaCTSP");

                    b.HasOne("_1_DAL_DataAccessLayer.Models.NhanVien", "nhanVien")
                        .WithMany()
                        .HasForeignKey("MaNV");

                    b.Navigation("chiTietSanPham");

                    b.Navigation("nhanVien");
                });

            modelBuilder.Entity("_1_DAL_DataAccessLayer.Models.HoaDon", b =>
                {
                    b.HasOne("_1_DAL_DataAccessLayer.Models.KhachHang", "khachHang")
                        .WithMany()
                        .HasForeignKey("MaKH")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_1_DAL_DataAccessLayer.Models.NhanVien", "nhanVien")
                        .WithMany()
                        .HasForeignKey("MaNV")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_1_DAL_DataAccessLayer.Models.Voucher", "voucher")
                        .WithMany()
                        .HasForeignKey("MaVouCher")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("khachHang");

                    b.Navigation("nhanVien");

                    b.Navigation("voucher");
                });

            modelBuilder.Entity("_1_DAL_DataAccessLayer.Models.HoaDonChiTiet", b =>
                {
                    b.HasOne("_1_DAL_DataAccessLayer.Models.ChiTietSanPham", "chiTietSanPham")
                        .WithMany()
                        .HasForeignKey("MaCTSP")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_1_DAL_DataAccessLayer.Models.HoaDon", "hoaDon")
                        .WithMany()
                        .HasForeignKey("MaHd")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("chiTietSanPham");

                    b.Navigation("hoaDon");
                });

            modelBuilder.Entity("_1_DAL_DataAccessLayer.Models.Image", b =>
                {
                    b.HasOne("_1_DAL_DataAccessLayer.Models.ChiTietSanPham", "chiTietSanPham")
                        .WithMany()
                        .HasForeignKey("MaCTSP");

                    b.Navigation("chiTietSanPham");
                });

            modelBuilder.Entity("_1_DAL_DataAccessLayer.Models.NhanVien", b =>
                {
                    b.HasOne("_1_DAL_DataAccessLayer.Models.ChucVu", "chucVu")
                        .WithMany()
                        .HasForeignKey("MaChucVu");

                    b.Navigation("chucVu");
                });

            modelBuilder.Entity("_1_DAL_DataAccessLayer.Models.SanPham", b =>
                {
                    b.HasOne("_1_DAL_DataAccessLayer.Models.DanhMuc", "danhMuc")
                        .WithMany()
                        .HasForeignKey("MaDanhMuc");

                    b.HasOne("_1_DAL_DataAccessLayer.Models.NhaCungCap", "nhaCungCap")
                        .WithMany()
                        .HasForeignKey("MaNCC");

                    b.Navigation("danhMuc");

                    b.Navigation("nhaCungCap");
                });
#pragma warning restore 612, 618
        }
    }
}
