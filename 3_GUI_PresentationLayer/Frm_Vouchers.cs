﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _1_DAL_DataAccessLayer.Models;
using _2_BUS_BusinessLayer.IService;
using _2_BUS_BusinessLayer.Service;


namespace _3_GUI_PresentationLayer
{
    public partial class Frm_Vouchers : Form
    {
        private IBUS_VoucherService _voucherService;
        
        public Frm_Vouchers()
        {
            InitializeComponent();

            _voucherService = new BUS_VoucherService();

        }
        public void loatdata()
        {
          
            dtgv_vorcher.ColumnCount = 9; //setup cái data có 9 cột
            dtgv_vorcher.Columns[0].Name = "ID";
            dtgv_vorcher.Columns[0].Visible = false; // ẩn ko cho hiện cột  này
            dtgv_vorcher.Columns[1].Name = "Tên Voucher";
            dtgv_vorcher.Columns[2].Name = "Ngày bắt đầu";
            dtgv_vorcher.Columns[3].Name = "Ngày kết thúc";
            dtgv_vorcher.Columns[4].Name = "Số lượng";
            dtgv_vorcher.Columns[5].Name = "Số lượng đã Dùng";
            dtgv_vorcher.Columns[6].Name = "Mệnh giá";
            dtgv_vorcher.Columns[7].Name = "Trạng thái ";
            dtgv_vorcher.Columns[8].Name = "Ghi chú";
            DataGridViewComboBoxColumn dtgv_cbb = new DataGridViewComboBoxColumn(); // setup combobox ở bên trong data 
            dtgv_cbb.HeaderText = "Chức năng";
            dtgv_cbb.Name = "cbb";
            dtgv_cbb.Items.Add("Thêm");
            dtgv_cbb.Items.Add("Sửa");
            dtgv_cbb.Items.Add("Kết thúc voucher");
            dtgv_cbb.Items.Add("Loat danh sách");
            DataGridViewButtonColumn dtgv_bt = new DataGridViewButtonColumn(); // nút xác nhận
            dtgv_bt.HeaderText = "Xác nhận";
            dtgv_bt.Name = "bt";
            dtgv_bt.Text = "Xác nhận";
            dtgv_bt.UseColumnTextForButtonValue = true;
            dtgv_vorcher.Columns.Add(dtgv_cbb);// ađ 2 nút vào data
            dtgv_vorcher.Columns.Add(dtgv_bt);
            dtgv_vorcher.Rows.Clear();// xóa trắng dl form
            foreach (var x in _voucherService.ListvVouchers()) // lấy dữ liệu từ sql đẩy lên data
            {
                dtgv_vorcher.Rows.Add(x.MaVouCher,x.Vouchers, x.NSD, x.HSD, x.SoLuong, x.SoLuongDSD, x.MenhGia,
                    x.TrangThai == 0  || x.HSD< DateTime.Now? "Kết thúc" : "Hoạt động", x.GhiChu);
            }
        }
       
        private void dtgv_vorcher_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                var index = e.RowIndex; // lấy index  mk  vừa nhấn
                if (index < 0 || _voucherService.ListvVouchers().Count == index) return; // check lấy sai  index thì dừng lại


               
                    if (e.ColumnIndex == dtgv_vorcher.Columns["bt"].Index && dtgv_vorcher.Rows[index].Cells["cbb"].Value.ToString() == "Thêm") // khi nhấn thêm, xác nhận thì hiển thị form thêm voucher
                    {
                        FrmThongtinVoucher a = new FrmThongtinVoucher();
                        a.FormClosed += new FormClosedEventHandler(Frm_Vouchers_Load);
                        a.Show();


                    }
               
                else if (e.ColumnIndex == dtgv_vorcher.Columns["bt"].Index && dtgv_vorcher.Rows[index].Cells["cbb"].Value.ToString() == "Sửa")
                {

                    FrmThongtinVoucher _a = new FrmThongtinVoucher(dtgv_vorcher.Rows[index].Cells[0].Value.ToString(), dtgv_vorcher.Rows[index].Cells[1].Value.ToString(), dtgv_vorcher.Rows[index].Cells[2].Value.ToString(), dtgv_vorcher.Rows[index].Cells[3].Value.ToString(), dtgv_vorcher.Rows[index].Cells[4].Value.ToString(), dtgv_vorcher.Rows[index].Cells[5].Value.ToString(), dtgv_vorcher.Rows[index].Cells[6].Value.ToString(), dtgv_vorcher.Rows[index].Cells[7].Value.ToString(), dtgv_vorcher.Rows[index].Cells[8].Value.ToString());
                    _a.Show();

                    _a.FormClosed += new FormClosedEventHandler(Frm_Vouchers_Load);


                }
                else if (e.ColumnIndex == dtgv_vorcher.Columns["bt"].Index && dtgv_vorcher.Rows[index].Cells["cbb"].Value.ToString() == "Loat danh sách") //nhán load danh sách, xán nhận thì load lại // giống kiểu làm mới

                {
                    loatdata();

                }
                else if (e.ColumnIndex == dtgv_vorcher.Columns["bt"].Index && dtgv_vorcher.Rows[index].Cells["cbb"].Value.ToString() == "Kết thúc voucher")
                {
                    Voucher a = new Voucher(); 
                    a = _voucherService.ListvVouchers().Find(c => c.MaVouCher == dtgv_vorcher.Rows[index].Cells[0].Value.ToString());
                    a.TrangThai = 0;// chuyển trạng thái =0, ko cho dùng
                    MessageBox.Show(_voucherService.update(a), "Thông báo", MessageBoxButtons.OK);
                    loatdata();

                }

            }
            catch (Exception exception )
            {

                MessageBox.Show("lỗi ",exception.Message);
            }
           
        }

        private void Frm_Vouchers_Load(object sender, EventArgs e)
        {
            loatdata();
           
                
        }

        private void dtgv_vorcher_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        //private int x = 12, y = 10, a = 1;



        //private Random random = new Random();
        //private void timer1_Tick(object sender, EventArgs e)
        //{

        //    x += a;
        //    label1.Location = new Point(x, y);
        //    if (x >= 564)
        //    {
        //        a = -1;
        //        label1.ForeColor = Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
        //    }
        //    if (x <= 116)
        //    {
        //        a = 1;
        //        label1.ForeColor = Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
        //    }

        //}
    }
}
