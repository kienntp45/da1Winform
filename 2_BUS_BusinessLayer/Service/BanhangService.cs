﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_BUS_BusinessLayer.IService;
using _2_BUS_BusinessLayer.Models;
using _1_DAL_DataAccessLayer.Models;
using _1_DAL_DataAccessLayer.IDALServices;
using _1_DAL_DataAccessLayer.DALServices;

namespace _2_BUS_BusinessLayer.Service
{
    public class BanhangService : IBanhangService
    {
        private ISanphamServices _sanphamServices; // khởi tạo interface 16-30
        private IChitietSanPhamServices _chitietSanPhamServices;
        private INhacungcapServices _nhacungcapServices;
        private IImgServices _imgServices;
        private IChatlieuServices _chatlieuServices;
        private ISizeServices _SizeServices;
        private ILoaicogiayServices _loaicogiayServices;
        private IColorServices _colorServices;
        private IDanhmucServices _danhmucServices;
        private IHoadonchitietServices _hoadonchitietServices;
        private INhanvienServices _nhanvienServices;
        private IKhachhangServices _khachhangServices;
        private IHoadonServices _hoadonServices;
        private IVouCherServices _vouCherServices;

        private List<SanphambanViewModel> _lstSanphambanViews; // khởi tạo list đối đối tượng  31-47
        private List<AddHoadon> _lstaddHoadons;

        private List<SanPham> _lstsanPhams;
        private List<ChiTietSanPham> _lstChiTietSanPhams;
        private List<NhaCungCap> _lstnhaCungCaps;
        private List<Image> _lstimages;
        private List<ChatLieu> _lstchatLieus;
        private List<Size> _lstSizes;
        private List<LoaiCoGiay> _lstloaiCoGiays;
        private List<Color> _lstcolors;
        private List<DanhMuc> _lstdanhMucs;
        private List<HoaDonChiTiet> _lsthoaDonChiTiets;
        private List<NhanVien> _lstnhanViens;
        private List<KhachHang> _lstkhachHangs;
        private List<HoaDon> _lsthoaDons;
        private List<Voucher> _lstvouchers;






        public BanhangService() // controler // lớp khởi tạo
        {
            _sanphamServices = new SanphamServices();
            _chitietSanPhamServices = new ChitietSanPhamServices();
            _nhacungcapServices = new NhacungcapServices();
            _imgServices = new ImgServices();
            _chatlieuServices = new ChatlieuServices();
            _SizeServices = new SizeServices();
            _loaicogiayServices = new LoaicogiayServices();
            _colorServices = new ColorServices();
            _danhmucServices = new DanhmucServices();
            _hoadonchitietServices = new HoadonchitietServices();
            _nhanvienServices = new NhanvienServices();
            _khachhangServices = new KhachhangServices();
            _hoadonServices = new HoadonServices();
            _vouCherServices = new VouCherServices();
           

            _lstSanphambanViews = new List<SanphambanViewModel>();
            _lstaddHoadons = new List<AddHoadon>();

            _lstsanPhams = _sanphamServices.Getlst(); // lấy dữ liệu db từ tầng 1_dal 75-89
            _lstChiTietSanPhams = _chitietSanPhamServices.Getlst();
            _lstnhaCungCaps = _nhacungcapServices.Getlst();
            _lstimages = _imgServices.Getlst();
            _lstchatLieus = _chatlieuServices.Getlst();
            _lstSizes = _SizeServices.Getlst();
            _lstloaiCoGiays = _loaicogiayServices.Getlst();
            _lstcolors = _colorServices.Getlst();
            _lstdanhMucs = _danhmucServices.Getlst();
            _lsthoaDonChiTiets = _hoadonchitietServices.Getlst();
            _lstnhanViens = _nhanvienServices.Getlst();
            _lstkhachHangs = _khachhangServices.Getlst();
            _lsthoaDons = _hoadonServices.Getlst();
            _lstvouchers = _vouCherServices.Getlst();

        }

        #region loaddata

     

        public List<SanPham> loadsp()
        {
            return _lstsanPhams;
        }  
        public List<ChiTietSanPham> loadspct()
        {
            return _lstChiTietSanPhams;
        } 
        public List<NhanVien> loadnv()
        {
            return _lstnhanViens;
        }
        public List<KhachHang> loadkh()
        {
            return _lstkhachHangs=_khachhangServices.Getlst();
        }
        public List<HoaDon> loadhd()
        {
            return _lsthoaDons=_hoadonServices.Getlst();
        }
        public List<HoaDonChiTiet> loadhdct()
        {
            return _lsthoaDonChiTiets=_hoadonchitietServices.Getlst();
        }

        public List<Voucher> loadVoucher()
        {
            return _lstvouchers = _vouCherServices.Getlst();
        }

        #endregion



        public List<AddHoadon> viewHoadons()
        {
            _lstsanPhams = _sanphamServices.Getlst();
            _lstChiTietSanPhams = _chitietSanPhamServices.Getlst();
            _lsthoaDonChiTiets = _hoadonchitietServices.Getlst();
            _lsthoaDons = _hoadonServices.Getlst();
            _lstaddHoadons = (from a in _lstsanPhams
                              join b in _lstChiTietSanPhams on a.MaSp equals b.MaSP
                              join c in _lsthoaDonChiTiets on b.MaCTSP equals c.MaCTSP
                              join d in _lsthoaDons on c.MaHd equals d.MaHd
                              select new AddHoadon()
                              {
                                  sanPham = a,
                                  chiTietSanPham=b,
                                  hoaDonChiTiet=c,
                                  hoaDon=d


                              }).ToList();
          
            return _lstaddHoadons;
        }




        public List<SanphambanViewModel> SanphambanViews()
        {

            _lstSanphambanViews = (from a in _lstsanPhams
                                   join b in _lstChiTietSanPhams on a.MaSp equals b.MaSP
                                   join c in _lstnhaCungCaps on a.MaNCC equals c.MaNcc
                                   join d in _lstimages on b.MaCTSP equals d.MaCTSP
                                   join e in _lstchatLieus on b.MaChatLieu equals e.MaChatLieu
                                   join f in _lstSizes on b.MaSize equals f.MaSize
                                   join g in _lstloaiCoGiays on b.MaCo equals g.MaCo
                                   join h in _lstcolors on b.MaCLR equals h.MaClr
                                   join q in _lstdanhMucs on a.MaDanhMuc equals q.MaDanhMuc
                                   select new SanphambanViewModel()
                                   {
                                       sanPham = a,
                                       chiTietSanPham = b,
                                       nhaCungCap = c,
                                       image = d,
                                       ChatLieu = e,
                                       size = f,
                                       loaiCoGiay = g,
                                       color = h,
                                       danhMuc = q
                                   }).ToList();

            return _lstSanphambanViews;
        }

       
        public string addhoadon(HoaDon hoaDon, KhachHang khachHang)
        {
            _khachhangServices.add(khachHang);
            _hoadonServices.add(hoaDon);
            return "tạo thành công";
        }

        public string updatehoadon(HoaDon hoaDon)
        {
            _hoadonServices.update(hoaDon);
            return "Thanh toán thành công";
        }

        public string addHoadonchitiet(HoaDonChiTiet hoaDonChiTiet)
        {
            _hoadonchitietServices.add(hoaDonChiTiet);
            return null;
        }
        public string updatehoadonchitiet(HoaDonChiTiet hoaDonChiTiet)
        {
            _hoadonchitietServices.update(hoaDonChiTiet);
            return null;
        }
        public string deletedhoadonchitiet(string hd, string msp)
        {
            _hoadonchitietServices.delete(hd, msp);
            return null;
        }

        public string updatectsp(ChiTietSanPham chiTietSanPham)
        {
            _chitietSanPhamServices.update(chiTietSanPham);
            return null;
        } 
        public string updatekhachhang(KhachHang khachHang)
        {
            _khachhangServices.update(khachHang);
            return "Đặt hàng thành công";
        }
        public string updatevoucher(Voucher voucher)
        {
            _vouCherServices.update(voucher);
            return null;
        }
    }
}
