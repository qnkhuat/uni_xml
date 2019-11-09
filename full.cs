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

        private void load_combobox()
        {
            DataSet dts = new DataSet();
            dts.ReadXml(path);
            cmbMaSinhVien.DataSource = dts.Tables["sinhvien"];
            cmbMaSinhVien.DisplayMember = "masv";
            cmbMonHoc.DataSource = dts.Tables["sinhvien"];
            cmbMonHoc.DisplayMember = "monhoc";
        }

        private void hien_thi()
        {
            listView1.Items.Clear();
            DataSet dts = new DataSet();
            DataTable dtl = new DataTable();
            dts.ReadXml(path);
            dtl = dts.Tables["sinhvien"];
            if (dtl.Rows.Count > 0)
            {
                int i = 1;
                foreach (DataRow dr in dtl.Rows)
                {
                    ListViewItem lvi = new ListViewItem(i.ToString());
                    lvi.SubItems.Add(dr["masv"].ToString());
                    lvi.SubItems.Add(dr["monhoc"].ToString());
                    lvi.SubItems.Add(dr["diemlan1"].ToString());
                    lvi.SubItems.Add(dr["diemlan2"].ToString());
                    i++;
                    listView1.Items.Add(lvi);
                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu để hiển thị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        public Form1()
        {
            InitializeComponent();
            load_combobox();
            hien_thi();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                cmbMaSinhVien.Text = item.SubItems[1].Text;
                cmbMonHoc.Text = item.SubItems[2].Text;
                txtDiemLan1.Text = item.SubItems[3].Text;
                txtDiemLan2.Text = item.SubItems[4].Text;
            }
        }

        private void them()
        {
            doc.Load(path);
            XmlElement sinhvien, diemlan1, diemlan2;
            XmlAttribute masv, monhoc;
            sinhvien = doc.CreateElement("sinhvien");
            diemlan1 = doc.CreateElement("diemlan1");
            diemlan2 = doc.CreateElement("diemlan2");
            masv = doc.CreateAttribute("masv");
            monhoc = doc.CreateAttribute("monhoc");
            diemlan1.InnerText = txtDiemLan1.Text;
            diemlan2.InnerText = txtDiemLan2.Text;
            masv.InnerText = cmbMaSinhVien.Text;
            monhoc.InnerText = cmbMonHoc.Text;
            sinhvien.SetAttributeNode(masv);
            sinhvien.SetAttributeNode(monhoc);
            sinhvien.AppendChild(diemlan1);
            sinhvien.AppendChild(diemlan2);
            doc.DocumentElement.AppendChild(sinhvien);
            doc.Save(path);
            MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void sua()
        {
            doc.Load(path);
            XmlNode node = doc.SelectSingleNode("/bangdiem/sinhvien[@masv='" + (cmbMaSinhVien.Text).Trim() + "']");
            if (node != null)
            {
                node.Attributes[0].InnerText = cmbMaSinhVien.Text;
                node.Attributes[1].InnerText = cmbMonHoc.Text;
                node.ChildNodes[1].InnerText = txtDiemLan1.Text;
                node.ChildNodes[2].InnerText = txtDiemLan2.Text;
                doc.Save(path);
                MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Mã sinh viên muốn sửa không có trong CSDL!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        private listView1_SelectedIndexChanged(object sender, EventArgs e){
            foreach(ListViewItem item in listView1.SelectedItems){
                chinhanh_cbb.Text = item.SubItems[0].Text;
                sogoidi_cbb.Text = item.SubItems[1].Text;
                chinhanh_cbb.Text = item.SubItems[2].Text;
                chinhanh_cbb.Text = item.SubItems[3].Text;
                chinhanh_cbb.Text = item.SubItems[4].Text;
            }
        }

        private void xoa(){
            doc.Load(path);

            XmlNode node = doc.SelectSingleNode("/thongtincuocgoi/cuocgoi[sogoiden='"+(sogoiden_txt.Text).Trim() +"']");
            if (node!= nul)
            {
                doc.DocumentElement.RemoveChild(node);
                doc.Save(path);
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbMaSinhVien.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin muốn thêm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    xoa();
                    hien_thi();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Có lỗi xảy ra! Không thể thêm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbMaSinhVien.Text == "" || cmbMonHoc.Text == "" || txtDiemLan1.Text == "" || txtDiemLan2.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin muốn thêm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    them();
                    hien_thi();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Có lỗi xảy ra! Không thể thêm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
