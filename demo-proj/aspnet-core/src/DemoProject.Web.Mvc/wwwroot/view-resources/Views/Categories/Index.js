(function ($) {

    const $table = $('#CategoriesTable'),
        $createModal = $('#CategoryCreateModal'),
        $editModal = $('#CategoryEditModal');
    
    // Khởi tạo validation cho Form thêm mới
    const $CreateForm = $('form[name="CategoryCreateForm"]');
    if ($.fn.valid && $CreateForm.length) {
        $CreateForm.validate({
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
            errorClass: 'invalid-feedback',
        });
    }

    $(document).on('input', 'form[name="CategoryCreateForm"] input[name="Name"], #CategoryEditForm input[name="Name"]', function () {
        const $input = $(this);
        $input.removeClass('is-invalid');
        $input.siblings('.invalid-feedback').hide();
    });

    const dataTable = $table.DataTable({
        paging: true,
        serverSide: true,
        processing: true,
        searching: false,
        lengthMenu: [5, 10, 15, 20, 25],
        pageLength: 5,

        ajax: function (data, callback, settings) {
            const input = {
                filter: ($('#SearchKeyword').val() || '').trim(),
                skipCount: data.start || 0,
                maxResultCount: data.length || 5,
                sorting: 'Name ASC'
            };

            abp.ajax({
                url: abp.appPath + 'Categories/GetAll?' + $.param(input),
                type: 'GET'
            })
                .done(function (result) {
                    const resData = result.result || result;
                    callback({
                        recordsTotal: resData.totalCount || 0,
                        recordsFiltered: resData.totalCount || 0,
                        data: resData.items || []
                    });
                })
                .fail(function (error) {
                    console.error('Lỗi khi lấy danh sách danh mục:', error);
                });
        },

        columnDefs: [
            {
                targets: 0,
                data: null,
                defaultContent: '',
                className: 'text-center',
                sortable: false,
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            {
                targets: 1,
                data: 'name'
            },
            {
                targets: 2,
                data: null,
                defaultContent: '',
                className: 'text-center',
                sortable: false,
                render: function (data, type, row) {
                    return [
                        `<button type="button" class="btn btn-sm btn-primary edit-category mr-1" data-id="${row.id}">`,
                        `   <i class="fas fa-pencil-alt"></i> Sửa`,
                        `</button>`,
                        `<button type="button" class="btn btn-sm btn-danger delete-category" data-id="${row.id}" data-name="${row.name}">`,
                        `   <i class="fas fa-trash"></i> Xóa`,
                        `</button>`
                    ].join('');
                }
            }
        ]
    });

    // Ép mở modal thêm mới nếu data-toggle không hoạt động
    $('#CreateNewCategoryButton').click(function () {
        $createModal.modal('show');
    });

    // 1. Tạo Category mới
    $(document).on('submit', 'form[name="CategoryCreateForm"]', function (e) {
        e.preventDefault();
        const $form = $(this);
        const $nameInput = $form.find('input[name="Name"]');

        // Validate phía client cơ bản (Required, Maxlength...)
        if (!$form.valid()) {
            return;
        }

        const category = $form.serializeFormToObject();
        
        abp.ui.setBusy($createModal);

        abp.ajax({
            url: abp.appPath + 'Categories/Create',
            type: 'POST',
            data: JSON.stringify(category),
            abpHandleError: false,
            error: function () {}
        }).done(function () {
            $createModal.modal('hide');
            $form[0].reset();
            $form.find('.is-invalid').removeClass('is-invalid');
            dataTable.ajax.reload();
            abp.notify.success('Thêm mới danh mục thành công');
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
            abp.ui.clearBusy($createModal);
        });
    });

    // Reset Form khi ẩn Modal
    $createModal.on('hidden.bs.modal hide.bs.modal', function () {
        const $form = $(this).find('form');
        if ($form.length) {
            $form[0].reset();
            $form.find('.is-invalid').removeClass('is-invalid');
            $form.find('.invalid-feedback').hide().text('');
            
            if ($form.data('validator')) {
                $form.data('validator').resetForm();
            }
        }
    });

    // 2. Mở Modal Cập nhật Category
    $(document).on('click', '.edit-category', function () {
        const id = $(this).attr('data-id');

        abp.ajax({
            url: abp.appPath + 'Categories/EditModal?id=' + id,
            type: 'GET',
            dataType: 'html'
        }).done(function (data) {
            $editModal.find('.modal-content').html(data);
            $editModal.modal('show');

            const $editForm = $('#CategoryEditForm');
            if ($.fn.valid && $editForm.length) {
                $editForm.validate({
                    rules: { 
                        Name: { 
                            required: true, 
                            maxlength: 100 } 
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
        });
    });

    // 3. Xử lý sự kiện Submit Form Sửa 
    $(document).on('submit', '#CategoryEditForm', function (e) {
        e.preventDefault();

        const $editForm = $(this);
        const $nameInput = $editForm.find('input[name="Name"]');

        // SỬA: Cú pháp chuẩn check validate
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
            dataTable.ajax.reload();
            abp.notify.info('Cập nhật danh mục thành công');
        }).fail(function (error) {
            $nameInput.addClass('is-invalid');

            // SỬA: Thêm dấu !
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

    // 4. Xóa Category
    $(document).on('click', '.delete-category', function () {
        const id = $(this).attr('data-id');
        const name = $(this).attr('data-name');

        abp.message.confirm(
            `Bạn có chắc chắn muốn xóa danh mục "${name}" không?`,
            'Xác nhận xóa',
            function (isConfirmed) {
                if (isConfirmed) {
                    abp.ajax({
                        url: abp.appPath + 'Categories/Delete?id=' + id,
                        type: 'DELETE',
                    }).done(function () {
                        dataTable.ajax.reload();
                        abp.notify.success('Xóa danh mục thành công');
                    });
                }
            }
        );
    });

    // 5. Tìm kiếm
    $('#SearchButton').click(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $('#SearchKeyword').on('keydown', function (e) {
        if (e.key === 'Enter') {
            e.preventDefault();
            dataTable.ajax.reload();
        }
    });

})(jQuery);