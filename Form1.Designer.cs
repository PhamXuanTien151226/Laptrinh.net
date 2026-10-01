namespace TechMartManager
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusCount;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.Panel panelInput;
        private System.Windows.Forms.Panel panelGrid;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.TextBox txtProductId;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.TextBox txtUnitPrice;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.PictureBox picAvatar;
        private System.Windows.Forms.Button btnChooseImage;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.DataGridView dgvProducts;
        private System.Windows.Forms.ErrorProvider errorProvider;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatusCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.panelInput = new System.Windows.Forms.Panel();
            this.panelGrid = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.txtProductId = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.lblPrice = new System.Windows.Forms.Label();
            this.txtUnitPrice = new System.Windows.Forms.TextBox();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.picAvatar = new System.Windows.Forms.PictureBox();
            this.btnChooseImage = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.dgvProducts = new System.Windows.Forms.DataGridView();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);

            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanelMain.SuspendLayout();
            this.panelInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).BeginInit();
            this.panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();

            // MenuStrip
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.fileToolStripMenuItem });
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Size = new System.Drawing.Size(1084, 24);

            this.fileToolStripMenuItem.Text = "File";
            this.exportCSVToolStripMenuItem.Text = "Export CSV";
            this.exportCSVToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.exportCSVToolStripMenuItem.Click += new System.EventHandler(this.exportCSVToolStripMenuItem_Click);

            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);

            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.exportCSVToolStripMenuItem, this.exitToolStripMenuItem });

            // StatusStrip
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.lblStatusCount });
            this.statusStrip1.Location = new System.Drawing.Point(0, 639);
            this.statusStrip1.Size = new System.Drawing.Size(1084, 22);

            this.lblStatusCount.Text = "Tổng số sản phẩm: 0";
            this.lblStatusCount.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);

            // TableLayoutPanel (35% - 65%)
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.ColumnCount = 2;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanelMain.Controls.Add(this.panelInput, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.panelGrid, 1, 0);
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(1084, 615);

            // Panel Cột Trái (35%)
            this.panelInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelInput.AutoScroll = true;
            this.panelInput.Padding = new System.Windows.Forms.Padding(10);

            this.lblTitle.Text = "THÔNG TIN SẢN PHẨM";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(15, 15);
            this.lblTitle.Size = new System.Drawing.Size(250, 25);

            // Tọa độ đã căn chỉnh chuẩn: Label X=15, TextBox X=110 (không bị đè chữ)
            this.lblId.Text = "Mã SP:";
            this.lblId.Location = new System.Drawing.Point(15, 53);
            this.lblId.Size = new System.Drawing.Size(85, 20);
            this.txtProductId.Location = new System.Drawing.Point(110, 50);
            this.txtProductId.Size = new System.Drawing.Size(230, 23);

            this.lblName.Text = "Tên SP:";
            this.lblName.Location = new System.Drawing.Point(15, 88);
            this.lblName.Size = new System.Drawing.Size(85, 20);
            this.txtProductName.Location = new System.Drawing.Point(110, 85);
            this.txtProductName.Size = new System.Drawing.Size(230, 23);

            this.lblCategory.Text = "Danh mục:";
            this.lblCategory.Location = new System.Drawing.Point(15, 123);
            this.lblCategory.Size = new System.Drawing.Size(85, 20);
            this.cboCategory.Location = new System.Drawing.Point(110, 120);
            this.cboCategory.Size = new System.Drawing.Size(230, 23);
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblPrice.Text = "Đơn giá:";
            this.lblPrice.Location = new System.Drawing.Point(15, 158);
            this.lblPrice.Size = new System.Drawing.Size(85, 20);
            this.txtUnitPrice.Location = new System.Drawing.Point(110, 155);
            this.txtUnitPrice.Size = new System.Drawing.Size(230, 23);

            this.lblQuantity.Text = "Số lượng:";
            this.lblQuantity.Location = new System.Drawing.Point(15, 193);
            this.lblQuantity.Size = new System.Drawing.Size(85, 20);
            this.txtQuantity.Location = new System.Drawing.Point(110, 190);
            this.txtQuantity.Size = new System.Drawing.Size(230, 23);

            this.picAvatar.Location = new System.Drawing.Point(110, 225);
            this.picAvatar.Size = new System.Drawing.Size(230, 130);
            this.picAvatar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.btnChooseImage.Text = "Chọn ảnh...";
            this.btnChooseImage.Location = new System.Drawing.Point(110, 365);
            this.btnChooseImage.Size = new System.Drawing.Size(230, 30);
            this.btnChooseImage.Click += new System.EventHandler(this.btnChooseImage_Click);

            this.btnAdd.Text = "Thêm mới";
            this.btnAdd.Location = new System.Drawing.Point(15, 415);
            this.btnAdd.Size = new System.Drawing.Size(95, 35);
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            this.btnUpdate.Text = "Cập nhật";
            this.btnUpdate.Location = new System.Drawing.Point(125, 415);
            this.btnUpdate.Size = new System.Drawing.Size(95, 35);
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);

            this.btnDelete.Text = "Xóa";
            this.btnDelete.Location = new System.Drawing.Point(235, 415);
            this.btnDelete.Size = new System.Drawing.Size(95, 35);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            this.panelInput.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTitle, this.lblId, this.txtProductId, this.lblName, this.txtProductName,
                this.lblCategory, this.cboCategory, this.lblPrice, this.txtUnitPrice, this.lblQuantity,
                this.txtQuantity, this.picAvatar, this.btnChooseImage, this.btnAdd, this.btnUpdate, this.btnDelete
            });

            // Panel Cột Phải (65%)
            this.panelGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGrid.Padding = new System.Windows.Forms.Padding(10);

            this.lblSearch.Text = "Tìm kiếm theo tên SP:";
            this.lblSearch.Location = new System.Drawing.Point(10, 15);
            this.lblSearch.AutoSize = true;

            this.txtSearch.Location = new System.Drawing.Point(150, 12);
            this.txtSearch.Size = new System.Drawing.Size(535, 23);
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);

            this.dgvProducts.Location = new System.Drawing.Point(10, 48);
            this.dgvProducts.Size = new System.Drawing.Size(675, 545);
            this.dgvProducts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));

            this.panelGrid.Controls.AddRange(new System.Windows.Forms.Control[] { this.lblSearch, this.txtSearch, this.dgvProducts });

            // Form Chính
            this.ClientSize = new System.Drawing.Size(1084, 661);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(950, 550);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TechMart Product Manager";

            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.panelInput.ResumeLayout(false);
            this.panelInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).EndInit();
            this.panelGrid.ResumeLayout(false);
            this.panelGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
