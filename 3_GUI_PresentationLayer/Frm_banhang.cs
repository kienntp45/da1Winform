using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _2_BUS_BusinessLayer.IService;
using _2_BUS_BusinessLayer.Service;
using AForge.Video.DirectShow;
using ZXing;
using _1_DAL_DataAccessLayer.Models;
using _2_BUS_BusinessLayer.Models;
using Microsoft.VisualBasic;
using System.Threading;
using System.Drawing.Drawing2D;
using _2_BUS_BusinessLayer.Utilities;
using System.Globalization;

namespace _3_GUI_PresentationLayer
{
    public partial class Frm_banhang : Form
    {
        private IBanhangService _banhangService; // khởi tạo 26-56
        private FilterInfoCollection filterInfoCollection;
        private VideoCaptureDevice videoCaptureDevice;
        KhachHang khachHang;
        HoaDon hoaDon;
        ChiTietSanPham chiTietSanPham;
        HoaDonChiTiet hoaDonChiTiet;
        NhanVien nhanVien;
        Voucher voucher;
        Form _f;
        Validateform _Validateform;

        private IQlSanPhamService _qlSanPhamService;

        public Frm_banhang()
        {
            InitializeComponent();
            _banhangService = new BanhangService();
            khachHang = new KhachHang();
            hoaDon = new HoaDon();
            hoaDonChiTiet = new HoaDonChiTiet();
            chiTietSanPham = new ChiTietSanPham();
            nhanVien = new NhanVien();
            voucher = new Voucher();
            _qlSanPhamService = new QlSanPhamService();
            _Validateform = new Validateform();
            btn_thanhtoan.ForeColor = System.Drawing.Color.Black;
            btn_thanhtoan.BackColor = System.Drawing.Color.WhiteSmoke;
            loadSpbanhang();

            loadhdchuathanhthoan();
        }


        private void Frm_banhang_Load(object sender, EventArgs e) // bỏ
        {


            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo x in filterInfoCollection)
            {

            }


            btn_thanhtoan.Enabled = false;

        }


        void loadHoadon(string mahd) // load lên hóa đơn
        {
            dtgview_hoadon.ColumnCount = 6; // set up 6 cột
            dtgview_hoadon.Columns[0].HeaderText = "Sản phẩm";
            dtgview_hoadon.Columns[1].HeaderText = "Số lượng";
            dtgview_hoadon.Columns[1].Name = "tbx_soluong";
            dtgview_hoadon.Columns[2].HeaderText = "Đơn giá";
            dtgview_hoadon.Columns[3].HeaderText = "Giảm giá";
            dtgview_hoadon.Columns[4].HeaderText = "Thành tiền";
            dtgview_hoadon.Columns[5].HeaderText = "Barcaode";
            dtgview_hoadon.Columns[5].Visible = false; // Visible ẩn cột 
            DataGridViewButtonColumn dtdelete = new DataGridViewButtonColumn(); // tạo nit button trong data 87-98
            dtdelete.Name = "dt_btn_delete";
            dtdelete.HeaderText = "";
            dtdelete.Text = "remove";


            dtdelete.UseColumnTextForButtonValue = true;
            int indexdelete = 6;
            if (dtgview_hoadon.Columns["dt_btn_delete"] == null)
            {
                dtgview_hoadon.Columns.Insert(indexdelete, dtdelete);
            }
            dtgview_hoadon.Rows.Clear(); // xóa dữ liệu trong data
            foreach (var x in _banhangService.viewHoadons().Where(c => c.hoaDon.MaHd == mahd)) // đẩy dữ liệu mới vào data 100-106
            {
                string makm = _qlSanPhamService.GetSPAll().Where(c => c.ChiTietGiamGia.MaDanhMuc == x.sanPham.MaDanhMuc).Select(c => c.ChiTietGiamGia.MaKM).FirstOrDefault();

                dtgview_hoadon.Rows.Add(x.sanPham.TenSp + " " + x.chiTietSanPham.Mota, x.hoaDonChiTiet.soluong, x.hoaDonChiTiet.DonGia,/*_qlSanPhamService.GetSPAll().Where(c=>c.KhuyenMai.MaKM==makm).Select(c=>c.KhuyenMai.GiamGia).FirstOrDefault()+ */" %", x.hoaDonChiTiet.Thanhtien, x.chiTietSanPham.MaQR);

            }

        }




        void loadSpbanhang() // load sản phẩm
        {
            dtgview_thongtinsp.ColumnCount = 8; //  setup 8 cột
            dtgview_thongtinsp.Columns[0].HeaderText = "Mã";
            dtgview_thongtinsp.Columns[1].HeaderText = "Tên giày";
            dtgview_thongtinsp.Columns[2].HeaderText = "Thương hiệu";
            dtgview_thongtinsp.Columns[3].HeaderText = "Size";
            dtgview_thongtinsp.Columns[4].HeaderText = "Màu sắc";
            dtgview_thongtinsp.Columns[5].HeaderText = "Chất liệu";
            dtgview_thongtinsp.Columns[6].HeaderText = "Loại cổ giày";
            dtgview_thongtinsp.Columns[7].HeaderText = "Số lượng";

            dtgview_thongtinsp.Rows.Clear(); // xóa hết dl trong data
            foreach (var x in _qlSanPhamService.GetSPAll().Where(c=>c.ChiTietSanPham.soluong>0)) // đẩy dl vào bảng data 
            {
                dtgview_thongtinsp.Rows.Add(x.ChiTietSanPham.MaQR, x.SanPham.TenSp + " " + x.ChiTietSanPham.Mota, x.SanPham.ThuongHieu, x.Size.SizeSp, x.Color.ColorSP, x.ChatLieu.ChatLieuSP, x.LoaiCoGiay.LoaiCoGiaySP, x.ChiTietSanPham.soluong);
            }
        }
        void loadSpbanhang(string tk) // load tìm kiếm sản phẩm
        {
            dtgview_thongtinsp.ColumnCount = 8;
            dtgview_thongtinsp.Columns[0].HeaderText = "Mã";
            dtgview_thongtinsp.Columns[1].HeaderText = "Tên giày";
            dtgview_thongtinsp.Columns[2].HeaderText = "Thương hiệu";
            dtgview_thongtinsp.Columns[3].HeaderText = "Size";
            dtgview_thongtinsp.Columns[4].HeaderText = "Màu sắc";
            dtgview_thongtinsp.Columns[5].HeaderText = "Chất liệu";
            dtgview_thongtinsp.Columns[6].HeaderText = "Loại cổ giày";
            dtgview_thongtinsp.Columns[7].HeaderText = "Số lượng";

            dtgview_thongtinsp.Rows.Clear();
            foreach (var x in _qlSanPhamService.GetSPAll().Where(c => c.SanPham.TenSp.ToLower().StartsWith(tk)).ToList()) // tìm kiếm sp theo tên gần đúng,  trong sql rồi đẩy vào dtaa
            {
                dtgview_thongtinsp.Rows.Add(x.ChiTietSanPham.MaQR, x.SanPham.TenSp + " " + x.ChiTietSanPham.Mota, x.SanPham.ThuongHieu, x.Size.SizeSp, x.Color.ColorSP, x.ChatLieu.ChatLieuSP, x.LoaiCoGiay.LoaiCoGiaySP, x.ChiTietSanPham.soluong);
            }
        }


        private void Frm_banhang_FormClosed(object sender, FormClosedEventArgs e) // barcode bỏ
        {
            if (videoCaptureDevice != null)
            {
                if (videoCaptureDevice.IsRunning)
                {

                    videoCaptureDevice.SignalToStop();
                    Thread.Sleep(1000);
                }
            }

        }

        string barcode;

        private void dtgview_thongtinsp_CellClick(object sender, DataGridViewCellEventArgs e) // chọn sản phẩm 
        {
            int colum = e.ColumnIndex; // lấy inde của sp mk vừa nhấn

            if (lbl_mahoadon.Text == "")
            {
                MessageBox.Show("Bạn chưa có hóa đơn để thêm sản phẩm !", "Thông báo");
                return;
            }
            int RowIndex = e.RowIndex;
            if (RowIndex == _qlSanPhamService.GetSPAll().Count || RowIndex == -1) return;// ko lấy dc index sp thiwf dừng
            barcode = dtgview_thongtinsp.Rows[RowIndex].Cells[0].Value.ToString();
            if (e.ColumnIndex == dtgview_thongtinsp.Columns[colum].Index)
            {

                _f = new Form();
                _f.Text = "Nhập số lượng";
                TextBox textBox = new TextBox();
                textBox.Size = new System.Drawing.Size(284, 39);
                textBox.Location = (new Point(370, 54));
                Button button = new Button();
                Label label = new Label();
                label.Location = (new Point(42, 54));

                label.Size = new System.Drawing.Size(264, 32);
                button.Text = "OK";
                button.Size = new System.Drawing.Size(150, 46);
                button.Location = (new Point(100, 200));
                _f.Controls.Add(textBox);
                _f.Controls.Add(button);
                _f.Controls.Add(label);
                _f.Controls[2].Left = 10;
                _f.Controls[2].Top = 13;
                _f.Controls[0].Left = 80;
                _f.Controls[1].Left = 100;
                _f.Controls[0].Top = 10;
                _f.Controls[1].Top = 50;
                _f.Size = new System.Drawing.Size(700, 200);
                _f.StartPosition = FormStartPosition.CenterScreen;
                _f.ControlBox = false;
                button.Click += Button_Click;
                _f.ShowDialog();

            }



        }
        int _soluong;
        private void Button_Click(object sender, EventArgs e) // nhập số lượng
        {



            var giaban = _qlSanPhamService.GetSPAll().Where(c => c.ChiTietSanPham.MaQR == barcode).Select(c => c.ChiTietSanPham.giaban).FirstOrDefault();
            var sl = _qlSanPhamService.GetSPAll().Where(c => c.ChiTietSanPham.MaQR == barcode).Select(c => c.ChiTietSanPham.soluong).FirstOrDefault();
            var ctsp = _qlSanPhamService.GetSPAll().Where(c => c.ChiTietSanPham.MaQR == barcode).Select(c => c.ChiTietSanPham.MaCTSP).FirstOrDefault();
            var hdct = _banhangService.viewHoadons().Where(c => c.hoaDonChiTiet.MaHd == lbl_mahoadon.Text && c.chiTietSanPham.MaQR == barcode).Select(c => c.chiTietSanPham.MaCTSP).FirstOrDefault();
            if (sl <= 0)
            {
                MessageBox.Show("Sản phẩm đã hết", "Thông báo");
                return;
            }
            if (_f.Controls[0].Text == "")
            {
                MessageBox.Show("Yêu cầu bạn nhập số lượng");
                return;
            }
            if (Convert.ToInt32(_f.Controls[0].Text) <= 0)
            {
                MessageBox.Show("Yêu cầu bạn nhập số lớn hơn 0");
                return;
            }
            if (_Validateform.checkSo(_f.Controls[0].Text))
            {

                MessageBox.Show("Yêu cầu bạn nhập số !");
                return;
            }
            _soluong = Convert.ToInt32(_f.Controls[0].Text);
            var updatects = _banhangService.loadspct().FirstOrDefault(c => c.MaCTSP == ctsp);
            if (hdct == ctsp)
            {

                MessageBox.Show("Bạn đã chọn sản phẩm");
                _f.Close();
                return;
            }
            Button button = sender as Button;


            hoaDonChiTiet.MaHd = lbl_mahoadon.Text; // lưu sp vào hd chi tiết
            hoaDonChiTiet.MaCTSP = ctsp;
            hoaDonChiTiet.TrangThai = 1;
            hoaDonChiTiet.soluong = _soluong;
            hoaDonChiTiet.DonGia = giaban;
            hoaDonChiTiet.Thanhtien = giaban * hoaDonChiTiet.soluong;
            updatects.soluong = sl - _soluong;
            _banhangService.addHoadonchitiet(hoaDonChiTiet);
            _banhangService.updatectsp(updatects);
            tongtien();
            loadHoadon(lbl_mahoadon.Text);
            loadSpbanhang();
            _f.Close();
        }


        int hoadont = 50;
        int so = 1;
        Button button;
        string _Makhachhang;
        private void btn_themhoadon_Click(object sender, EventArgs e) // tạo hóa đơn chờ
        {
            if (MessageBox.Show("Bạn có muốn tạo hóa đơn ?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                btn_thanhtoan.BackColor = System.Drawing.Color.WhiteSmoke;
                button = new Button();
                button.Text = "HD" + _banhangService.loadhd().Count() + 10;
                this.pnl_newhoadon.Controls.Add(button);
                button.Width = 120;// thiết kế cho nút
                button.Height = 50;
                button.ForeColor = System.Drawing.Color.White;
                button.Dock = DockStyle.Top;
                button.Location = new Point(0, hoadont);
                hoadont += 50;
                so += 1;
                button.Click += new EventHandler(this.buttun_click);
                khachHang.MaKh = "KH" + _banhangService.loadkh().Count() + 10;
                _Makhachhang = khachHang.MaKh;
                khachHang.TenKh = tbx_tenkh.Text;
                khachHang.Sdt = tbx_sdtkh.Text;
                hoaDon.MaHd = "HD" + _banhangService.loadhd().Count() + 10;
                hoaDon.MaNV = Frm_Login.name;
                hoaDon.MaKH = khachHang.MaKh;
                hoaDon.thoigian = DateTime.Now;
                hoaDon.TrangThaiHd = 0;
                tbx_date.Text = (hoaDon.thoigian).ToString();
                MessageBox.Show(_banhangService.addhoadon(hoaDon, khachHang), "Thông báo");// thêm hd vào sql
                lbl_mahoadon.Text = hoaDon.MaHd; // hiển thị hóa đơn lên lable
                loadHoadon(lbl_mahoadon.Text);
            }
        }

        void buttun_click(object sender, EventArgs e) // sự kiện ấn vào hóa đơn chờ
        {

            Button button = sender as Button;
            loadHoadon(button.Text);
            foreach (var x in pnl_newhoadon.Controls)
            {
                button.BackColor = System.Drawing.Color.FromArgb(39, 39, 58);
            }
            lbl_mahoadon.Text = button.Text;
            tbx_magiamgia.Text = null;
            _Makhachhang = _banhangService.viewHoadons().Where(c => c.hoaDon.MaHd == lbl_mahoadon.Text).Select(c => c.hoaDon.MaKH).FirstOrDefault();
            var tenkh = _banhangService.loadkh().Where(c => c.MaKh == _Makhachhang).Select(c => c.TenKh).FirstOrDefault();
            var sdt = _banhangService.loadkh().Where(c => c.MaKh == _Makhachhang).Select(c => c.Sdt).FirstOrDefault();
            var diachi = _banhangService.loadkh().Where(c => c.MaKh == _Makhachhang).Select(c => c.Diachi).FirstOrDefault();
            tbx_tenkh.Text = tenkh;
            tbx_sdtkh.Text = sdt;
            tbx_diachi.Text = diachi;
            tongtien();

        }
        void loadhdchuathanhthoan() // load hóa đơn chờ 
        {
            foreach (var x in _banhangService.loadhd().Where(c => c.TrangThaiHd == 0).Select(c => c.MaHd))
            {
                button = new Button();
                button.Text = x;
                this.pnl_newhoadon.Controls.Add(button);
                button.Width = 120;
                button.Height = 50;
                button.ForeColor = System.Drawing.Color.White;
                button.Dock = DockStyle.Top;
                button.Location = new Point(0, hoadont);
                hoadont += 50;
                so += 1;
                button.Click += new EventHandler(this.buttun_click);
            }
        }



        private void btn_thanhtoan_Click(object sender, EventArgs e) // nút thanh toán
        {

            if (MessageBox.Show("Bạn có muốn thanh toán ?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)// hiển thị thông báo
            {
                if (Convert.ToDouble(tbx_tienthua.Text) < 0) return;//chưa trả đủ tiền thì ko thực hiện thnah toán
                dtgview_hoadon.Rows.Clear();
                if (MessageBox.Show("Bạn có muốn in hóa đơn không ?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    printPreviewDialog1.Document = printDocument1;
                    printPreviewDialog1.ShowDialog();
                }
                var hd = _banhangService.loadhd().FirstOrDefault(c => c.MaHd == lbl_mahoadon.Text);
                hd.TrangThaiHd = 1;// chuyển trạng thái từ chưa thanh toán sang thanh toán r
                hd.GhiChu = tbx_ghichu.Text;
                _banhangService.updatehoadon(hd);
                tbx_date.Text = null; // clear mấy ô text
                tbx_tongtien.Text = null;
                tbx_khachtra.Text = null;
                tbx_tienthua.Text = null;
                tbx_ghichu.Text = null;
                tbx_magiamgia.Text = null;
                tbx_giamgia.Text = null;
                tbx_tenkh.Text = null;
                tbx_sdtkh.Text = null;
                lbl_mahoadon.Text = null;
                button.Visible = false;

                loadhdchuathanhthoan();



            }

        }

        void thanhtoan()
        {

        }


        private void tongtien() // tính tổng tiền
        {
            var hd = _banhangService.loadhd().FirstOrDefault(c => c.MaHd == lbl_mahoadon.Text);
            var tongtien = _banhangService.viewHoadons().Where(c => c.hoaDon.MaHd == lbl_mahoadon.Text).Sum(c => c.hoaDonChiTiet.Thanhtien); // tính tổng tiền
            var menhgia = _banhangService.loadVoucher().Where(c => c.Vouchers == tbx_magiamgia.Text).Select(c => c.MenhGia).FirstOrDefault(); // tìm giá của cái vourch
            hd.Tongtien = tongtien - menhgia; // từ tiền 
            _banhangService.updatehoadon(hd);
            tbx_tongtien.Text = (hd.Tongtien).ToString();
            tbx_giamgia.Text = (menhgia).ToString();
            tbx_tongtien.Text = String.Format("{0:#,##0.##}", int.Parse(tbx_tongtien.Text)); // 0 format lại định dạng số
            tbx_giamgia.Text = String.Format("{0:#,##0.##}", int.Parse(tbx_giamgia.Text)); // 0
            tbx_tongtiendathang.Text = tbx_tongtien.Text;

        }


        private void dtgview_hoadon_CellContentClick(object sender, DataGridViewCellEventArgs e) // xóa  sản phẩm ở hóa đơn
        {
            try
            {
                var column = e.ColumnIndex;
                var Row = e.RowIndex; // lấy index của dòng click
                if (Row == _banhangService.viewHoadons().Count || Row == -1) return;

                string mahd = lbl_mahoadon.Text;
                string qr = dtgview_hoadon.Rows[Row].Cells[5].Value.ToString();
                string soluong = dtgview_hoadon.Rows[Row].Cells[1].Value.ToString();
                var mactsp = _banhangService.viewHoadons().Where(c => c.chiTietSanPham.MaQR == qr).Select(c => c.chiTietSanPham.MaCTSP).FirstOrDefault();
                var soluongctsp = _banhangService.loadspct().FirstOrDefault(c => c.MaCTSP == mactsp);

                if (column == dtgview_hoadon.Columns["dt_btn_delete"].Index) // xác nhận xóa
                {
                    if (MessageBox.Show("Bạn có chắc muốn xóa không ?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        soluongctsp.soluong = soluongctsp.soluong + int.Parse(soluong); // xóa sản phẩm bên trong hóa đơn
                        _banhangService.deletedhoadonchitiet(mahd, mactsp);
                        _banhangService.updatectsp(soluongctsp);
                        loadHoadon(lbl_mahoadon.Text);
                        loadSpbanhang();
                        tongtien();
                    }
                }
            }
            catch (Exception)
            {

                return;
            }

        }

        private void tbx_khachtra_TextChanged(object sender, EventArgs e) // nhập số lượng tiền khách trả 
        {


            if (tbx_khachtra.Text == "" || tbx_tongtien.Text == "")
            {
                btn_thanhtoan.BackColor = System.Drawing.Color.WhiteSmoke;
                btn_thanhtoan.Enabled = false;
                return;
            }
            if (_Validateform.checkSo(tbx_khachtra.Text))
            {
                btn_thanhtoan.BackColor = System.Drawing.Color.WhiteSmoke;
                btn_thanhtoan.Enabled = false;
                return;
            }
            else
            {

                btn_thanhtoan.Enabled = true;
            }
            if ((Convert.ToDouble(tbx_khachtra.Text) - Convert.ToDouble(tbx_tongtien.Text)) > 0)
            {
                btn_thanhtoan.BackColor = System.Drawing.Color.Green;
            }

            var hd = _banhangService.loadhd().FirstOrDefault(c => c.MaHd == lbl_mahoadon.Text);
            tbx_tienthua.Text = (Convert.ToDouble(tbx_khachtra.Text) - Convert.ToDouble(tbx_tongtien.Text)).ToString();
            tbx_tienthua.Text = String.Format("{0:#,##0.##}", Convert.ToDouble(tbx_tienthua.Text)); // 0
            hd.TienNhan = Convert.ToDouble(tbx_khachtra.Text);
            _banhangService.updatehoadon(hd);

        }
        int xuong = 0;

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Hóa đơn", new Font("Arial", 20, FontStyle.Bold), Brushes.Black, new Point(400, 10));
            e.Graphics.DrawString("Mã hóa đơn " + lbl_mahoadon.Text, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(100, 40));
            e.Graphics.DrawString("____________________________________________________________________________________________________________________________________", new Font("Arial", 20, FontStyle.Bold), Brushes.Black, new Point(0, 40));
            e.Graphics.DrawString((DateTime.Now).ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(700, 40));
            e.Graphics.DrawString("Sản phẩm", new Font("Arial", 15, FontStyle.Regular), Brushes.Black, new Point(50, 90));
            e.Graphics.DrawString("Đơn giá", new Font("Arial", 15, FontStyle.Regular), Brushes.Black, new Point(200, 90));
            e.Graphics.DrawString("Số lượng", new Font("Arial", 15, FontStyle.Regular), Brushes.Black, new Point(350, 90));
            e.Graphics.DrawString("Thành tiền", new Font("Arial", 15, FontStyle.Regular), Brushes.Black, new Point(500, 90));
            foreach (var x in _banhangService.viewHoadons().Where(c => c.hoaDon.MaHd == lbl_mahoadon.Text))
            {


                e.Graphics.DrawString(x.sanPham.TenSp, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(50, xuong + 140));
                e.Graphics.DrawString((x.hoaDonChiTiet.DonGia).ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(200, xuong + 140));
                e.Graphics.DrawString((x.hoaDonChiTiet.soluong).ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(350, xuong + 140));
                e.Graphics.DrawString((x.hoaDonChiTiet.Thanhtien).ToString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(500, xuong + 140));
                xuong += 50;
            }
            e.Graphics.DrawString("Tổng tiền : ", new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(400, 140 + xuong));
            e.Graphics.DrawString(tbx_tongtien.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(600, 140 + xuong));
            e.Graphics.DrawString("Khách trả : ", new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(400, 190 + xuong));
            e.Graphics.DrawString(tbx_khachtra.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(600, 190 + xuong));
            e.Graphics.DrawString("Tiền thừa :", new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(400, 240 + xuong));
            e.Graphics.DrawString(tbx_tienthua.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, new Point(600, 240 + xuong));


        }



        private void btn_timgiamgia_Click(object sender, EventArgs e) // tìm mã giảm giá 
        {
            if (MessageBox.Show("Bạn muốn tìm có phải không ?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                if (lbl_mahoadon.Text == "")
                {
                    MessageBox.Show("Bạn chưa có hóa đơn để thêm sản phẩm !", "Thông báo");
                    return;
                }
                var mavoucher = _banhangService.loadVoucher().Where(c => c.Vouchers == tbx_magiamgia.Text).Select(c => c.MaVouCher).FirstOrDefault();
                var menhgia = _banhangService.loadVoucher().Where(c => c.Vouchers == tbx_magiamgia.Text).Select(c => c.MenhGia).FirstOrDefault();
                var hd = _banhangService.loadhd().FirstOrDefault(c => c.MaHd == lbl_mahoadon.Text);
                hd.MaVouCher = mavoucher;
                var vouchersoluong = _banhangService.loadVoucher().Where(c => c.MaVouCher == mavoucher).Select(c => c.SoLuong).FirstOrDefault();// tìm mã gg
                var voucher = _banhangService.loadVoucher().FirstOrDefault(C => C.MaVouCher == mavoucher);
                if (vouchersoluong == 0 || mavoucher == null)
                {
                    MessageBox.Show("Mã voucher đã hết hoặc không tồn tại", "Thông báo");
                    return;
                }
                foreach (var x in _banhangService.loadVoucher().Where(c => c.MaVouCher == mavoucher))
                {
                    if (x.NSD > DateTime.Now || x.HSD < DateTime.Now || x.TrangThai == 0)
                    {
                        MessageBox.Show("Mã voucher chưa hoạt động hoặc đã hết hạn");
                        return;
                    }
                }
                vouchersoluong -= 1; // trừ sl có thể dùng
                voucher.SoLuong = vouchersoluong;
                _banhangService.updatehoadon(hd);
                _banhangService.updatevoucher(voucher);
                tongtien();

            }
        }
        int _soluonghientai;
        private void dtgview_hoadon_CellClick(object sender, DataGridViewCellEventArgs e) // lấy số lượng sp vùa click
        {
            int RowIndex = e.RowIndex;
            int colum = e.ColumnIndex;
            if (RowIndex == _banhangService.viewHoadons().Count(c => c.hoaDonChiTiet.MaHd == lbl_mahoadon.Text) || RowIndex == -1) return;
            if (_Validateform.checkSo(dtgview_hoadon.Rows[RowIndex].Cells[1].Value.ToString())) return;

            _soluonghientai = int.Parse(dtgview_hoadon.Rows[RowIndex].Cells[1].Value.ToString());
        }
        private void dtgview_hoadon_CellValueChanged(object sender, DataGridViewCellEventArgs e) // thay đổi số lượng trong data 
        {
            var column = e.ColumnIndex;
            var Row = e.RowIndex;
            if (Row == _banhangService.viewHoadons().Count || Row == -1) return;
            if (_Validateform.checkSo(dtgview_hoadon.Rows[Row].Cells[column].Value.ToString()))
            {
                MessageBox.Show("Bạn phải nhập số để dũ liệu có thể sử lý", "Thông báo");
                return;
            }
            var barcode = dtgview_hoadon.Rows[Row].Cells[5].Value.ToString();
            var ma_ctsp = _banhangService.loadspct().Where(c => c.MaQR == barcode).Select(c => c.MaCTSP).FirstOrDefault();
            var hoadonct = _banhangService.loadhdct().FirstOrDefault(c => c.MaHd == lbl_mahoadon.Text && c.MaCTSP == ma_ctsp);
            var ctsp = _banhangService.loadspct().FirstOrDefault(c => c.MaQR == barcode);
            hoadonct.soluong = Convert.ToInt32(dtgview_hoadon.Rows[Row].Cells[column].Value.ToString());
            ctsp.soluong -= Convert.ToInt32(dtgview_hoadon.Rows[Row].Cells[column].Value.ToString());
            ctsp.soluong += _soluonghientai;
            hoadonct.Thanhtien = hoadonct.soluong * hoadonct.DonGia;
            _banhangService.updatehoadonchitiet(hoadonct);
            _banhangService.updatectsp(ctsp);
            loadHoadon(lbl_mahoadon.Text);
            tongtien();
            loadSpbanhang();
        }
        private void tbx_timkiem_TextChanged(object sender, EventArgs e)
        {
            loadSpbanhang(tbx_timkiem.Text);
        }

        private void btn_tatbarcode_Click(object sender, EventArgs e)
        {

        }

        private void btn_addvoucher_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn tạo voucher ?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                FrmThongtinVoucher frmThongtinVoucher = new FrmThongtinVoucher();// hiển thị form add vou
                frmThongtinVoucher.Show();
                frmThongtinVoucher.StartPosition = FormStartPosition.CenterScreen;
            }
        }
        GraphicsPath GetRoundPath(RectangleF Rect, int radius)
        {
            float r2 = radius / 2f;
            GraphicsPath GraphPath = new GraphicsPath();
            GraphPath.AddArc(Rect.X, Rect.Y, radius, radius, 180, 90);
            GraphPath.AddLine(Rect.X + r2, Rect.Y, Rect.Width - r2, Rect.Y);
            GraphPath.AddArc(Rect.X + Rect.Width - radius, Rect.Y, radius, radius, 270, 90);
            GraphPath.AddLine(Rect.Width, Rect.Y + r2, Rect.Width, Rect.Height - r2);
            GraphPath.AddArc(Rect.X + Rect.Width - radius,
                             Rect.Y + Rect.Height - radius, radius, radius, 0, 90);
            GraphPath.AddLine(Rect.Width - r2, Rect.Height, Rect.X + r2, Rect.Height);
            GraphPath.AddArc(Rect.X, Rect.Y + Rect.Height - radius, radius, radius, 90, 90);
            GraphPath.AddLine(Rect.X, Rect.Height - r2, Rect.X, Rect.Y + r2);
            GraphPath.CloseFigure();
            return GraphPath;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            RectangleF Rect = new RectangleF(0, 0, this.Width, this.Height);
            using (GraphicsPath GraphPath = GetRoundPath(Rect, 50))
            {
                this.Region = new Region(GraphPath);
                using (Pen pen = new Pen(System.Drawing.Color.CadetBlue, 1.75f))
                {
                    pen.Alignment = PenAlignment.Inset;
                    e.Graphics.DrawPath(pen, GraphPath);
                }
            }
        }



        private void tbx_khachtra_KeyPress(object sender, KeyPressEventArgs e)// check kí tự
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void btn_dathang_Click(object sender, EventArgs e) // dặt hàng on
        {

        }
    }
}
