﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;

namespace BTL_QUANLYSINHVIEN
{
    public partial class frmPhieuDiemLop : Form
    {
        public string maLop;
        public string tenLop;
        public int namNhapHoc;
        public frmPhieuDiemLop()
        {
            InitializeComponent();
            btnXuatExcel.Enabled = false;
            // This line of code is generated by Data Source Configuration Wizard
            // Fill a SqlDataSource
        }
        private void load(int namHoc)
        {
            try
            {
                LopbindingSource.DataSource = Lop_BUS.phieuDiemLop_namHoc(maLop, namHoc);
                gcLop.DataSource = LopbindingSource;
            }
            catch
            {
                MessageBox.Show("Load điểm bị lỗi!");
            }
        }
        public void loadInfo()
        {
            tbLop.Text = tenLop;
            int namHienTai=DateTime.Now.Year;
            int namHoc=namNhapHoc;
            int t = cboNamHoc.Items.Count;
            for (int i = 0; i < t ; i++)
            {
                cboNamHoc.Items.RemoveAt(0);
            }
            string nam = namHoc.ToString() + "-" + (namHoc + 1).ToString();
            namHoc++;
            cboNamHoc.Items.Add(nam);
            for(int i=0;i<namHienTai-namNhapHoc-1;i++)
            {
                nam = namHoc.ToString() + "-" + (namHoc + 1).ToString();
                namHoc++;
                cboNamHoc.Items.Add(nam);
            }
            cboNamHoc.SelectedIndex = 0;

        }

        private void btnXemDiem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnXuatExcel.Enabled = true;
            String[] catchuoi = cboNamHoc.Text.Split('-');
            int namHoc = Convert.ToInt32(catchuoi[1]);
            gcLop.RefreshDataSource();
            int t = LopbindingSource.Count;
            for (int i = 0; i < t; i++)
                LopbindingSource.RemoveAt(0);
            //MessageBox.Show(namHoc.ToString());
            load(namHoc);
        }

        private void btnXuatExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //String[] catchuoi = cboNamHoc.Text.Split('-');
            //int namHoc = Convert.ToInt32(catchuoi[1]);
            DataTable dt = (DataTable)LopbindingSource.DataSource;
            dt.TableName = "allstudents";
            Report report = new Report();
            report.report_Lop("", dt, tenLop,cboNamHoc.Text);
        }
    }
}
