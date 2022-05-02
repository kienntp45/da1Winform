﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using _1_DAL_DataAccessLayer.Models;
using _2_BUS_BusinessLayer.IService;
using _2_BUS_BusinessLayer.Service;
using _2_BUS_BusinessLayer.Utilities;

namespace _3_GUI_PresentationLayer
{
    public partial class FrmThongtinVoucher : Form 
    {
        private IBUS_VoucherService _voucherService;
        private Voucher _voucher;
        public string _mavoucher;
        private Validateform _validateform;
       
        public FrmThongtinVoucher()
        {
            InitializeComponent();
           
            _voucher = new Voucher();
            _voucherService = new BUS_VoucherService();
            _validateform = new Validateform();
            btn_fix.Visible = false;
           
            txt_soluongdasudung.Text = "0";
            rdb_hoatdong.Checked = true;

        }

        public FrmThongtinVoucher(string voucher, string namvoucher, string nsd, string hsd, string soluong, string soluongdsd, string menhgia, string trngthai, string ghichu)
        {
            InitializeComponent();
            _voucherService = new BUS_VoucherService();
            _validateform = new Validateform();
            _voucher = new Voucher();
            txt_ghichu.Text = ghichu;
            txt_namevoucher.Text = namvoucher;
            dtp_nsd.Text = nsd;
            dtp_hsd.Text = hsd;
            nud_menhgia.Text = menhgia;
            if (trngthai == "Hoạt động")
            {
                rdb_hoatdong.Checked = true;
            }
            else if (trngthai == "Kết thúc")
            {
                rbt_ketthuc.Checked = true;
            }
            txt_soluong.Text = soluong;
            txt_soluongdasudung.Text = soluongdsd;

            _mavoucher = voucher;

            btn_add.Visible = false;

            // khia báo
        }
       
        private void btn_add_Click(object sender, EventArgs e) // thêm vou
        {

            try
            {
                _validateform.checkSo(txt_soluongdasudung.Text);
                _validateform.checkSo(nud_menhgia.Text);
                _validateform.checkSo(txt_soluong.Text);
                _validateform.checkChu(txt_ghichu.Text);
                if (txt_namevoucher.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn nhập thiếu tên Voucher", "thông báo", MessageBoxButtons.OK);
                    txt_namevoucher.Focus();
                    return;
                }
                else if (txt_soluong.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn nhập thiếu số lượng", "thông báo", MessageBoxButtons.OK);
                    txt_soluong.Focus();
                    return;
                }
                else if (txt_soluongdasudung.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn nhập thiếu số lượng đã sử dụng", "thông báo", MessageBoxButtons.OK);
                    txt_soluongdasudung.Focus();
                    return;
                }
                else if (txt_ghichu.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn nhập thiếu ghi chú", "thông báo", MessageBoxButtons.OK);
                    txt_ghichu.Focus();
                    return;
                }
                else if (rbt_ketthuc.Checked==false&&rdb_hoatdong.Checked==false)
                {
                    MessageBox.Show("Bạn nhập thiếu Trạng thái", "thông báo", MessageBoxButtons.OK);
                    return;
                }
                else if (nud_menhgia.Value<=0)
                {
                    MessageBox.Show("Mệnh giá phải lớn hơn 0", "thông báo", MessageBoxButtons.OK);
                    return;
                }
                _voucher = new Voucher(); // lấy dữ liệu từ form 111-120
                _voucher.MaVouCher = "VC" + _voucherService.ListvVouchers().Max(c => c.MaVouCher) + 1;
                _voucher.Vouchers = txt_namevoucher.Text;
                _voucher.NSD = dtp_nsd.Value;
                _voucher.HSD = dtp_hsd.Value;
                _voucher.SoLuong = Convert.ToInt32(txt_soluong.Text);
                _voucher.SoLuongDSD = Convert.ToInt32(txt_soluongdasudung.Text);
                _voucher.TrangThai = rdb_hoatdong.Checked == true ? 1 : 0;
                _voucher.GhiChu = txt_ghichu.Text;
                _voucher.MenhGia = Convert.ToInt32(nud_menhgia.Value);
                MessageBox.Show(_voucherService.add(_voucher), "Thông báo");// lưu dữ liệu vào sql
            }
            catch (Exception x)
            {

                MessageBox.Show(x.Message);
            }
        }

        private void btn_fix_Click(object sender, EventArgs e)// sửa  vou
        {
            try
            {
                _voucher = new Voucher(); // lấy dl lại từ form
                _voucher = _voucherService.ListvVouchers().Find(c => c.MaVouCher == _mavoucher);
                _voucher.Vouchers = txt_namevoucher.Text;
                _voucher.NSD = dtp_nsd.Value;
                _voucher.HSD = dtp_hsd.Value;
                _voucher.SoLuong = Convert.ToInt32(txt_soluong.Text);
                _voucher.SoLuongDSD = Convert.ToInt32(txt_soluongdasudung.Text);
                _voucher.TrangThai = rdb_hoatdong.Checked == true ? 1 : 0;
                _voucher.GhiChu = txt_ghichu.Text;
                _voucher.MenhGia = Convert.ToInt32(nud_menhgia.Value);
                MessageBox.Show(_voucherService.update(_voucher), "Thông báo"); // sửa lại
            }
            catch (Exception x)
            {

                MessageBox.Show(x.Message);
            }
        }

        private void FrmThongtinVoucher_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
