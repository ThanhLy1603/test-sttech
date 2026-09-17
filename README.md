Dưới đây là file **`README.md`** hoàn chỉnh, đầy đủ và chuyên nghiệp dành cho dự án của bạn (hệ thống Quản lý Sách và Danh mục sử dụng **ASP.NET Core / ABP Framework**, **Entity Framework Core**, **Bootstrap 4/5**, **jQuery Validation**, và **DataTables**):

---

# QUẢN LÝ SÁCH

Hệ thống quản lý **Sách** và **Danh mục** được xây dựng trên nền tảng **ASP.NET Core** kết hợp với **ABP Framework**. Dự án áp dụng mô hình chuẩn kiến trúc đa tầng (Layered Architecture), render phía Server-side với **Razor Views** và **AJAX/DataTables** cho trải nghiệm người dùng mượt mà không cần tải lại trang.

---

## Công nghệ sử dụng (Tech Stack)

### **Backend**

* **Framework:** ASP.NET Core (.NET Core / .NET Framework)
* **Application Framework:** ABP Framework (ASP.NET Boilerplate)
* **ORM:** Entity Framework Core
* **DTO & Mapping:** AutoMapper
* **Database:** SQL Server

### **Frontend**

* **View Engine:** Razor Pages / MVC Views (`.cshtml`)
* **UI Library:** Bootstrap 4/5 (AdminLTE 3)
* **JavaScript / Query:** jQuery, jQuery Validation (`jquery.validate.js`)
* **Data Presentation:** DataTables (Server-side processing)
* **Icons:** FontAwesome 5+
* **Alerts & UI Helpers:** ABP UI Block (`abp.ui.setBusy`), ABP Notifications (`abp.notify`), ABP Dialogs (`abp.message`)

---

## Tính năng chính

### 1. Quản lý Danh mục (Categories)

* **Danh sách Danh mục:** Hiển thị danh sách danh mục theo dạng bảng phân trang (Server-side) sử dụng DataTables.
* **Tìm kiếm:** Tìm kiếm danh mục theo từ khóa (tên danh mục).
* **Thêm mới Danh mục:**
* Validate client-side (Bắt buộc nhập, giới hạn độ dài 100 ký tự).
* Xử lý trùng tên phía server và hiển thị lỗi trực tiếp dưới ô input.


* **Cập nhật Danh mục:** Load dữ liệu động vào Modal bằng AJAX và cập nhật không tải lại trang.
* **Xóa Danh mục:** Xác nhận trước khi xóa bằng ABP Confirm Dialog.

### 2. Quản lý Sách (Books)

* **Danh sách Sách:** Hiển thị Tên sách, Tác giả, Giá sách (đã format chuẩn tiền tệ `VNĐ`), và Tên danh mục liên kết.
* **Thêm mới Sách:**
* Chọn danh mục từ danh sách dropdown.
* Validate các trường thông tin: Tên sách (max 100 ký tự), Tác giả (max 100 ký tự), Giá sách (min 0, max 999.999.999 VNĐ).


* **Cập nhật Sách:**
* Render giao diện chỉnh sửa động (`_EditModal.cshtml`).
* Tự động chọn đúng Danh mục hiện tại của sách.


* **Xóa Sách:** Cảnh báo xác nhận xóa theo tên sách.
* **Tìm kiếm:** Tìm kiếm theo từ khóa (Tên sách hoặc Tác giả).

---

## Cấu trúc thư mục Frontend chính

```text
wwwroot/
└── view-resources/
    └── Views/
        ├── Categories/
        │   ├── Index.js          # Logic DataTables, Thêm mới, Xóa Danh mục
        │   └── _EditModal.js     # Validate và submit form Cập nhật Danh mục
        └── Books/
            ├── Index.js          # Logic DataTables, Thêm mới, Xóa Sách
            └── _EditModal.js     # Validate và submit form Cập nhật Sách

Views/
├── Categories/
│   ├── Index.cshtml              # Trang quản lý Danh mục
│   └── _EditModal.cshtml         # Partial view Modal sửa Danh mục
└── Books/
    ├── Index.cshtml              # Trang quản lý Sách
    └── _EditModal.cshtml         # Partial view Modal sửa Sách

```

---

## Hướng dẫn cài đặt & Chạy dự án

### **1. Yêu cầu hệ thống**

* .NET SDK 6.0 / 7.0 / 8.0 (tùy thuộc phiên bản dự án)
* SQL Server (hoặc SQL Express / LocalDB)
* Visual Studio 2022 / Visual Studio Code

### **2. Các bước cấu hình**

1. **Clone repository về máy:**
```bash
git clone https://github.com/your-username/your-repo-name.git
cd your-repo-name

```


2. **Cấu hình Chuỗi kết nối Database (Connection String):**
Mở file `appsettings.json` trong dự án `EntityFrameworkCore` hoặc `Web.Host` / `Web.Mvc` và cập nhật:
```json
"ConnectionStrings": {
  "Default": "Server=localhost; Database=DemoProjectDb; Trusted_Connection=True; TrustServerCertificate=True;"
}

```


3. **Cập nhật Database (Migration):**
Mở **Package Manager Console** trong Visual Studio chọn project `.EntityFrameworkCore` làm Default Project:
```powershell
Update-Database

```


*Hoặc chạy lệnh CLI:*
```bash
dotnet ef database update

```


4. **Chạy dự án:**
Chạy project `Web.Mvc` (hoặc `Web.Host`):
```bash
dotnet run

```


Sau đó truy cập trình duyệt theo địa chỉ: `https://localhost:44301` (hoặc cổng mà ứng dụng cấp).

---

## Các quy chuẩn chính trong Mã nguồn JavaScript

1. **Phân tách trách nhiệm (Separation of Concerns):**
* Code JS được tách riêng giữa trang danh sách chính (`Index.js`) và Modal động (`_EditModal.js`).


2. **Quản lý Namespace:**
* Xuất biến `dataTable` ra global window (ví dụ: `window.Books.dataTable`) để file `_EditModal.js` có thể tương tác và `reload()` lại bảng sau khi cập nhật thành công.


3. **Validation & UI:**
* Sử dụng `jquery.validate.js` kết hợp với class `is-invalid` của Bootstrap.
* Lỗi từ Server trả về (như trùng tên) được bắt và append linh hoạt vào khối `invalid-feedback` bên dưới trường nhập liệu.
