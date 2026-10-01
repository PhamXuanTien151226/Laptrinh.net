using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TechMartManager
{
    public partial class Form1 : Form
    {
        private readonly BindingList<Product> _allProducts = new BindingList<Product>();
        private readonly BindingSource _productBindingSource = new BindingSource();
        private List<Category> _categoryList = new List<Category>();
        private string _currentImagePath = string.Empty;

        public Form1()
        {
            InitializeComponent();
            SetupApp();
            LoadSeedData();
        }

        private void SetupApp()
        {
            // 1. Cấu hình ComboBox Danh mục
            _categoryList = new List<Category>
            {
                new Category("CAT01", "Điện thoại"),
                new Category("CAT02", "Laptop"),
                new Category("CAT03", "Phụ kiện")
            };
            cboCategory.DataSource = _categoryList;
            cboCategory.DisplayMember = "CategoryName";
            cboCategory.ValueMember = "CategoryId";

            // 2. PictureBox chế độ Zoom
            picAvatar.SizeMode = PictureBoxSizeMode.Zoom;

            // 3. Cấu hình DataGridView theo yêu cầu
            dgvProducts.AutoGenerateColumns = false;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.MultiSelect = false;
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.ReadOnly = true;

            dgvProducts.Columns.Clear();
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ProductId",
                HeaderText = "Mã SP",
                Width = 90
            });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ProductName",
                HeaderText = "Tên SP",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CategoryName",
                HeaderText = "Danh Mục",
                Width = 120
            });

            // Định dạng Đơn giá N0
            var priceCol = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "UnitPrice",
                HeaderText = "Đơn Giá",
                Width = 130
            };
            priceCol.DefaultCellStyle.Format = "N0";
            priceCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvProducts.Columns.Add(priceCol);

            // Cột Số lượng
            var qtyCol = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Quantity",
                HeaderText = "Số Lượng",
                Width = 90
            };
            qtyCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvProducts.Columns.Add(qtyCol);

            // Gán dữ liệu qua BindingSource
            _productBindingSource.DataSource = _allProducts;
            dgvProducts.DataSource = _productBindingSource;

            // Đăng ký sự kiện nạp ngược dữ liệu lên controls
            dgvProducts.SelectionChanged += DgvProducts_SelectionChanged;
        }

        private void LoadSeedData()
        {
            _allProducts.Add(new Product { ProductId = "SP001", ProductName = "iPhone 15 Pro Max", CategoryId = "CAT01", CategoryName = "Điện thoại", UnitPrice = 30990000m, Quantity = 12 });
            _allProducts.Add(new Product { ProductId = "SP002", ProductName = "Laptop ASUS Zenbook", CategoryId = "CAT02", CategoryName = "Laptop", UnitPrice = 24500000m, Quantity = 7 });
            _allProducts.Add(new Product { ProductId = "SP003", ProductName = "Cáp sạc Type-C Baseus", CategoryId = "CAT03", CategoryName = "Phụ kiện", UnitPrice = 150000m, Quantity = 50 });
            UpdateStatusCount();
        }

        private void UpdateStatusCount()
        {
            lblStatusCount.Text = $"Tổng số sản phẩm: {_productBindingSource.Count}";
        }

        // ================= VALIDATION ERRORPROVIDER (TC02) =================
        private bool ValidateFormInputs()
        {
            bool isValid = true;
            errorProvider.Clear();

            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                errorProvider.SetError(txtProductName, "Tên sản phẩm không được để trống!");
                isValid = false;
            }

            if (!decimal.TryParse(txtUnitPrice.Text.Trim(), out decimal price) || price <= 0)
            {
                errorProvider.SetError(txtUnitPrice, "Đơn giá phải là số lớn hơn 0!");
                isValid = false;
            }

            if (!int.TryParse(txtQuantity.Text.Trim(), out int qty) || qty < 0)
            {
                errorProvider.SetError(txtQuantity, "Số lượng phải là số nguyên không âm (≥ 0)!");
                isValid = false;
            }

            return isValid;
        }

        // ================= CÁC HÀM SỰ KIỆN NÚT VÀ MENU =================

        // 1. Thêm mới
        public void btnAdd_Click(object? sender, EventArgs e)
        {
            if (!ValidateFormInputs()) return;

            string id = string.IsNullOrWhiteSpace(txtProductId.Text) ? $"SP{_allProducts.Count + 1:D3}" : txtProductId.Text.Trim();

            if (_allProducts.Any(p => p.ProductId.Equals(id, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Mã sản phẩm này đã tồn tại! Vui lòng chọn mã khác.", "Cảnh báo trùng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboCategory.SelectedItem is Category cat)
            {
                var newProd = new Product
                {
                    ProductId = id,
                    ProductName = txtProductName.Text.Trim(),
                    CategoryId = cat.CategoryId,
                    CategoryName = cat.CategoryName,
                    UnitPrice = decimal.Parse(txtUnitPrice.Text.Trim()),
                    Quantity = int.Parse(txtQuantity.Text.Trim()),
                    ImagePath = _currentImagePath
                };

                _allProducts.Add(newProd);
                ApplySearch(txtSearch.Text);
                UpdateStatusCount();
                ClearForm();
                MessageBox.Show("Thêm mới sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // 2. Cập nhật
        public void btnUpdate_Click(object? sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng click chọn 1 dòng sản phẩm trên bảng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateFormInputs()) return;

            if (dgvProducts.CurrentRow.DataBoundItem is Product cur && cboCategory.SelectedItem is Category cat)
            {
                cur.ProductName = txtProductName.Text.Trim();
                cur.CategoryId = cat.CategoryId;
                cur.CategoryName = cat.CategoryName;
                cur.UnitPrice = decimal.Parse(txtUnitPrice.Text.Trim());
                cur.Quantity = int.Parse(txtQuantity.Text.Trim());
                cur.ImagePath = _currentImagePath;

                _productBindingSource.ResetBindings(false);
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // 3. Xóa (TC05)
        public void btnDelete_Click(object? sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng click chọn sản phẩm cần xóa trên bảng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa sản phẩm này không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                if (dgvProducts.CurrentRow.DataBoundItem is Product cur)
                {
                    _allProducts.Remove(cur);
                    ApplySearch(txtSearch.Text);
                    UpdateStatusCount();
                    ClearForm();
                }
            }
        }

        // 4. Chọn ảnh (TC04)
        public void btnChooseImage_Click(object? sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn ảnh sản phẩm";
                ofd.Filter = "Tệp hình ảnh (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _currentImagePath = ofd.FileName;
                    if (picAvatar.Image != null) picAvatar.Image.Dispose();
                    picAvatar.Image = Image.FromFile(ofd.FileName);
                }
            }
        }

        // 5. Live Search
        public void txtSearch_TextChanged(object? sender, EventArgs e)
        {
            ApplySearch(txtSearch.Text);
        }

        private void ApplySearch(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                _productBindingSource.DataSource = _allProducts;
            }
            else
            {
                var filtered = _allProducts
                    .Where(p => p.ProductName.IndexOf(keyword.Trim(), StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();
                _productBindingSource.DataSource = new BindingList<Product>(filtered);
            }
            UpdateStatusCount();
        }

        // 6. Nạp ngược dữ liệu khi click dòng
        public void DgvProducts_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null || dgvProducts.CurrentRow.Index < 0) return;

            if (dgvProducts.CurrentRow.DataBoundItem is Product p)
            {
                txtProductId.Text = p.ProductId;
                txtProductName.Text = p.ProductName;
                txtUnitPrice.Text = p.UnitPrice.ToString("0");
                txtQuantity.Text = p.Quantity.ToString();
                cboCategory.SelectedValue = p.CategoryId;

                _currentImagePath = p.ImagePath;
                if (!string.IsNullOrEmpty(p.ImagePath) && File.Exists(p.ImagePath))
                {
                    if (picAvatar.Image != null) picAvatar.Image.Dispose();
                    picAvatar.Image = Image.FromFile(p.ImagePath);
                }
                else
                {
                    picAvatar.Image = null;
                }
            }
        }

        // 7. Xuất CSV
        public void exportCSVToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV File (*.csv)|*.csv";
                sfd.FileName = $"TechMart_Products_{DateTime.Now:yyyyMMdd_HHmm}.csv";
                sfd.Title = "Xuất danh sách sản phẩm ra CSV";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var sb = new StringBuilder();
                        sb.AppendLine("Mã SP,Tên Sản Phẩm,Danh Mục,Đơn Giá,Số Lượng");

                        foreach (var p in _allProducts)
                        {
                            sb.AppendLine($"\"{p.ProductId}\",\"{p.ProductName}\",\"{p.CategoryName}\",{p.UnitPrice},{p.Quantity}");
                        }

                        File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                        MessageBox.Show("Xuất file CSV thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi xuất file: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // 8. Thoát
        public void exitToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ClearForm()
        {
            txtProductId.Clear();
            txtProductName.Clear();
            txtUnitPrice.Clear();
            txtQuantity.Clear();
            if (picAvatar.Image != null) picAvatar.Image.Dispose();
            picAvatar.Image = null;
            _currentImagePath = string.Empty;
            errorProvider.Clear();
        }
    }
}