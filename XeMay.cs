using System;

namespace LogisticsAutoSpeed
{
    // Kế thừa từ PhuongTien (Inheritance)
    public class XeMay : PhuongTien
    {
        private int _dungTichXylanh;

        public int DungTichXylanh
        {
            get => _dungTichXylanh;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Dung tích xylanh phải lớn hơn 0!");
                _dungTichXylanh = value;
            }
        }

        public XeMay(string maPT, string tenHang, int namSanXuat, decimal giaGoc, int dungTichXylanh)
            : base(maPT, tenHang, namSanXuat, giaGoc)
        {
            DungTichXylanh = dungTichXylanh;
        }

        // Ghi đè phương thức tính giá lăn bánh
        public override decimal TinhGiaLanBanh()
        {
            decimal thueTruocBaRate = DungTichXylanh < 175 ? 0.02m : 0.05m;
            return GiaGoc + (GiaGoc * thueTruocBaRate);
        }

        public override string GetInfo()
        {
            return $"{base.GetInfo()} | Dung tích: {DungTichXylanh} cc";
        }
    }
}