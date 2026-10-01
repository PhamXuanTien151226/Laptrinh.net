# Laptrinh.net
Câu 1: Trình bày sự khác nhau giữa Value Types và Reference Types trong C# về cơ chế lưu trữ vùng nhớ (Stack vs Heap)
Trong kiến trúc thực thi của .NET Common Language Runtime (CLR), hệ thống phân loại dữ liệu thành hai nhánh lớn bắt nguồn từ lớp cơ sở System.Object: Value Types (kế thừa từ System.ValueType) và Reference Types (kế thừa trực tiếp từ System.Object). Sự khác biệt cốt lõi nằm ở cách thức và vị trí cấp phát vùng nhớ:
[Stack]                                    [Managed Heap]
+-------------------------------+          +-------------------------------+
| int a = 10 (Chứa giá trị trực tiếp) |          |                               |
+-------------------------------+          |                               |
| objRef (Chứa địa chỉ 0x00FF4A) |--------->| 0x00FF4A: Object Data         |
+-------------------------------+          |  - SyncBlockIndex / TypeHandle|
                                           |  - Các trường dữ liệu thực tế |
                                           +-------------------------------+
1. Vùng nhớ Stack và Managed Heap
•	Vùng nhớ Stack (Ngăn xếp):
o	Là vùng nhớ có cấu trúc LIFO (Last In, First Out), được gắn liền trực tiếp với từng luồng thực thi (Thread-specific).
o	Tốc độ truy xuất và cấp phát cực nhanh: Hệ thống chỉ cần tịnh tiến con trỏ ngăn xếp (Stack Pointer).
o	Vòng đời ngắn: Biến bị hủy tự động và giải phóng ngay khi luồng thực thi thoát khỏi phạm vi (scope) của hàm/khối lệnh, hoàn toàn không cần sự can thiệp của bộ thu gom rác.
•	Vùng nhớ Managed Heap (Đống quản lý):
o	Là vùng nhớ dùng chung toàn bộ tiến trình (Process-wide), cấp phát động theo yêu cầu tại runtime.
o	Tốc độ cấp phát chậm hơn Stack do CLR phải tìm kiếm khoảng trống, phân đoạn các thế hệ đối tượng (Gen 0, Gen 1, Gen 2, Large Object Heap - LOH).
o	Vòng đời do Garbage Collector (GC) quản lý: Khi không còn con trỏ tham chiếu nào trỏ tới, đối tượng sẽ được đánh dấu để dọn dẹp trong các chu kỳ thu gom.
2. Cơ chế lưu trữ chi tiết
•	Value Types (Kiểu giá trị):
o	Bản chất: Biến chứa trực tiếp giá trị thực tế của dữ liệu.
o	Vị trí lưu trữ:
	Khi là biến cục bộ (local variable) hoặc tham số trong một phương thức: Lưu trực tiếp trên Stack.
	Khi là thuộc tính/trường (field) của một Class hoặc phần tử của một mảng tham chiếu: Nằm nội tuyến (inline) bên trong cấu trúc của đối tượng đó trên Managed Heap.
o	Cơ chế gán/sao chép: Thực hiện phép sao chép theo giá trị (Copy-by-value / Bitwise copy). Thao tác biến đổi trên bản sao hoàn toàn không ảnh hưởng đến biến gốc.
•	Reference Types (Kiểu tham chiếu):
o	Bản chất: Biến không chứa dữ liệu thực tế mà chỉ chứa một con trỏ/địa chỉ bộ nhớ (Memory Address) trỏ đến nơi lưu trữ đối tượng.
o	Vị trí lưu trữ: Bản thân dữ liệu đối tượng (gồm Type Object Pointer, Sync Block Index và các fields) luôn nằm trên Managed Heap. Biến tham chiếu (chứa địa chỉ) nằm trên Stack (nếu là biến cục bộ) hoặc nằm trong lòng một đối tượng khác trên Heap.
o	Cơ chế gán/sao chép: Thực hiện sao chép địa chỉ tham chiếu (Copy-by-reference / Shallow copy). Sau khi gán, cả hai biến cùng trỏ vào một vùng nhớ trên Heap. Thay đổi dữ liệu qua một biến sẽ phản ánh trực tiếp lên biến còn lại.
3. Bảng tổng hợp so sánh
Tiêu chí so sánh	Value Types	Reference Types
Lớp cơ sở kế thừa	System.ValueType (kế thừa từ System.Object)	Kế thừa trực tiếp System.Object
Các kiểu dữ liệu tiêu biểu	int, double, bool, char, decimal, struct, enum	class, string, interface, delegate, mảng (array)
Vị trí cấp phát chính	Stack (khi là local var) hoặc inline trên Heap (khi là field)	Dữ liệu luôn ở Heap; con trỏ ở Stack
Hành vi khi gán (=)	Sao chép toàn bộ giá trị (độc lập bộ nhớ)	Chỉ sao chép địa chỉ con trỏ (dùng chung dữ liệu)
Cơ chế dọn dẹp bộ nhớ	Tự động giải phóng khi ra khỏi scope	Do Garbage Collector (GC) tự động quét và thu gom
Khả năng nhận giá trị null	Không (Mặc định không null; phải dùng Nullable<T> / T?)	Có thể mang giá trị null mặc định
Hiện tượng Boxing / Unboxing	Xảy ra khi chuyển đổi qua lại giữa Value Type và object	Không có hiện tượng này
Câu 2: Tính năng Init-only Properties (init) trong C# 9/10 khác gì so với set thông thường? Nêu trường hợp sử dụng thực tế
1. Bản chất kỹ thuật và sự khác biệt
•	Thuộc tính có set thông thường (Mutable):
o	Cho phép gán hoặc sửa đổi giá trị của thuộc tính ở bất kỳ thời điểm nào trong suốt vòng đời của đối tượng, miễn là thỏa mãn phạm vi truy cập (access modifier).
o	Đối tượng có trạng thái mở, dễ bị thay đổi ngẫu nhiên từ bên ngoài luồng nghiệp vụ.
•	Thuộc tính có init (Init-only Accessor):
o	Được giới thiệu từ C# 9, init là một biến thể của set nhưng có ràng buộc khắt khe: Chỉ cho phép gán dữ liệu trong quá trình khởi tạo đối tượng.
o	Các ngữ cảnh được phép gán:
	Bên trong thân Constructor của lớp hiện tại hoặc lớp kế thừa.
	Thông qua cú pháp khởi tạo đối tượng (Object Initializer syntax - { Prop = value }).
	Thông qua biểu thức khởi tạo with (trong record).
o	Sau khi quá trình khởi tạo hoàn tất, thuộc tính sẽ trở thành Read-only (Bất biến). Nếu có bất kỳ dòng code nào cố tình gán lại giá trị, trình biên dịch sẽ báo lỗi ngay tại Compile-time (Lỗi CS8852).
o	Bản chất bên dưới CLR: Trình biên dịch C# gắn cờ chỉ thị modreq (modifier requirement) IsExternalInit lên phương thức setter nội bộ, ngăn chặn lời gọi thông thường nhưng vẫn mở cho cơ chế reflection cấp cao.
2. So sánh trực quan bằng Code
C#
public class NhanVien
{
    // Dùng set: Bị sửa đổi bất kỳ lúc nào
    public decimal LuongCoBan { get; set; }

    // Dùng init: Chỉ gán lúc tạo đối tượng, sau đó bất biến
    public string CCCD { get; init; }
}

// Thực thi:
var nv = new NhanVien 
{ 
    CCCD = "001200001234",  // HỢP LỆ (Gán qua Object Initializer)
    LuongCoBan = 10000000 
};

nv.LuongCoBan = 12000000;   // HỢP LỆ (set cho phép đổi)
// nv.CCCD = "001200009999"; // LỖI BIÊN DỊCH CS8852: Init-only property cannot be assigned!
3. Trường hợp sử dụng thực tế (Use Cases)
•	Xây dựng DTO (Data Transfer Object) và ViewModels: Khi đọc dữ liệu từ Database, Web API hoặc file JSON, ta cần map dữ liệu vào object một lần duy nhất rồi gửi đi. Dùng init đảm bảo dữ liệu qua các tầng xử lý (Layers) không bị các hàm trung gian vô tình sửa đổi làm sai lệch nghiệp vụ.
•	Bảo vệ tính toàn vẹn của Định danh (Identity Fields): Các trường như Mã định danh căn cước (CCCD), Mã số sinh viên, GUID của Transaction, Khóa chính (Primary Key) phải bất biến sau khi sinh ra.
•	Lập trình an toàn đa luồng (Thread-safety): Các đối tượng bất biến (Immutable Objects) an toàn tuyệt đối khi chia sẻ giữa nhiều luồng chạy song song mà không cần cơ chế khóa (lock), loại bỏ hoàn toàn nguy cơ tranh chấp tài nguyên (Race Condition).
Câu 3: Phân biệt sự khác nhau giữa phương thức virtual ở lớp cha và phương thức override ở lớp con khi triển khai tính Đa hình (Polymorphism)
Đa hình tại thời điểm chạy (Runtime Polymorphism hay Dynamic Polymorphism) trong C# được hiện thực hóa dựa trên cặp từ khóa virtual và override cùng cơ chế bảng ảo (V-Table - Virtual Method Table).
1. Định nghĩa và Vai trò
•	Phương thức virtual (Khai báo tại Lớp Cha):
o	Đóng vai trò là phương thức gốc, cung cấp một hành vi mặc định.
o	Phát tín hiệu cho CLR và các lớp kế thừa rằng: "Phương thức này mở, cho phép lớp con định nghĩa lại cách hoạt động nếu cần thiết". Lớp con không bắt buộc phải viết lại; nếu không viết lại, lớp con sẽ dùng luôn code mặc định này.
•	Phương thức override (Khai báo tại Lớp Con):
o	Đóng vai trò là phương thức ghi đè, cung cấp một hành vi chuyên biệt hóa.
o	Bắt buộc chữ ký phương thức (tên hàm, kiểu trả về, danh sách tham số) phải trùng khớp 100% với phương thức virtual (hoặc abstract) ở lớp cha.
2. Cơ chế hoạt động của Virtual Method Table (V-Table)
•	Khi một lớp chứa phương thức virtual, trình biên dịch sẽ tạo ra cho lớp đó một V-Table chứa danh sách con trỏ hàm trỏ đến code thực thi.
•	Khi lớp con dùng override, địa chỉ con trỏ hàm trong V-Table của lớp con sẽ được trỏ lại (rewrite) sang vùng nhớ code của phương thức mới ở lớp con.
•	Tại Runtime, cơ chế Late Binding (Liên kết muộn) được kích hoạt: Khi gọi phương thức qua một con trỏ kiểu cha (PhuongTien pt = new OTo()), CLR sẽ không nhìn vào kiểu khai báo (PhuongTien) mà tra cứu vào V-Table của đối tượng thực tế trên Heap (OTo) để nhảy đến đúng hàm override.
3. Bảng phân biệt
Tiêu chí	Phương thức virtual (Lớp Cha)	Phương thức override (Lớp Con)
Vị trí xuất hiện	Chỉ khai báo tại lớp cơ sở (Base Class).	Chỉ khai báo tại lớp dẫn xuất (Derived Class).
Tính bắt buộc thực thi	Có sẵn thân hàm ({ ... }), cung cấp giải pháp mặc định.	Cung cấp thân hàm mới để thay thế hành vi của lớp cha.
Tính ràng buộc ở lớp con	Lớp con không bắt buộc phải ghi đè (tùy chọn).	Chỉ viết khi lớp cha có phương thức virtual hoặc abstract.
Tái sử dụng code cha	Tự thực hiện logic nội tại.	Có thể tái sử dụng code của cha thông qua từ khóa base.Method().
Câu 4: Tại sao một thành phần được khai báo là static trong Lớp (Class) lại không thể truy xuất thông qua một thể hiện (Object Instance) được tạo bằng toán tử new?
Trong một số ngôn ngữ như Java, bạn có thể gọi instance.staticMethod() (dù trình biên dịch sẽ đưa ra cảnh báo). Tuy nhiên, trong C#, điều này bị cấm hoàn toàn ở mức biên dịch (Compiler Error CS0176). Lý do xuất phát từ cả kiến trúc quản lý bộ nhớ của CLR lẫn triết lý thiết kế ngôn ngữ hướng đối tượng:
1. Nguyên nhân kiến trúc bộ nhớ (Memory Allocation)
•	Instance Members (Thành phần thể hiện):
o	Được cấp phát riêng biệt cho từng đối tượng trên Managed Heap mỗi khi lệnh new được thực thi.
o	Mỗi đối tượng sở hữu một bản sao dữ liệu riêng và được truyền ngầm định tham chiếu this vào các phương thức instance để thao tác đúng dữ liệu của thể hiện đó.
•	Static Members (Thành phần tĩnh):
o	Thuộc về phạm vi Type-level (Mức định nghĩa kiểu) chứ không thuộc về bất kỳ đối tượng cụ thể nào.
o	Được cấp phát một lần duy nhất vào một vùng nhớ đặc biệt (AppDomain / High Frequency Heap) khi lớp đó được nạp lần đầu tiên vào bộ nhớ (Type Loading).
o	Không bao giờ có con trỏ this. Do đó, một instance không chứa, không quản lý và không sở hữu biến static. Việc dùng biến thể hiện để gọi thành phần tĩnh là hoàn toàn sai lệch về bản chất vị trí cấp phát.
2. Nguyên nhân từ triết lý thiết kế C# (Design Philosophy)
•	Xóa bỏ tính nhập nhằng (Ambiguity): Nếu cho phép gọi sp1.DemSoLuong(), người đọc mã nguồn rất dễ nhầm lẫn rằng hàm này chỉ đếm riêng cho sp1, trong khi thực tế nó đang thao tác trên biến dùng chung của toàn bộ ứng dụng. C# ép buộc phải viết SanPham.DemSoLuong() để code tường minh 100%.
•	Tối ưu hóa mã máy (Call Optimization): Lời gọi static được CLR phân giải trực tiếp ngay tại thời điểm biên dịch (Direct Call) trỏ thẳng vào địa chỉ hàm tĩnh thông qua Metadata, bỏ qua hoàn toàn bước kiểm tra null con trỏ đối tượng (Null check) và tra cứu bảng ảo V-Table, mang lại hiệu năng tối đa.

