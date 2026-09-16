(function ($) {

    const $table = $('#CategoriesTable'),
        $createModal = $('#CategoryCreateModal'),
        $editModal = $('#CategoryEditModal');

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

        if ($.fn.valid && !$form.valid()) return;

        const category = $form.serializeFormToObject();

        abp.ajax({
            url: abp.appPath + 'Categories/Create',
            type: 'POST',
            data: JSON.stringify(category),
        }).done(function () {
            $createModal.modal('hide');
            $form[0].reset();
            dataTable.ajax.reload();
            abp.notify.success('Thêm mới danh mục thành công');
        });
    });

    // Reset Form khi ẩn Modal
    $createModal.on('hidden.bs.modal hide.bs.modal', function () {
        const $form = $(this).find('form');
        if ($form.length) $form[0].reset();
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
        });
    });

    // 3. Xử lý sự kiện Submit Form Sửa 
    $(document).on('submit', '#CategoryEditForm', function (e) {
        e.preventDefault();

        const $editForm = $(this);

        if ($.fn.valid && !$editForm.valid()) return;

        const category = {
            id: $editForm.find('input[name="Id"]').val(),
            name: $editForm.find('input[name="Name"]').val()
        };

        abp.ajax({
            url: abp.appPath + 'Categories/Update',
            type: 'PUT',
            data: JSON.stringify(category),
        }).done(function () {
            $editModal.modal('hide');
            dataTable.ajax.reload();
            abp.notify.info('Cập nhật danh mục thành công');
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