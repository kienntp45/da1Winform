﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using _1_DAL_DataAccessLayer.Models;
using _2_BUS_BusinessLayer.IService;
using _2_BUS_BusinessLayer.Models;
using _2_BUS_BusinessLayer.Service;
using AForge.Video;
using AForge.Video.DirectShow;
using BarcodeLib;
using ZXing;
using _1_DAL_DataAccessLayer.IDALServices;
using _1_DAL_DataAccessLayer.DALServices;
using Color = System.Drawing.Color;
using Image = System.Drawing.Image;
using Timer = System.Threading.Timer;

namespace _3_GUI_PresentationLayer
{
    public partial class Frm_sanpham : Form
    {
        private IQlSanPhamService _iQlSanPhamService; // khai báo
        public static string temb;
        public static string _barcode;
        public string _bcode;
        private System.Windows.Forms.Timer _timer;
        QLSanPham qlSanPham;
        IChitietSanPhamServices chitietSanPhamServices;
        public Frm_sanpham()
        {
            InitializeComponent();
            _iQlSanPhamService = new QlSanPhamService();
            load();
            loadCogay();
            loadColor();
            loadSize();
            loadtsp();
            loaddanhMuc();
            trangThai();
            loadChatLieu();
            loadanhMucSP();
            loadChatLieuSP();
            Km();
            giaBan();
            chitietSanPhamServices = new ChitietSanPhamServices();

        }

        private FilterInfoCollection _filterInfoCollection;
        private VideoCaptureDevice _videoCaptureDevice;


        void load() // laod sp
        {

            dgrid.RowsDefaultCellStyle.BackColor = Color.LightBlue;
            dgrid.AlternatingRowsDefaultCellStyle.BackColor = Color.LightSkyBlue;
            dgrid.ColumnCount = 9; // setup data có 9 cột
            dgrid.Columns[0].Name = "Tên sản phẩm";
            dgrid.Columns[1].Name = "Thương hiệu";
            dgrid.Columns[2].Name = "Danh mục";
            dgrid.Columns[3].Name = "Size";
            dgrid.Columns[4].Name = "Màu";
            dgrid.Columns[5].Name = "Chất liệu";
            dgrid.Columns[6].Name = "Giá tiền";
            dgrid.Columns[7].Name = "Trạng thái";
            dgrid.Columns[8].Name = "mã";
            dgrid.Columns[8].Visible = false; // Visible ẩn cột
            dgrid.Rows.Clear();// xóa dl trong data
            foreach (var x in _iQlSanPhamService.GetSPAll())// đẩy dl mới lên dâtt
            {
                dgrid.Rows.Add(x.SanPham.TenSp + ' ' + x.ChiTietSanPham.Mota, x.SanPham.ThuongHieu,
                    x.DanhMuc.TenDanhMuc, x.Size.SizeSp, x.Color.ColorSP, x.ChatLieu.ChatLieuSP, x.ChiTietSanPham.giaban, x.ChiTietSanPham.TrangThai == 0 || x.ChiTietSanPham.soluong <= 0 ? "Hết hàng" : "Còn hàng", x.ChiTietSanPham.MaCTSP);
            }
        }

        void loadChatLieu()
        {
            txt_chatLieu.Items.Clear(); // xóa dl trong   txt_chatLieu
            txt_chatLieu.ResetText();
            foreach (var x in _iQlSanPhamService.GetLstChatLieu()) //đẩy dl mới
            {
                txt_chatLieu.Items.Add(x.ChatLieuSP);
            }

            txt_chatLieu.SelectedIndex = 0;// hiển thị tk add vào đầu tiên của   txt_chatLieu
        }
        void loadChatLieuSP()
        {

            cbo_chatLieu.Items.Add("Tất cả");
            foreach (var x in _iQlSanPhamService.GetLstChatLieu())
            {

                cbo_chatLieu.Items.Add(x.ChatLieuSP);

            }

            cbo_chatLieu.SelectedIndex = 0;
        }
        void loadanhMucSP()
        {

            cbo_loadDanhMuc.Items.Add("Tất cả");
            foreach (var x in _iQlSanPhamService.GetLstDanhMuc())
            {
                cbo_loadDanhMuc.Items.Add(x.TenDanhMuc);
            }
            cbo_loadDanhMuc.SelectedIndex = 0;
        }
        void loaddanhMuc()
        {
            txt_danhmuc.Items.Clear();
            txt_danhmuc.ResetText();
            foreach (var x in _iQlSanPhamService.GetLstDanhMuc())
            {
                txt_danhmuc.Items.Add(x.TenDanhMuc);
            }

            txt_danhmuc.SelectedIndex = 0;
        }
        void loadtsp()
        {
            foreach (var x in _iQlSanPhamService.GetLstSP())
            {
                txt_tsp.Items.Add(x.TenSp);
            }

            txt_tsp.SelectedIndex = 0;

        }

        void loadSize()
        {
            cbx_size.Items.Clear();
            cbx_size.ResetText();
            foreach (var x in _iQlSanPhamService.GetLstSize())
            {
                cbx_size.Items.Add(x.SizeSp);
            }

            cbx_size.SelectedIndex = 0;
        }

        void loadColor()
        {
            cbx_mau.Items.Clear();
            cbx_mau.ResetText();
            foreach (var x in _iQlSanPhamService.GetLstColor())
            {
                cbx_mau.Items.Add(x.ColorSP);
            }

            cbx_mau.SelectedIndex = 0;
        }
        void trangThai()
        {
            cbx_trangThai.Items.Add("Còn hàng");
            cbx_trangThai.Items.Add("Còn cái nịt");
            cbx_trangThai.SelectedIndex = 0;
        }
        void loadCogay()
        {
            foreach (var x in _iQlSanPhamService.GetLstLoaiCoGiay())
            {

                cbx_coGiay.Items.Add(x.LoaiCoGiaySP);

                cbx_coGiay.Items.Add(x.LoaiCoGiaySP);

            }
            cbx_coGiay.SelectedIndex = 0;
        }
        private void Thêm_Click(object sender, EventArgs e) // thêm sản phẩm
        {

            QLSanPham qlSanPham = new QLSanPham();
            qlSanPham.ChiTietSanPham.MaCTSP = "CTSP" + (_iQlSanPhamService.GetLstCTSanPham().Count + 1);//tự tang ma sp
            qlSanPham.ChiTietSanPham.MaSP = _iQlSanPhamService.GetLstSP().Where(c => c.TenSp == txt_tsp.Text).Select(c => c.MaSp).FirstOrDefault();
            qlSanPham.ChiTietSanPham.MaCLR = _iQlSanPhamService.GetLstColor().Where(c => c.ColorSP == cbx_mau.Text)
                .Select(c => c.MaClr).FirstOrDefault();
            qlSanPham.ChiTietSanPham.MaSize = _iQlSanPhamService.GetLstSize().Where(c => c.SizeSp == int.Parse(cbx_size.Text))
                .Select(c => c.MaSize).FirstOrDefault();
            qlSanPham.ChiTietSanPham.GiaNhap = Convert.ToInt32(txt_gianhap.Text);
            qlSanPham.ChiTietSanPham.giaban = Convert.ToInt32(txt_giaban.Text);
            qlSanPham.ChiTietSanPham.soluong = Convert.ToInt32(txt_soluong.Text);
            qlSanPham.ChiTietSanPham.Mota = txt_tspct.Text;
            qlSanPham.ChiTietSanPham.MaChatLieu = _iQlSanPhamService.GetLstChatLieu().Where(c => c.ChatLieuSP == txt_chatLieu.Text).
                Select(c => c.MaChatLieu).FirstOrDefault();
            qlSanPham.ChiTietSanPham.MaCo = _iQlSanPhamService.GetLstLoaiCoGiay()
                .Where(c => c.LoaiCoGiaySP == cbx_coGiay.Text).Select(c => c.MaCo).FirstOrDefault();
            qlSanPham.ChiTietSanPham.GhiChu = txt_ghi.Text;
            qlSanPham.ChiTietSanPham.MaQR = txt_barCode.Text;
            qlSanPham.ChiTietSanPham.MaPB = "PB1";
            // lấy dl từ form 182-198
            qlSanPham.ChiTietSanPham.TrangThai = (cbx_trangThai.Text == "Còn hàng" ? 1 : 0);
            if (qlSanPham.ChiTietSanPham.MaCTSP != null && qlSanPham.ChiTietSanPham.MaSP != null && qlSanPham.ChiTietSanPham.MaCLR != null && qlSanPham.ChiTietSanPham.GiaNhap != null &&
                 qlSanPham.ChiTietSanPham.giaban != null && qlSanPham.ChiTietSanPham.soluong != null && qlSanPham.ChiTietSanPham.Mota != null && qlSanPham.ChiTietSanPham.MaChatLieu != null
                && qlSanPham.ChiTietSanPham.MaCo != null && qlSanPham.ChiTietSanPham.GhiChu != null && qlSanPham.ChiTietSanPham.MaQR != null && qlSanPham.ChiTietSanPham.TrangThai != null && qlSanPham.ChiTietSanPham.MaSize != null
                )
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc sẽ sử dụng chức năng trên", "Thông báo", MessageBoxButtons.YesNo); // thê
                if (dialogResult == DialogResult.Yes)
                {
                    _iQlSanPhamService.addCTSanPham(qlSanPham.ChiTietSanPham);
                    MessageBox.Show("Successful", "Thông báo");
                    load();
                }
            }
            else
            {
                MessageBox.Show("Thông tin không được để trống", "Thông báo");
            }

        }
        string masp;
        private void dgrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            int row = e.RowIndex; // lấy index
            temb = dgrid.Rows[row].Cells[8].Value.ToString();// lấy mã sp
            ChiTietSanPham1.SelectedIndex = 1;
            var tembSp = _iQlSanPhamService.GetSPAll().Where(c => c.ChiTietSanPham.MaCTSP == temb).FirstOrDefault();
            txt_danhmuc.Text = tembSp.DanhMuc.TenDanhMuc;
            txt_tsp.Text = tembSp.SanPham.TenSp;
            txt_tspct.Text = tembSp.ChiTietSanPham.Mota;
            txt_giaban.Text = (tembSp.ChiTietSanPham.giaban).ToString();
            txt_gianhap.Text = (tembSp.ChiTietSanPham.GiaNhap).ToString();
            cbx_mau.Text = tembSp.Color.ColorSP;
            cbx_size.Text = tembSp.Size.SizeSp.ToString();
            txt_ghi.Text = tembSp.ChiTietSanPham.GhiChu;
            txt_barCode.Text = tembSp.ChiTietSanPham.MaQR;
            txt_chatLieu.Text = tembSp.ChatLieu.ChatLieuSP;
            txt_soluong.Text = (tembSp.ChiTietSanPham.soluong).ToString();
            cbx_coGiay.Text = tembSp.LoaiCoGiay.LoaiCoGiaySP;
            cbx_trangThai.Text = tembSp.ChiTietSanPham.TrangThai == 1 ? "Còn hàng" : "Hết hàng";
            //var temb1 = _iQlSanPhamService.GetLstImage().Where(c => c.MaCTSP == temb).Select(c => c.Images).FirstOrDefault();
            //if (_iQlSanPhamService.GetLstImage().Where(c => c.Images == temb1) != null)
            //{
            //    lbl_anh.Text = temb1;
            //    pic.Image = Image.FromFile(lbl_anh.Text);
            //}
            lbl_anh.Text = "";
            //var img = _iQlSanPhamService.GetLstImage().Where(c => c.MaCTSP == temb).ToList();
            //lbl_anh.Text = img[0].Images;
            //pic.Image = Image.FromFile(lbl_anh.Text);
            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 2000;//2 s
            _timer.Tick += new System.EventHandler(timer1_Tick);
            _timer.Start();

        }
        int a = 0;
        private void timer1_Tick(object? sender, EventArgs e)
        {

            var img = _iQlSanPhamService.GetLstImage().Where(c => c.MaCTSP == temb).ToList();
            if (_iQlSanPhamService.GetLstImage().Where(c => c.MaCTSP == temb).FirstOrDefault() != null)
            {

                if (img.Count > a)
                {
                    lbl_anh.Text = img[a].Images;
                    pic.Image = Image.FromFile(lbl_anh.Text);


                }
                else
                {
                    a = 0;
                    lbl_anh.Text = img[a].Images;
                    pic.Image = Image.FromFile(lbl_anh.Text);
                    return;
                }
            }
            a++;
        }

        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void Frm_sanpham_Load(object sender, EventArgs e)
        {

        }

        private void btn_start_Click(object sender, EventArgs e)
        {

        }

        private void VideoCaptureDevice_name(object sender, NewFrameEventArgs eventargs)
        {

        }

        private void btn_end_Click(object sender, EventArgs e)
        {

        }



        private void Sửa_Click(object sender, EventArgs e)
        {


            foreach (var x in chitietSanPhamServices.Getlst().Where(c => c.MaCTSP == temb))
            {
                x.TrangThai = (cbx_trangThai.Text == "Còn hàng" ? 1 : 0);
                x.GhiChu = txt_ghi.Text;
                x.giaban = Convert.ToInt32(txt_giaban.Text);
                x.GiaNhap = Convert.ToInt32(txt_gianhap.Text);
                x.soluong = Convert.ToInt32(txt_soluong.Text);
                chitietSanPhamServices.update(x);
            }
            

            MessageBox.Show("successful");
            ChiTietSanPham1.SelectedIndex = 1;
            load();
        }

        private void upload_Click(object sender, EventArgs e) // up ảnh
        {
            _timer.Stop();
            lbl_anh.Text = "";
            OpenFileDialog opnfd = new OpenFileDialog();
            opnfd.Filter = "Image Files (*.jpg;*.jpeg;.*.gif;)|*.jpg;*.jpeg;.*.gif";
            if (opnfd.ShowDialog() == DialogResult.OK)
            {
                pic.Image = new Bitmap(opnfd.FileName);
                lbl_anh.Text = opnfd.FileName;
            }

        }

        private void btn_anh_Click(object sender, EventArgs e) // thêm ảnh
        {

            QLSanPham qlSanPham = new QLSanPham();
            qlSanPham.Image.TrangThai = 1;
            qlSanPham.Image.MaImage = "Img" + _iQlSanPhamService.GetLstImage().Count + 1;
            qlSanPham.Image.MaCTSP = temb;
            qlSanPham.Image.Images = lbl_anh.Text;
            _iQlSanPhamService.addImage(qlSanPham.Image);
            MessageBox.Show("successful");
            _timer.Start();
        }

        private void txt_barCode_TextChanged(object sender, EventArgs e) // bỏ
        {
            //_iQlSanPhamService.GetLstCTSanPham().Where(c => c.MaQR == txt_barCode.Text) != null;
            //MessageBox.Show(txt_barCode.Text);
            //var result = _iQlSanPhamService.GetLstCTSanPham().Where(c => c.MaQR == txt_barCode.Text)
            //    .Select(c => c.MaCTSP)
            //    .FirstOrDefault();
            try
            {
                if (_iQlSanPhamService.GetLstCTSanPham().Where(c => c.MaQR == txt_barCode.Text).Select(c => c.MaCTSP)
                    .FirstOrDefault() != null)
                {

                    var dt = _iQlSanPhamService.GetLstCTSanPham().Where(c => c.MaQR == txt_barCode.Text).Select(c => c.MaCTSP).FirstOrDefault();// tìm mactsp bằng barcode
                    var tembSp = _iQlSanPhamService.GetSPAll().Where(c => c.ChiTietSanPham.MaCTSP == dt).FirstOrDefault(); // tìm sản phẩm = mactsp
                    txt_danhmuc.Text = tembSp.DanhMuc.TenDanhMuc;
                    txt_tsp.Text = tembSp.SanPham.TenSp;
                    txt_tspct.Text = tembSp.ChiTietSanPham.Mota;
                    txt_giaban.Text = (tembSp.ChiTietSanPham.giaban).ToString();
                    txt_gianhap.Text = (tembSp.ChiTietSanPham.GiaNhap).ToString();
                    cbx_mau.Text = tembSp.Color.ColorSP;
                    cbx_size.Text = tembSp.Size.SizeSp.ToString();
                    txt_ghi.Text = tembSp.ChiTietSanPham.GhiChu;
                    txt_barCode.Text = tembSp.ChiTietSanPham.MaQR;
                    txt_chatLieu.Text = tembSp.ChatLieu.ChatLieuSP;
                    txt_soluong.Text = (tembSp.ChiTietSanPham.soluong).ToString();
                    cbx_coGiay.Text = tembSp.LoaiCoGiay.LoaiCoGiaySP;
                    cbx_trangThai.Text = tembSp.ChiTietSanPham.TrangThai == 1 ? "Còn hàng" : "Hết hàng";
                    //var temb1 = _iQlSanPhamService.GetLstImage().Where(c => c.MaCTSP == temb).Select(c => c.Images).FirstOrDefault();
                    //if (_iQlSanPhamService.GetLstImage().Where(c => c.Images == temb1) != null)
                    //{
                    //    //lbl_anh.Text = temb1;
                    //    //pic.Image = Image.FromFile(lbl_anh.Text);
                    //}
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void cbo_loadDanhMuc_TextChanged(object sender, EventArgs e)// tìm kiém
        {
            dgrid.Rows.Clear();
            if (cbo_loadDanhMuc.Text != "Tất cả")// tìm kiems danh mục
            {
                foreach (var x in _iQlSanPhamService.GetSPAll().Where(c => c.DanhMuc.TenDanhMuc == cbo_loadDanhMuc.Text))
                {
                    dgrid.Rows.Add(x.SanPham.TenSp + ' ' + x.ChiTietSanPham.Mota, x.SanPham.ThuongHieu,

                        x.DanhMuc.TenDanhMuc, x.Size.SizeSp, x.Color.ColorSP, x.ChatLieu.ChatLieuSP, x.ChiTietSanPham.giaban, x.ChiTietSanPham.TrangThai == 0 || x.ChiTietSanPham.soluong <= 0 ? "Hết hàng" : "Còn hàng", x.ChiTietSanPham.MaCTSP);
                }

            }
            else
            {
                foreach (var x in _iQlSanPhamService.GetSPAll())
                {
                    dgrid.Rows.Add(x.SanPham.TenSp + ' ' + x.ChiTietSanPham.Mota, x.SanPham.ThuongHieu,

                        x.DanhMuc.TenDanhMuc, x.Size.SizeSp, x.Color.ColorSP, x.ChatLieu.ChatLieuSP, x.ChiTietSanPham.giaban, x.ChiTietSanPham.TrangThai == 0 || x.ChiTietSanPham.soluong <= 0 ? "Hết hàng" : "Còn hàng", x.ChiTietSanPham.MaCTSP);
                }
            }
        }

        private void cbo_chatLieu_TextChanged(object sender, EventArgs e)
        {
            dgrid.Rows.Clear();
            if (cbo_chatLieu.Text == "Tất cả")// tìm kiếm chất liệu
            {
                foreach (var x in _iQlSanPhamService.GetSPAll())
                {
                    dgrid.Rows.Add(x.SanPham.TenSp + ' ' + x.ChiTietSanPham.Mota, x.SanPham.ThuongHieu,

                        x.DanhMuc.TenDanhMuc, x.Size.SizeSp, x.Color.ColorSP, x.ChatLieu.ChatLieuSP, x.ChiTietSanPham.giaban, x.ChiTietSanPham.TrangThai == 0 || x.ChiTietSanPham.soluong <= 0 ? "Hết hàng" : "Còn hàng", x.ChiTietSanPham.MaCTSP);
                }
            }
            else
            {
                foreach (var x in _iQlSanPhamService.GetSPAll().Where(c => c.ChatLieu.ChatLieuSP == cbo_chatLieu.Text))
                {
                    dgrid.Rows.Add(x.SanPham.TenSp + ' ' + x.ChiTietSanPham.Mota, x.SanPham.ThuongHieu,

                        x.DanhMuc.TenDanhMuc, x.Size.SizeSp, x.Color.ColorSP, x.ChatLieu.ChatLieuSP, x.ChiTietSanPham.giaban, x.ChiTietSanPham.TrangThai == 0 || x.ChiTietSanPham.soluong <= 0 ? "Hết hàng" : "Còn hàng", x.ChiTietSanPham.MaCTSP);
                }
            }
        }

        void Km() // them dlvaof combobox
        {
            cbo_dkm.Items.Add("Tất cả");
            cbo_dkm.Items.Add("Đang sale");
            cbo_dkm.Items.Add("Không sale");
            cbo_dkm.SelectedIndex = 0;
        }
        private void cbo_dkm_TextChanged(object sender, EventArgs e)
        {
            dgrid.Rows.Clear();
            if (cbo_dkm.Text == "Tất cả")
            {
                foreach (var x in _iQlSanPhamService.GetSPAll())
                {
                    dgrid.Rows.Add(x.SanPham.TenSp + ' ' + x.ChiTietSanPham.Mota, x.SanPham.ThuongHieu,

                        x.DanhMuc.TenDanhMuc, x.Size.SizeSp, x.Color.ColorSP, x.ChatLieu.ChatLieuSP, x.ChiTietSanPham.giaban, x.ChiTietSanPham.TrangThai == 0 || x.ChiTietSanPham.soluong <= 0 ? "Hết hàng" : "Còn hàng", x.ChiTietSanPham.MaCTSP);
                }

            }
            else if (cbo_dkm.Text == "Đang sale")
            {
                foreach (var x in _iQlSanPhamService.GetSPAll().Where(c => c.ProductBack.TrangThai == 3))
                {
                    dgrid.Rows.Add(x.SanPham.TenSp + ' ' + x.ChiTietSanPham.Mota, x.SanPham.ThuongHieu,

                        x.DanhMuc.TenDanhMuc, x.Size.SizeSp, x.Color.ColorSP, x.ChatLieu.ChatLieuSP, x.ChiTietSanPham.giaban, x.ChiTietSanPham.TrangThai == 0 || x.ChiTietSanPham.soluong <= 0 ? "Hết hàng" : "Còn hàng", x.ChiTietSanPham.MaCTSP);
                }
            }
            else if (cbo_dkm.Text == "Không sale")
            {
                foreach (var x in _iQlSanPhamService.GetSPAll().Where(c => c.ProductBack.TrangThai == 0 || c.ProductBack.TrangThai == 1))
                {
                    dgrid.Rows.Add(x.SanPham.TenSp + ' ' + x.ChiTietSanPham.Mota, x.SanPham.ThuongHieu,

                        x.DanhMuc.TenDanhMuc, x.Size.SizeSp, x.Color.ColorSP, x.ChatLieu.ChatLieuSP, x.ChiTietSanPham.giaban, x.ChiTietSanPham.TrangThai == 0 || x.ChiTietSanPham.soluong <= 0 ? "Hết hàng" : "Còn hàng", x.ChiTietSanPham.MaCTSP);
                }
            }
        }

        void giaBan()
        {
            cbo_giaBan.Items.Add("Tất cả");
            cbo_giaBan.Items.Add("Từ 0-500k");
            cbo_giaBan.Items.Add("Từ 500k-1tr");
            cbo_giaBan.Items.Add("Từ 1tr-2tr");
            cbo_giaBan.Items.Add("Từ 2tr-5tr");
            cbo_giaBan.Items.Add("Trên 5tr");
            cbo_giaBan.SelectedIndex = 0;

        }

        private void cbo_giaBan_TextChanged(object sender, EventArgs e)// tìm kiếm htoe giá
        {
            dgrid.Rows.Clear();
            if (cbo_giaBan.Text == "Tất cả")
            {
                foreach (var x in _iQlSanPhamService.GetSPAll())
                {
                    dgrid.Rows.Add(x.SanPham.TenSp + ' ' + x.ChiTietSanPham.Mota, x.SanPham.ThuongHieu,

                        x.DanhMuc.TenDanhMuc, x.Size.SizeSp, x.Color.ColorSP, x.ChatLieu.ChatLieuSP, x.ChiTietSanPham.giaban, x.ChiTietSanPham.TrangThai == 0 || x.ChiTietSanPham.soluong <= 0 ? "Hết hàng" : "Còn hàng", x.ChiTietSanPham.MaCTSP);
                }

            }
            else if (cbo_giaBan.Text == "Từ 0-500k")
            {
                foreach (var x in _iQlSanPhamService.GetSPAll().Where(c => c.ChiTietSanPham.giaban > 0 && c.ChiTietSanPham.giaban <= 500000))
                {
                    dgrid.Rows.Add(x.SanPham.TenSp + ' ' + x.ChiTietSanPham.Mota, x.SanPham.ThuongHieu,

                        x.DanhMuc.TenDanhMuc, x.Size.SizeSp, x.Color.ColorSP, x.ChatLieu.ChatLieuSP, x.ChiTietSanPham.giaban, x.ChiTietSanPham.TrangThai == 0 || x.ChiTietSanPham.soluong <= 0 ? "Hết hàng" : "Còn hàng", x.ChiTietSanPham.MaCTSP);
                }
            }
            else if (cbo_giaBan.Text == "500k-1tr")
            {
                foreach (var x in _iQlSanPhamService.GetSPAll().Where(c => c.ChiTietSanPham.giaban > 500000 && c.ChiTietSanPham.giaban <= 1000000))
                {
                    dgrid.Rows.Add(x.SanPham.TenSp + ' ' + x.ChiTietSanPham.Mota, x.SanPham.ThuongHieu,

                        x.DanhMuc.TenDanhMuc, x.Size.SizeSp, x.Color.ColorSP, x.ChatLieu.ChatLieuSP, x.ChiTietSanPham.giaban, x.ChiTietSanPham.TrangThai == 0 || x.ChiTietSanPham.soluong <= 0 ? "Hết hàng" : "Còn hàng", x.ChiTietSanPham.MaCTSP);
                }
            }
            else if (cbo_giaBan.Text == "Từ 1tr-2tr")
            {
                foreach (var x in _iQlSanPhamService.GetSPAll().Where(c => c.ChiTietSanPham.giaban > 1000000 && c.ChiTietSanPham.giaban <= 2000000))
                {
                    dgrid.Rows.Add(x.SanPham.TenSp + ' ' + x.ChiTietSanPham.Mota, x.SanPham.ThuongHieu,

                        x.DanhMuc.TenDanhMuc, x.Size.SizeSp, x.Color.ColorSP, x.ChatLieu.ChatLieuSP, x.ChiTietSanPham.giaban, x.ChiTietSanPham.TrangThai == 0 || x.ChiTietSanPham.soluong <= 0 ? "Hết hàng" : "Còn hàng", x.ChiTietSanPham.MaCTSP);
                }
            }
            else if (cbo_giaBan.Text == "Từ 2tr-5tr")
            {
                foreach (var x in _iQlSanPhamService.GetSPAll().Where(c => c.ChiTietSanPham.giaban > 2000000 && c.ChiTietSanPham.giaban <= 5000000))
                {
                    dgrid.Rows.Add(x.SanPham.TenSp + ' ' + x.ChiTietSanPham.Mota, x.SanPham.ThuongHieu,

                        x.DanhMuc.TenDanhMuc, x.Size.SizeSp, x.Color.ColorSP, x.ChatLieu.ChatLieuSP, x.ChiTietSanPham.giaban, x.ChiTietSanPham.TrangThai == 0 || x.ChiTietSanPham.soluong <= 0 ? "Hết hàng" : "Còn hàng", x.ChiTietSanPham.MaCTSP);
                }
            }
            else if (cbo_giaBan.Text == "Trên 5tr")
            {
                foreach (var x in _iQlSanPhamService.GetSPAll().Where(c => c.ChiTietSanPham.giaban > 5000000))
                {
                    dgrid.Rows.Add(x.SanPham.TenSp + ' ' + x.ChiTietSanPham.Mota, x.SanPham.ThuongHieu,

                        x.DanhMuc.TenDanhMuc, x.Size.SizeSp, x.Color.ColorSP, x.ChatLieu.ChatLieuSP, x.ChiTietSanPham.giaban, x.ChiTietSanPham.TrangThai == 0 || x.ChiTietSanPham.soluong <= 0 ? "Hết hàng" : "Còn hàng", x.ChiTietSanPham.MaCTSP);
                }
            }


        }

        private void txt_giaban_Leave(object sender, EventArgs e) // check chữ
        {
            Regex regex = new Regex("[a-z]");
            if (regex.IsMatch(txt_giaban.Text))
            {
                MessageBox.Show("Giá bán không thể nhập chữ", " Thông báo");
                txt_giaban.Text = "";
            }
        }

        private void txt_gianhap_Leave(object sender, EventArgs e)
        {
            Regex regex = new Regex("[a-z]");
            if (regex.IsMatch(txt_gianhap.Text))
            {
                MessageBox.Show("Giá bán không thể nhập chữ", " Thông báo");
                txt_gianhap.Text = "";
            }
        }

        private void txt_soluong_Leave(object sender, EventArgs e)
        {
            Regex regex = new Regex("[a-z]");
            if (regex.IsMatch(txt_soluong.Text))
            {
                MessageBox.Show("Giá bán không thể nhập chữ", " Thông báo");
                txt_soluong.Text = "";
            }
        }

        private void cbx_size_Leave(object sender, EventArgs e)
        {
            Regex regex = new Regex("[a-z]");
            if (regex.IsMatch(cbx_size.Text))
            {
                MessageBox.Show("Giá bán không thể nhập chữ", " Thông báo");
                cbx_size.Text = "";
            }
        }

        private void btn_xóa_Click(object sender, EventArgs e)// xóa sp 
        {
            ChitietSanPhamServices chitietSanPhamServices1 = new ChitietSanPhamServices();
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc sẽ sử dụng chức năng trên", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)// ko xóa trong sql
            {

                foreach (var x in chitietSanPhamServices1.Getlst().Where(c => c.MaCTSP == temb))
                {
                    x.TrangThai = 0;
                    chitietSanPhamServices1.update(x);
                }


                MessageBox.Show("Successful", "Thông báo");
                load();
            }



        }

        private void lbl_addDanhMuc_Click(object sender, EventArgs e)
        {
            if ((_iQlSanPhamService.GetLstDanhMuc().Where(c => c.TenDanhMuc == txt_danhmuc.Text).FirstOrDefault() == null))
            {
                QLSanPham qLSanPham = new QLSanPham();
                qLSanPham.DanhMuc.TenDanhMuc = txt_danhmuc.Text;
                qLSanPham.DanhMuc.MaDanhMuc = "DM" + (_iQlSanPhamService.GetLstDanhMuc().Count + 1);
                qLSanPham.DanhMuc.GhiChu = "";
                qLSanPham.DanhMuc.TrangThai = 1;
                _iQlSanPhamService.addDanhMuc(qLSanPham.DanhMuc);
                loaddanhMuc();
            }
            else
            {
                MessageBox.Show("Danh mục đã tồn tại", "Thông báo");
            }
        }

        private void lbl_addtsp_Click(object sender, EventArgs e)
        {
            //QLSanPham qLSanPham = new QLSanPham();
            //qLSanPham.SanPham.TenSp = txt_tsp.Text;
            //qLSanPham.SanPham.TenSp = txt_tsp.Text;
        }

        private void lbl_addSize_Click(object sender, EventArgs e)
        {
            if (_iQlSanPhamService.GetLstSize().Where(c => c.SizeSp == Convert.ToInt32(cbx_size.Text)).FirstOrDefault() == null)
            {
                QLSanPham qLSanPham = new QLSanPham();
                qLSanPham.Size.MaSize = "Sz" + (_iQlSanPhamService.GetLstSize().Count + 1);
                qLSanPham.Size.SizeSp = Convert.ToInt32(cbx_size.Text);
                qLSanPham.Size.ChieuDaiChan = Convert.ToInt32(cbx_size.Text) / 2 + 1;
                qLSanPham.Size.TrangThai = 1;
                _iQlSanPhamService.addSize(qLSanPham.Size);
                loadSize();
            }
            else
            {
                MessageBox.Show("Size đã tồn tại", "Thông báo");
            }
        }

        private void lbl_addColor_Click(object sender, EventArgs e)
        {
            if (_iQlSanPhamService.GetLstColor().Where(c => c.ColorSP == cbx_mau.Text).FirstOrDefault() == null)
            {
                QLSanPham qLSanPham = new QLSanPham();
                qLSanPham.Color.MaClr = "Clr" + (_iQlSanPhamService.GetLstColor().Count + 1);
                qLSanPham.Color.ColorSP = cbx_mau.Text;
                qLSanPham.Color.TrangThai = 1;
                _iQlSanPhamService.addColor(qLSanPham.Color);
                loadColor();
            }
            else
            {
                MessageBox.Show("Màu đã tồn tại", "Thông báo");
            }
        }

        private void lbl_addChatLieu_Click(object sender, EventArgs e)
        {
            if (_iQlSanPhamService.GetLstChatLieu().Where(c => c.ChatLieuSP == txt_chatLieu.Text).FirstOrDefault() == null)
            {
                QLSanPham qLSanPham = new QLSanPham();
                qLSanPham.ChatLieu.MaChatLieu = "ChL" + (_iQlSanPhamService.GetLstChatLieu().Count + 1);
                qLSanPham.ChatLieu.ChatLieuSP = txt_chatLieu.Text;
                qLSanPham.ChatLieu.TrangThai = 1;
                _iQlSanPhamService.addChatLieu(qLSanPham.ChatLieu);
                loadChatLieu();
            }
            else
            {
                MessageBox.Show("Chất liệu đã tồn tại", "Thông báo");
            }
        }

        private void txt_danhmuc_TextChanged(object sender, EventArgs e) // tìm sp theo danh muc
        {
            try
            {
                var a = _iQlSanPhamService.GetLstDanhMuc().Where(c => c.TenDanhMuc == txt_danhmuc.Text).Select(c => c.MaDanhMuc).FirstOrDefault();
                var lstdm = _iQlSanPhamService.GetLstSP().Where(c => c.MaDanhMuc == a).Select(c => c.TenSp).ToList();

                if (lstdm.Count != 0)
                {
                    txt_tsp.Items.Clear();
                    txt_tsp.ResetText();
                    foreach (var x in lstdm)
                    {
                        txt_tsp.Items.Add(x);
                    }

                    txt_tsp.SelectedIndex = 0;
                }
                else
                {
                    txt_tsp.Items.Clear();
                    txt_tsp.ResetText();
                }
            }
            catch (Exception)
            {

                //MessageBox.Show("Lỗi hệ thống");
            }
        }

        private void dgrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
