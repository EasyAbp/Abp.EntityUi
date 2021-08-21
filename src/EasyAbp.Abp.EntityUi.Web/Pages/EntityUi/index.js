$(function () {

    var l = abp.localization.getResource(localizationResourceName);

    var propertyColumns = [];
    for (var propertyName in propertyNameTitleMapping) {
        propertyColumns.push({
            title: l(propertyNameTitleMapping[propertyName]),
            data: propertyName,
            render: function (data) {
                return data ?? null;
            }
        })
    }
    
    var service = eval(serviceCode);
    var createModal = new abp.ModalManager(abp.appPath + createModalSubPath);
    var editModal = new abp.ModalManager(abp.appPath + editModalSubPath);
    
    eval(buildSubEntitiesRowActionItemsCode);

    var dataTable = $('#' + tableId).DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        autoWidth: false,
        scrollCollapse: true,
        order: [[0, "asc"]],
        ajax: abp.libs.datatables.createAjax(service.getList, function () {
            return getListInput ?? {}
        }),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            ... subEntitiesRowActionItems,
                            {
                                text: l(editRowActionItemText),
                                visible: editEnable && abp.auth.isGranted(editPermission),
                                action: function (data) {
                                    eval('editModal.open(' + editKeysCode + ')');
                                }
                            },
                            {
                                text: l(deletionRowActionItemText),
                                visible: deletionEnable && abp.auth.isGranted(deletionPermission),
                                confirmMessage: function (data) {
                                    return eval(deletionConfirmMessageReturnCode);
                                },
                                action: function (data) {
                                    eval('service.delete(' + deletionActionInputCode + ')' +
                                        `.then(function () {
                                                abp.notify.info(l(successfullyDeletedNotificationText));
                                                dataTable.ajax.reload();
                                            }
                                        )`);
                                }
                            }
                        ]
                }
            },
            ... propertyColumns
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#' + newButtonId).click(function (e) {
        e.preventDefault();
        createModal.open();
    });

});