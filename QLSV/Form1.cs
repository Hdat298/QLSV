using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            setNull();
            setNut(true);
            setKhoa(true);
        }

        public void setNull()
        {
            txtMS.Text = "";
            txtName.Text = "";
            txtScore.Text = "";
            rdoNam.Checked = true;
            txtMS.Focus();
        }

        public void setNut(bool e)
        {
            btnAdd.Enabled = e;
            btnDel.Enabled = e;
            btnEdit.Enabled = e;
            btnSave.Enabled = !e;
            btnNoSave.Enabled = !e;
            btnClose.Enabled = e;
        }

        public void setKhoa(bool e)
        {
            txtMS.ReadOnly = e;
            txtName.ReadOnly = e;
            txtScore.ReadOnly = e;
            cbxNganh.Enabled = !e;
            dtpNgaySInh.Enabled = !e;
        }

        private bool ktTrung(string mssv)
        {
            for (int i = 0; i < lvSV.Items.Count; i++)
            {
                if (lvSV.Items[i].SubItems[0].Text.ToString() == mssv)
                {
                    return true;
                }
            }
            return false;
        }
        private bool ktNhap(string mssv, string name, string gioiTinh, string ngaySinh, string diemTB, string chuyenNganh)
        {
            if (mssv == "" || name == "" || gioiTinh == "" || ngaySinh == "" || diemTB == "" || chuyenNganh == "")
            {
                return true;
            }
            return false;
        }

        private int viTriTrung(string mssv)
        {
            for (int i = 0; i < lvSV.Items.Count; i++)
            {
                if (lvSV.Items[i].SubItems[0].ToString() == mssv)
                {
                    return i;
                }
            }
            return -1;
        }
        private int countNam()
        {
            int n = lvSV.Items.Count;
            int temp = 0;
            for (int i = 0; i < n; i++)
            {
                if (lvSV.Items[i].SubItems[2].Text == "Nam")
                    temp++;
            }
            return temp;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            setNull();
            setNut(false);
            setKhoa(false);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            setNut(false);
            setKhoa(false);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string MSSV = txtMS.Text;
            string name = txtName.Text;
            string gioiTinh;
            if (rdoNam.Checked == true)
                gioiTinh = "Nam";
            else
                gioiTinh = "Nữ";
            string ngaySinh = dtpNgaySInh.Value.ToShortDateString();
            double diemTB = double.Parse(txtScore.Text);
            string chuyenNganh = cbxNganh.SelectedItem.ToString();

            if (ktNhap(MSSV, name, gioiTinh, ngaySinh, diemTB.ToString(), chuyenNganh) == false)
            {
                if (ktTrung(txtMS.Text) == false)
                {
                    ListViewItem lvStu = lvSV.Items.Add(MSSV);
                    lvStu.SubItems.Add(name);
                    lvStu.SubItems.Add(gioiTinh);
                    lvStu.SubItems.Add(ngaySinh);
                    lvStu.SubItems.Add(diemTB.ToString());
                    lvStu.SubItems.Add(chuyenNganh);
                }
                else
                {
                    int i = viTriTrung(txtMS.Text);
                    lvSV.Items[i].SubItems[0].Text = MSSV;
                    lvSV.Items[i].SubItems[1].Text = name;
                    lvSV.Items[i].SubItems[2].Text = gioiTinh;
                    lvSV.Items[i].SubItems[3].Text = ngaySinh;
                    lvSV.Items[i].SubItems[4].Text = diemTB.ToString();
                    lvSV.Items[i].SubItems[5].Text = chuyenNganh;
                }
                setNut(true);
                setKhoa(true);
                setNull();

                txtTongNam.Text = countNam().ToString();
                txtTongNu.Text = (lvSV.Items.Count - countNam()).ToString();
            }
            else
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if(lvSV.SelectedItems.Count > 0)
            {
                if(MessageBox.Show("Bạn có muốn xóa!", "Thông báo",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    setNull();
                    lvSV.Items.RemoveAt(lvSV.SelectedIndices[0]);
                }
            }
        }

        private void lvSV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvSV.SelectedItems.Count > 0)
            {
                txtMS.Text = lvSV.SelectedItems[0].SubItems[0].Text;
                txtName.Text = lvSV.SelectedItems[0].SubItems[1].Text;
                if (lvSV.SelectedItems[0].SubItems[2].Text == "Nữ")
                    rdoNu.Checked = true;
                else
                    rdoNam.Checked = true;
                dtpNgaySInh.Text = lvSV.SelectedItems[0].SubItems[3].Text;
                txtScore.Text = lvSV.SelectedItems[0].SubItems[4].Text;
                cbxNganh.Text = lvSV.SelectedItems[0].SubItems[5].Text;
            }
        }

        private void txtScore_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar !='.'))
            {
                e.Handled = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

}
