(function ($) {
    const $editModal = $('#CategoryEditModal');

    // Tự động khởi tạo validation khi modal HTML được render
    function initValidation() {
        const $editForm = $('#CategoryEditForm');
        if ($.fn.valid && $editForm.length) {
            $editForm.validate({
                rules: {
                    Name: {
                        required: true,
                        maxlength: 100
                    }
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

    // Xóa invalid state khi người dùng gõ
    $(document).on('input', '#CategoryEditForm input[name="Name"]', function () {
        const $input = $(this);
        $input.removeClass('is-invalid');
        $input.siblings('.invalid-feedback').hide();
    });

    // Chạy validate sau khi AJAX tải HTML Modal thành công
    $editModal.on('shown.bs.modal', function () {
        initValidation();
    });

    // Xử lý submit form sửa
    $(document).on('submit', '#CategoryEditForm', function (e) {
        e.preventDefault();

        const $editForm = $(this);
        const $nameInput = $editForm.find('input[name="Name"]');

        if (!$editForm.valid()) {
            return;
        }

        const category = {
            id: $editForm.find('input[name="Id"]').val(),
            name: $nameInput.val().trim(),
        };

        abp.ui.setBusy($editModal);

        abp.ajax({
            url: abp.appPath + 'Categories/Update',
            type: 'PUT',
            data: JSON.stringify(category),
            abpHandleError: false
        }).done(function () {
            $editModal.modal('hide');
            
            if (window.Categories && window.Categories.dataTable) {
                window.Categories.dataTable.ajax.reload();
            }

            abp.notify.info('Cập nhật danh mục thành công');
        }).fail(function (error) {
            $nameInput.addClass('is-invalid');

            let $errorSpan = $nameInput.siblings('.invalid-feedback');
            if (!$errorSpan.length) {
                $nameInput.after('<span class="invalid-feedback"></span>');
                $errorSpan = $nameInput.siblings('.invalid-feedback');
            }

            const errorMessage = error && error.message
                ? error.message
                : 'Tên danh mục đã tồn tại trong hệ thống';

            $errorSpan.text(errorMessage).show();
        }).always(function () {
            abp.ui.clearBusy($editModal);
        });
    });

})(jQuery);