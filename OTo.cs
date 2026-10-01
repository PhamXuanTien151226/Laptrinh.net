using System;

namespace LogisticsAutoSpeed
{
    // Kế thừa từ PhuongTien (Inheritance)
    public class OTo : PhuongTien
    {
        private int _soChoNgoi;
        private double _dungTichDongCo;

        public int SoChoNgoi
        {
            get => _soChoNgoi;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Số chỗ ngồi phải lớn hơn 0!");
                _soChoNgoi = value;
            }
        }

        public double DungTichDongCo
        {
            get => _dungTichDongCo;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Dung tích động cơ phải lớn hơn 0!");
                _dungTichDongCo = value;
            }
        }

        public OTo(string maPT, string tenHang, int namSanXuat, decimal giaGoc, int soChoNgoi, double dungTichDongCo)
            : base(maPT, tenHang, namSanXuat, giaGoc)
        {
            SoChoNgoi = soChoNgoi;
            DungTichDongCo = dungTichDongCo;
        }

        // Ghi đè phương thức tính giá lăn bánh (Polymorphism)
        public override decimal TinhGiaLanBanh()
        {
            if (SoChoNgoi <= 9)
            {
                // Thuế trước bạ: 12%, Thuế tiêu thụ đặc biệt: 30%
                return GiaGoc + (GiaGoc * 0.12m) + (GiaGoc * 0.30m);
            }
            else
            {
                // Xe trên 9 chỗ: Thuế trước bạ 10%
                return GiaGoc + (GiaGoc * 0.10m);
            }
        }

        // Ghi đè phương thức hiển thị thông tin
        public override string GetInfo()
        {
            return $"{base.GetInfo()} | Chỗ ngồi: {SoChoNgoi} chỗ | Động cơ: {DungTichDongCo:F1}L";
        }
    }
}