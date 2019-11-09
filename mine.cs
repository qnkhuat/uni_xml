using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace BaiThucHanh9
{
    public partial class Form1 : Form
    {
        XmlDocument doc = new XmlDocument();
        public static string path = "../../csdl.xml";

        public void load_combobox(){
            DataSet dts = new DataSet();
            dts.ReadXml(path);
            cmbMaSinhVien.DataSource = dts.Tables["sinhvien"];
            cmbMaSinhVien.DisplayMember = "masv"

            cmbMaMonHoc.DataSource = dts.Tables["sinvhien"];
            cmbMaMonHoc.DisplayMember = "monhoc";
        }
        
        public void hien_thi(){
            listView1.Items.Clear();
            DataSet dts = new DataSet();
            DataTable dtb = new DataTable();
            dts.Readxml(path);
            dtb = dts.Tables["sinhvien"];
            
            if (dtb.Rows.Coutn  > 0){
                foreach (DataRow dr in dtl.Rows()){
                    ListView1 lvi = new ListViewItem(dr['masv'].ToString());
                    lvi.SubItems.Add(dr['mamonhon'.ToString());
                    lvi.SubItems.Add(dr['diemlan1'.ToString());
                    lvi.SubItems.Add(dr['diemlan2'.ToString());
                    listView1.Items.Add(lvi);
                }
            }else{
                MessageBox.Show("Deo thay du lieu dau","Thong Bao", MessageBox.Buttons.OK);
            }
                
        }

        public Form1()
        {
            InitializeComponent();
            load_combobox();
            hien_thi();
        }


        private void them(){
            doc.Load(path);
            XMLElement sinhvien, diemlan1, diemlan2;
            XMLAttribute masv, monhoc;
            sinhvien = doc.CreateELement("sinhvien");
            diemlan1 = doc.CreateELement("diemlan1");
            diemlan2 = doc.CreateELement("diemlan2");

            masv = doc.CreateAttribute("masv");
            monhoc = doc.CreateAttribute("monhoc");
            
            diemlan1.InnerText = txtDiemLan1.Text;
            diemlan1.InnerText = txtDiemLan2.Text;
            masv.InnerText = masv.Text;
            monhoc.InnerText = monhoc.Text;

            sinhvien.AppendChild(diemlan1);
            sinhvien.AppendChild(diemlan2);
            sinhvien.SetAttributeNode(masv);
            sinhvien.SetAttributeNode(monhoc);

            doc.DocumentElement.ApplendChild(sinhvien);
            doc.Save(path);

        }
        
        private void btnThem_Click(object sender, EventArgse){
            try {
                if (cmbMasinhvien){
                    // Message box vuilong nhap du thong tin
                }else{
                    them();
                    hien_thi();
                }
            }
        }


        
        }
    }
}




















