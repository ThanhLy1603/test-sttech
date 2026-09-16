(function ($) {
    const $table = $('#BooksTable'),
        $createModal = $('#BookCreateModal'),
        $createForm = $createModal.find('form[name="BookCreateForm"]'),
        $editModal = $('#BookEditModal');
    
    if ($.fn.validate) {
        $createForm.validate();
    }
    
    // Cài đặt cấu hình và render Table
    const dataTable = $table.DataTable({
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
            {
                targets: 1, 
                data: 'title',
            },
            {
                targets: 2,
                data: 'author',
            },
            {
                targets: 3,
                data: 'price',
                className: 'text-center',
                render: function (data) {
                    if (data !== null && data !== undefined) {
                        return new Intl.NumberFormat('vi-VN').format(data);
                    }
                    
                    return '0 VNĐ';
                }
            },
            {
                targets: 4,
                data: 'category.name',
                defaultContent: '-'
            },
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
    
    // Thêm sách mới
    $createForm.on('submit', function (event) {
        event.preventDefault();
        
        if ($createForm.valid && !$createForm.valid()) return;
        
        const book = $createForm.serializeFormToObject();
        
        abp.ui.setBusy($createModal);
        
        abp.ajax({
            url: abp.appPath + 'Books/Create',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(book),
        }).done(function () {
            if (document.activeElement) {
                document.activeElement.blur();
            }
            
            $createModal.modal('hide');
            $createForm[0].reset();
            dataTable.ajax.reload();
            abp.notify.success('Thêm sách mới thành công');
        }).always(function () {
            abp.ui.clearBusy($createForm);
        });
    });
    
    // Reset Form khi đóng Modal Create
    $createModal.on('hidden.bs.modal hide.bs.modal', function () {
        $createForm[0].reset();
        
        if ($createForm.data('validator')) {
            $createForm.data('validator').resetForm();
        }
    });
    
    // Mở Modal chỉnh sửa thông tin sách
    $(document).on('click', '.edit-book', function () {
        const id = $(this).attr('data-id');
        
        abp.ajax({
            url: abp.appPath + 'Books/EditModal?id=' + id,
            type: 'GET',
            dataType: 'html'
        }).done(function (htmlContent) {
            $editModal.find('.modal-content').html(htmlContent);
            
            const $editForm = $editModal.find('form#BookEditForm');
            if ($.fn.validate) {
                $editForm.validate();
            }
            
            $editModal.modal('show');
        }).always(function () {
            abp.ui.clearBusy($createModal);
        });
    });
    
    // Cập nhật thông tin sách
    $(document).on('submit', '#BookEditForm', function (event) {
        event.preventDefault();
        
        const $editForm = $(this);
        
        if ($editForm.valid && !$editForm.valid()) return;
        
        const book = $editForm.serializeFormToObject();
        
        abp.ui.setBusy($createModal);
        
        abp.ajax({
            url: abp.appPath + 'Books/Update',
            type: 'PUT',
            contentType: 'application/json',
            data: JSON.stringify(book),
        }).done(function () {
            $editModal.modal('hide');
            dataTable.ajax.reload();
            abp.notify.info('Cập nhật thành công');
        }).always(function () {
            abp.ui.clearBusy($createModal);
        });
    });
    
    // Xóa sách
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
                        dataTable.ajax.reload();
                        abp.notify.success('Xóa sách thành công');
                    }).always(function () {
                        abp.ui.clearBusy($table);
                    });
                }
            }
        );
    })
    
    // Thực hiện tìm kiếm 
    $('#SearchButton').click(function (e) {
        e.preventDefault();
        
        dataTable.ajax.reload();
    });
    
    // Thêm chức năng nhấn Enter
    $('#SearchKeyword').on('keyup', function (e) {
        if (e.key === 'Enter' || e.keyCode === 13) {
            e.preventDefault();
            
            dataTable.ajax.reload();
        }
    });
})(jQuery);