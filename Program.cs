using System;

namespace LogisticsAutoSpeed
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("╔═════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║   HỆ THỐNG QUẢN LÝ PHƯƠNG TIỆN - LOGISTICS AUTOSPEED        ║");
            Console.WriteLine("╚═════════════════════════════════════════════════════════════╝\n");

            // TC01: Kiểm tra Validation Năm sản xuất (Ngoại lệ 1850)
            Console.WriteLine(">>> [TEST CASE 01]: Kiểm tra Validation Năm sản xuất (1850)...");
            try
            {
                var oToLoi = new OTo("OT001", "Toyota", 1850, 800_000_000m, 5, 2.0);
                Console.WriteLine("Thất bại: Đối tượng vẫn được tạo!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($" -> KẾT QUẢ: Hệ thống đã chặn và ném ngoại lệ thành công!");
                Console.WriteLine($" -> Thông điệp lỗi: \"{ex.Message}\"");
            }

            // TC02: Kiểm tra Giá lăn bánh Ô tô (5 chỗ, 1 tỷ VNĐ)
            Console.WriteLine("\n>>> [TEST CASE 02]: Tính Giá Lăn Bánh Ô tô 5 chỗ (Giá gốc: 1,000,000,000 VNĐ)...");
            var oto5Cho = new OTo("OT002", "Mercedes C300", 2023, 1_000_000_000m, 5, 2.0);
            decimal giaLanBanhOto = oto5Cho.TinhGiaLanBanh();
            Console.WriteLine($" -> Giá lăn bánh thực tế: {giaLanBanhOto:N0} VNĐ");
            Console.WriteLine($" -> Kết quả kiểm tra: {(giaLanBanhOto == 1_420_000_000m ? "ĐẠT CHUẨN (1.42 tỷ)" : "SAI")}");

            // TC03: Kiểm tra Giá lăn bánh Xe máy (150cc, 50 triệu VNĐ)
            Console.WriteLine("\n>>> [TEST CASE 03]: Tính Giá Lăn Bánh Xe máy 150cc (Giá gốc: 50,000,000 VNĐ)...");
            var sh150 = new XeMay("XM001", "Honda SH", 2022, 50_000_000m, 150);
            decimal giaLanBanhXeMay = sh150.TinhGiaLanBanh();
            Console.WriteLine($" -> Giá lăn bánh thực tế: {giaLanBanhXeMay:N0} VNĐ");
            Console.WriteLine($" -> Kết quả kiểm tra: {(giaLanBanhXeMay == 51_000_000m ? "ĐẠT CHUẨN (51 triệu)" : "SAI")}");

            // TC04: Kiểm tra Đa hình trong List<PhuongTien>
            Console.WriteLine("\n>>> [TEST CASE 04]: Kiểm tra tính Đa hình với List<PhuongTien>...");
            var ql = new QuanLyPhuongTien();
            ql.AddPhuongTien(oto5Cho);
            ql.AddPhuongTien(sh150);
            ql.AddPhuongTien(new OTo("OT003", "Ford Transit", 2021, 900_000_000m, 16, 2.4));
            ql.AddPhuongTien(new XeMay("XM002", "Yamaha R3", 2023, 130_000_000m, 321));
            ql.DisplayAll();

            // TC05: Kiểm tra Tìm Phương tiện có Giá lăn bánh cao nhất
            Console.WriteLine(">>> [TEST CASE 05]: Tìm xe có Giá lăn bánh cao nhất...");
            var maxPt = ql.FindMaxGiaLanBanh();
            if (maxPt != null)
            {
                Console.WriteLine($" -> Xe có giá lăn bánh cao nhất: {maxPt.TenHang} ({maxPt.MaPT})");
                Console.WriteLine($" -> Tổng giá trị lăn bánh: {maxPt.TinhGiaLanBanh():N0} VNĐ");
                Console.WriteLine($" -> Đánh giá: {(maxPt.MaPT == "OT002" ? "ĐẠT (Chính xác là Mercedes C300)" : "SAI")}");
            }

            Console.WriteLine("\nBấm phím bất kỳ để kết thúc...");
            Console.ReadKey();
        }
    }
}