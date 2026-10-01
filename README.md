# BÁO CÁO BÀI TẬP LÝ THUYẾT LẬP TRÌNH C# & .NET CORE

---

## CÂU 1: PHÂN BIỆT VALUE TYPES VÀ REFERENCE TYPES (STACK VS HEAP)

Trong kiến trúc thực thi của .NET Common Language Runtime (CLR), mọi kiểu dữ liệu đều kế thừa từ lớp gốc `System.Object`. Tuy nhiên, cơ chế quản lý vòng đời và cấp phát bộ nhớ được chia thành hai nhóm riêng biệt: **Value Types** (Kiểu giá trị) và **Reference Types** (Kiểu tham chiếu).

### 1. Kiến trúc phân bổ bộ nhớ

```text
[ VÙNG NHỚ STACK ]                           [ VÙNG NHỚ MANAGED HEAP ]
+------------------------------------+       +------------------------------------+
| int x = 100                        |       |                                    |
| (Chứa trực tiếp giá trị nhị phân)  |       |                                    |
+------------------------------------+       |                                    |
| Product p (Con trỏ tham chiếu)     |------>| [Địa chỉ: 0x00A1F0]                |
| Lưu địa chỉ: 0x00A1F0              |       | - SyncBlockIndex                   |
+------------------------------------+       | - TypeHandle (Metadata con trỏ)    |
                                             | - Trường dữ liệu thực tế: Id, Name |
                                             +------------------------------------+
2. Chi tiết cơ chế lưu trữVùng nhớ Stack (Ngăn xếp):Hoạt động theo cơ chế LIFO (Last In, First Out), gắn liền trực tiếp với từng luồng thực thi (Thread-specific).Tốc độ truy xuất và cấp phát cực nhanh: CLR chỉ cần tịnh tiến con trỏ ngăn xếp (Stack Pointer).Tự động giải phóng: Biến được dọn sạch ngay khi luồng thực thi thoát khỏi phạm vi của hàm (Scope/Block code), không cần can thiệp dọn rác.Vùng nhớ Managed Heap (Đống quản lý):Dùng chung cho toàn bộ tiến trình ứng dụng (Process-wide), cấp phát động theo nhu cầu lúc chạy.Chứa dữ liệu thực tế của các đối tượng phức tạp.Quản lý bởi Garbage Collector (GC): Quét và giải phóng các vùng nhớ không còn con trỏ tham chiếu thông qua các thế hệ đối tượng (Generation 0, 1, 2 và Large Object Heap - LOH).3. Bảng so sánh toàn diệnTiêu chíValue Types (Kiểu giá trị)Reference Types (Kiểu tham chiếu)Lớp cơ sở kế thừaKế thừa từ System.ValueType (System.Object)Kế thừa trực tiếp từ System.ObjectCác kiểu tiêu biểuint, double, bool, char, decimal, struct, enumclass, string, interface, delegate, array (mảng)Vị trí lưu trữ thực tếStack (khi là biến cục bộ trong hàm) hoặc Heap (khi nằm nội tuyến bên trong một class/mảng)Managed Heap (dữ liệu đối tượng); Con trỏ/tham chiếu nằm ở StackCơ chế sao chép gán (=)Deep copy (Sao chép giá trị): Tạo một bản sao độc lập dữ liệu bit; thay đổi bản sao không ảnh hưởng bản gốcShallow copy (Sao chép tham chiếu): Chỉ nhân bản địa chỉ con trỏ; hai biến cùng trỏ về một vùng nhớ chungThu hồi bộ nhớGiải phóng tự động tức thì khi ra khỏi scopeDo trình thu gom rác Garbage Collector (GC) dọn dẹp định kỳGiá trị nullMặc định không thể gán null (phải dùng Nullable<T> hoặc T?)Mặc định có thể nhận giá trị nullChuyển đổi Boxing/UnboxingCó xảy ra khi ép kiểu qua lại với objectKhông có hiện tượng nàyCÂU 2: TÍNH NĂNG INIT-ONLY PROPERTIES (init) TRONG C# 9/101. Bản chất kỹ thuậtTrước C# 9, để bảo vệ tính bất biến của một thuộc tính, lập trình viên thường dùng bộ truy xuất get kết hợp Constructor (private set hoặc chỉ get). Cách này đòi hỏi phải viết constructor dài dòng khi class có nhiều trường.Thuộc tính dùng set thông thường: Cho phép gán hoặc ghi đè giá trị tại bất kỳ thời điểm nào trong toàn bộ vòng đời đối tượng.Thuộc tính dùng init (Init-only setter):Chỉ cho phép gán dữ liệu duy nhất trong quá trình khởi tạo đối tượng (bên trong Constructor hoặc qua cú pháp Object Initializer).Sau khi đối tượng hoàn tất việc khởi tạo, thuộc tính lập tức trở thành Read-only (Bất biến).Nếu cố tình gán lại giá trị ở các dòng code tiếp theo, trình biên dịch sẽ chặn và báo lỗi ngay tại Compile-time (Lỗi CS8852).Ở tầng IL (Intermediate Language), trình biên dịch áp dụng cơ chế đánh dấu modreq (System.Runtime.CompilerServices.IsExternalInit), ngăn chặn các lời gọi setter thông thường nhưng vẫn an toàn khi thực thi reflection.2. Ví dụ minh họaC#public class UserDto
{
    // Dùng init: Chỉ đọc sau khi khởi tạo
    public string Id { get; init; }
    
    // Dùng set: Trạng thái mở, có thể thay đổi bất cứ lúc nào
    public string Address { get; set; }
}

class Program
{
    static void Main()
    {
        // Khởi tạo hợp lệ với cú pháp Object Initializer
        var user = new UserDto 
        { 
            Id = "USR_001", 
            Address = "Hà Nội" 
        };

        user.Address = "Đà Nẵng"; // HỢP LỆ: set cho phép sửa
        
        // user.Id = "USR_999"; 
        // LỖI BIÊN DỊCH CS8852: Init-only property cannot be assigned after initialization!
    }
}
3. Trường hợp sử dụng thực tế (Use Cases)Thiết kế Data Transfer Object (DTO) & ViewModels: Đọc dữ liệu từ Cơ sở dữ liệu hoặc External API đưa vào đối tượng một lần duy nhất, đảm bảo tính toàn vẹn khi dữ liệu đi qua các tầng nghiệp vụ (Layers) mà không lo bị ghi đè ngoài ý muốn.Bảo vệ khóa chính & trường định danh: Các mã ID, số Căn cước công dân (CCCD), Transaction GUID chỉ được nạp một lần duy nhất lúc tạo instance.An toàn trong môi trường đa luồng (Thread Safety): Các đối tượng bất biến (Immutable Objects) an toàn tự nhiên khi nhiều luồng truy cập cùng lúc, không phát sinh xung đột (Race conditions) và không cần sử dụng cơ chế khóa (lock).CÂU 3: PHƯƠNG THỨC virtual Ở LỚP CHA VÀ override Ở LỚP CONCặp từ khóa virtual và override là cốt lõi để triển khai cơ chế Đa hình tại thời điểm chạy (Runtime / Dynamic Polymorphism) thông qua cơ chế bảng ảo (V-Table - Virtual Method Table).1. Phân biệt vai tròPhương thức virtual (Khai báo tại Lớp Cha):Cung cấp một hành vi mặc định có sẵn thân hàm ({ ... }).Mở quyền cho các lớp dẫn xuất (con) có thể thay thế cách hoạt động nếu lớp con có yêu cầu đặc thù. Lớp con không bắt buộc phải viết lại phương thức này.Phương thức override (Khai báo tại Lớp Con):Cung cấp một hành vi chuyên biệt hóa, ghi đè hoàn toàn thuật toán mặc định của lớp cha.Chữ ký phương thức (Tên hàm, kiểu dữ liệu trả về, danh sách tham số) phải trùng khớp tuyệt đối với phương thức virtual (hoặc abstract) của lớp cha.2. Cơ chế hoạt động của Virtual Method Table (V-Table)PlaintextBiến tham chiếu: PhuongTien pt = new OTo();
                                 |
                                 V (Trỏ vào đối tượng thực tế trên Heap)
                      +----------------------+
                      |    Đối tượng OTo     |
                      |----------------------|
                      | Type Pointer         |-----> [ V-Table của Class OTo ]
                      | Data Fields...       |       | PhuongTien.GetInfo()  |--> Địa chỉ hàm OTo.GetInfo() [Ghi đè]
                      +----------------------+
Khi biên dịch một lớp có phương thức virtual, CLR sinh ra một V-Table chứa các con trỏ trỏ đến địa chỉ thực thi của từng phương thức ảo.Khi lớp con sử dụng override, con trỏ trong V-Table của lớp con sẽ được trỏ hướng lại (rewired) đến đoạn code mới được định nghĩa tại lớp con.Khi chương trình chạy, nhờ cơ chế Late Binding (Liên kết muộn), CLR tra cứu trực tiếp V-Table của đối tượng thực tế trên Heap để kích hoạt đúng phương thức của lớp con thay vì phương thức của lớp cha.3. Tóm tắt so sánhTiêu chívirtual (Lớp Cơ Sở / Cha)override (Lớp Dẫn Xuất / Con)Vị trí định nghĩaChỉ nằm ở lớp cha (Base class)Chỉ nằm ở lớp con (Derived class)Mục đíchKhai báo hành vi mặc định và cho phép tùy biếnTriển khai hành vi cụ thể thay thế cho lớp chaTính bắt buộc ở lớp conTùy chọn (Optional), không bắt buộc phải viết lạiChỉ khai báo khi muốn ghi đè phương thức chaKhả năng kế thừa tiếpCho phép các lớp kế thừa tiếp tục overrideCó thể dùng sealed override để chặn các lớp con phía sau ghi đèCÂU 4: TẠI SAO THÀNH PHẦN static KHÔNG THỂ TRUY XUẤT QUA INSTANCE ĐƯỢC TẠO BẰNG new?Trong ngôn ngữ C#, trình biên dịch cấm tuyệt đối việc truy xuất thành phần static thông qua biến thể hiện (instance.StaticMember). Cố tình thực hiện sẽ nhận thông báo lỗi: CS0176: Member cannot be accessed with an instance reference; qualify it with a type name instead.Lý do đến từ hai khía cạnh:1. Kiến trúc phân bổ bộ nhớ của CLRInstance Member (Thành phần đối tượng):Được cấp phát riêng rẽ cho từng đối tượng trên Managed Heap mỗi khi toán tử new được gọi.Khi thực thi, phương thức instance luôn nhận ngầm định một tham số con trỏ là this trỏ trực tiếp đến địa chỉ của đối tượng đang thao tác.Static Member (Thành phần tĩnh):Thuộc phạm vi Type-level (Mức định nghĩa kiểu dữ liệu), không phụ thuộc vào bất kỳ đối tượng cụ thể nào.Chỉ được cấp phát một lần duy nhất vào vùng nhớ tĩnh đặc biệt (High Frequency Heap / AppDomain) ngay thời điểm nạp kiểu dữ liệu (Type Loading).Phương thức static hoàn toàn không có con trỏ this.Bản thân instance tạo bằng new không chứa biến static trong cấu trúc bộ nhớ của nó, do đó việc gọi thành phần static qua instance là sai lệch về vị trí quản lý dữ liệu.2. Triết lý thiết kế ngôn ngữ C#Minh bạch hóa ngữ nghĩa (Eliminate Ambiguity):Nếu cho phép cú pháp oto1.DemSoLuong(), người đọc code sẽ dễ lầm tưởng hàm này chỉ đếm riêng cho xe oto1.C# ép buộc phải viết OTo.DemSoLuong() để khẳng định thao tác này tác động đến trạng thái toàn cục của cả Class OTo.Tối ưu hóa hiệu năng biên dịch (Direct Invocation):Lời gọi qua tên lớp (ClassName.StaticMethod()) được phân giải trực tiếp ngay tại thời điểm biên dịch (Direct Static Call).CLR không cần thực hiện kiểm tra con trỏ rỗng (Null Reference Check) trên biến instance và không cần tra cứu bảng ảo V-Table, giúp mã máy chạy với tốc độ tối đa.
