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
using _2_BUS_BusinessLayer.Utilities;

namespace _3_GUI_PresentationLayer
{
    public partial class Frm_Login : Form
    {
        public static string name; // khai báo biến vs class để sd
        private I_Login _chucnangLogin;
        private NhanVien _nv;
        private Validateform _validateform;
        private I_QLNhanVien I_QLNhanVien;
        public static int vaitro;
        public Frm_Login()
        {
            InitializeComponent();
            _chucnangLogin = new Login();
            _validateform = new Validateform();
            _nv = new NhanVien();
            I_QLNhanVien = new Service_QLNhanVien();
           
        }

        private void bnt_exit_Click(object sender, EventArgs e) // nút thoát
        {
            this.Close();
        }

        private void bnt_login_Click(object sender, EventArgs e)
        {
            _validateform.checkEmail(txt_email.Text); // check null 42-46 
            _validateform.checkNull(txt_email.Text);
            _validateform.checkNull(txt_matkhau.Text);
            _nv.Email = txt_email.Text;
            _nv.MatKhau = _chucnangLogin.BUSMahoaPassWord(txt_matkhau.Text);
            Frm_Main.mail = txt_email.Text;
            if (_chucnangLogin.login(_nv))  // check xem tài khoản có tồn tại trong sql ko
            {
                MessageBox.Show("chúc mừng " + txt_email.Text + " đăng nhập thành công", "thông báo");
                this.Hide();
                name = I_QLNhanVien.GetlstNhanVien().Where(c => c.Email == txt_email.Text).Select(c => c.MaNv).FirstOrDefault();
                Frm_Main a = new Frm_Main();
                a.Show();
            }
            else
            {
                MessageBox.Show(txt_email.Text + " đăng nhập thất bại", "thông báo");
            }
        }

        private void lal_quenPassWord_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // quên mk
        {
            if (txt_email.Text != "") // chheck nulll của email, bắt phải nhập email
            {
                if (_chucnangLogin.quenmatkhau(txt_email.Text)) // gửi mật khẩu mới về mail
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append(_chucnangLogin.randomstring(4, true));
                    builder.Append(_chucnangLogin.randomnumber(1000, 9999));
                    builder.Append(_chucnangLogin.randomstring(2, false));
                    string matkhaumoi = _chucnangLogin.BUSMahoaPassWord(builder.ToString());
                    _chucnangLogin.BUSdoimatkhau(txt_email.Text, matkhaumoi);
                    _chucnangLogin.sendmail(txt_email.Text, builder.ToString());
                    MessageBox.Show("Đã có mật khẩu mới gửi về Mail của bạn", "thông báo");

                }
            }
            else
            {
                MessageBox.Show("Bắt buộc phải nhập email", "thông báo");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)// click vào sẽ hiển thị form đổi mk
        {
            Frm_Doimatkhau a = new Frm_Doimatkhau();
            a.Show();
           
        }

        private void Frm_Login_Load(object sender, EventArgs e)
        {
            



        }
    }
}
