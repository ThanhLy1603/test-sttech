(function ($) {
    const $editModal = $('#BookEditModal');

    // Hàm khởi tạo Validate cho Form Edit
    function initValidation() {
        const $editForm = $('#BookEditForm');
        if ($.fn.valid && $editForm.length) {
            $editForm.validate({
                rules: {
                    Title: { required: true, maxlength: 100 },
                    Author: { required: true, maxlength: 100 },
                    Price: { required: true, min: 0, max: 999999999 },
                    CategoryId: { required: true }
                },
                messages: {
                    Title: { required: "Vui lòng nhập tên sách.", maxlength: "Tên sách không được vượt quá 100 ký tự." },
                    Author: { required: "Vui lòng nhập tên tác giả.", maxlength: "Tên tác giả không được vượt quá 100 ký tự." },
                    Price: { required: "Vui lòng nhập giá sách.", min: "Giá sách không được nhỏ hơn 0 đ.", max: "Giá sách quá lớn." },
                    CategoryId: { required: "Vui lòng chọn danh mục." }
                },
                highlight: function (element) {
                    $(element).addClass('is-invalid');
                },
                unhighlight: function (element) {
                    $(element).removeClass('is-invalid');
                },
                errorElement: 'span',
                errorClass: 'invalid-feedback'
            });
        }
    }

    // Tự động kích hoạt validation ngay sau khi HTML được nạp vào Modal
    $editModal.on('shown.bs.modal', function () {
        initValidation();
    });

    // Clear lỗi khi người dùng thay đổi dữ liệu trên Form Edit
    $(document).on('input change', '#BookEditForm input, #BookEditForm select', function () {
        const $input = $(this);
        $input.removeClass('is-invalid');
        $input.siblings('.invalid-feedback').hide();
    });

    // Xử lý Cập nhật sách (Submit Edit Form)
    $(document).on('submit', '#BookEditForm', function (event) {
        event.preventDefault();

        const $editForm = $(this);
        const $titleInput = $editForm.find('input[name="Title"]');

        if (!$editForm.valid()) return;

        const book = $editForm.serializeFormToObject();

        abp.ui.setBusy($editModal);

        abp.ajax({
            url: abp.appPath + 'Books/Update',
            type: 'PUT',
            contentType: 'application/json',
            data: JSON.stringify(book),
            abpHandleError: false,
            error: function () {}
        }).done(function () {
            $editModal.modal('hide');

            // Load lại DataTables ở trang Index
            if (window.Books && window.Books.dataTable) {
                window.Books.dataTable.ajax.reload();
            }

            abp.notify.info('Cập nhật thành công');
        }).fail(function (error) {
            $titleInput.addClass('is-invalid');

            let $errorSpan = $titleInput.siblings('.invalid-feedback');
            if (!$errorSpan.length) {
                $titleInput.after('<span class="invalid-feedback"></span>');
                $errorSpan = $titleInput.siblings('.invalid-feedback');
            }

            const errorMessage = error && error.message
                ? error.message
                : 'Tên sách đã tồn tại trong hệ thống';

            $errorSpan.text(errorMessage).show();
        }).always(function () {
            abp.ui.clearBusy($editModal);
        });
    });

})(jQuery);