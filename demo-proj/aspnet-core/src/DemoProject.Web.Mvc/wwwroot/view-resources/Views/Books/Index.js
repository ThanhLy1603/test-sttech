(function ($) {
    // Khai báo Namespace toàn cục ngay đầu file
    window.Books = window.Books || {};

    const $table = $('#BooksTable'),
        $createModal = $('#BookCreateModal'),
        $createForm = $createModal.find('form[name="BookCreateForm"]'),
        $editModal = $('#BookEditModal');

    // 1. Khởi tạo validation cho Form Thêm mới
    if ($.fn.valid && $createForm.length) {
        $createForm.validate({
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
            highlight: function (element) { $(element).addClass('is-invalid'); },
            unhighlight: function (element) { $(element).removeClass('is-invalid'); },
            errorElement: 'span',
            errorClass: 'invalid-feedback'
        });
    }

    $(document).on('input change', 'form[name="BookCreateForm"] input, form[name="BookCreateForm"] select', function () {
        $(this).removeClass('is-invalid').siblings('.invalid-feedback').hide();
    });

    // 2. Render DataTables & Gán ngay vào Window Object
    window.Books.dataTable = $table.DataTable({
        paging: true,
        serverSide: true,
        processing: true,
        searching: false,
        lengthMenu: [5, 10, 20],
        pageLength: 10,
        ajax: function (data, callback) {
            const input = {
                filter: ($('#SearchKeyword').val() || '').trim(),
                skipCount: data.start || 0,
                maxResultCount: data.length || 0,
                sorting: 'Title ASC'
            };

            abp.ajax({
                url: abp.appPath + 'Books/GetAll?' + $.param(input),
                type: 'GET',
            }).done(function (data) {
                const response = data.result || data;
                callback({
                    recordsTotal: response.totalCount || 0,
                    recordsFiltered: response.totalCount || 0,
                    data: response.items || []
                });
            });
        },
        columnDefs: [
            {
                targets: 0,
                data: null,
                className: 'text-center',
                sortable: false,
                render: (data, type, row, meta) => meta.row + meta.settings._iDisplayStart + 1
            },
            { targets: 1, data: 'title' },
            { targets: 2, data: 'author' },
            {
                targets: 3,
                data: 'price',
                className: 'text-center',
                render: (data) => data ? new Intl.NumberFormat('vi-VN').format(data) + ' VNĐ' : '0 VNĐ'
            },
            { targets: 4, data: 'category.name', defaultContent: '-' },
            {
                targets: 5,
                data: null,
                className: 'text-center',
                sortable: false,
                render: function (data, type, row) {
                    return `
                        <button type="button" class="btn btn-sm btn-primary edit-book mr-1" data-id="${row.id}">
                            <i class="fas fa-pencil-alt"></i> Sửa
                        </button>
                        <button type="button" class="btn btn-sm btn-danger delete-book" data-id="${row.id}" data-title="${row.title}">
                            <i class="fas fa-trash"></i> Xóa
                        </button>
                    `;
                }
            }
        ]
    });

    // 3. Xử lý Thêm sách mới
    $createForm.on('submit', function (event) {
        event.preventDefault();
        const $titleInput = $createForm.find('input[name="Title"]');
        if (!$createForm.valid()) return;

        const book = $createForm.serializeFormToObject();
        abp.ui.setBusy($createModal);

        abp.ajax({
            url: abp.appPath + 'Books/Create',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(book),
            abpHandleError: false
        }).done(function () {
            if (document.activeElement) document.activeElement.blur();
            $createModal.modal('hide');
            window.Books.dataTable.ajax.reload();
            abp.notify.success('Thêm sách mới thành công');
        }).fail(function (error) {
            $titleInput.addClass('is-invalid');
            let $errorSpan = $titleInput.siblings('.invalid-feedback');
            if (!$errorSpan.length) {
                $titleInput.after('<span class="invalid-feedback"></span>');
                $errorSpan = $titleInput.siblings('.invalid-feedback');
            }
            $errorSpan.text(error?.message || 'Tên sách đã tồn tại trong hệ thống').show();
        }).always(function () {
            abp.ui.clearBusy($createModal);
        });
    });

    $createModal.on('hidden.bs.modal hide.bs.modal', function () {
        $createForm[0].reset();
        $createForm.find('.is-invalid').removeClass('is-invalid');
        $createForm.find('.invalid-feedback').hide().text('');
        if ($createForm.data('validator')) $createForm.data('validator').resetForm();
    });

    // 4. Mở Modal Chỉnh sửa
    $(document).on('click', '.edit-book', function () {
        const id = $(this).attr('data-id');
        abp.ui.setBusy($table);

        abp.ajax({
            url: abp.appPath + 'Books/EditModal?id=' + id,
            type: 'GET',
            dataType: 'html'
        }).done(function (htmlContent) {
            $editModal.find('.modal-content').html(htmlContent);
            $editModal.modal('show');
        }).always(function () {
            abp.ui.clearBusy($table);
        });
    });

    $editModal.on('hidden.bs.modal hide.bs.modal', function () {
        $(this).find('.modal-content').html('');
    });

    // 5. Xóa sách
    $(document).on('click', '.delete-book', function () {
        const id = $(this).attr('data-id');
        const title = $(this).attr('data-title');

        abp.message.confirm(
            `Bạn có chắc chắn muốn xóa cuốn sách "${title}" không?`,
            'Xác nhận xóa',
            function (isConfirmed) {
                if (isConfirmed) {
                    abp.ui.setBusy($table);
                    abp.ajax({
                        url: abp.appPath + 'Books/Delete?id=' + id,
                        type: 'DELETE',
                    }).done(function () {
                        window.Books.dataTable.ajax.reload();
                        abp.notify.success('Xóa sách thành công');
                    }).always(function () {
                        abp.ui.clearBusy($table);
                    });
                }
            }
        );
    });

    // 6. Tìm kiếm
    const doSearch = () => window.Books.dataTable.ajax.reload();
    $('#SearchButton').click(doSearch);
    $('#SearchKeyword').on('keyup', function (e) {
        if (e.key === 'Enter' || e.keyCode === 13) {
            e.preventDefault();
            doSearch();
        }
    });

})(jQuery);