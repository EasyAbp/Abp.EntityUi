$(function () {

    var l = abp.localization.getResource(localizationResourceName);

    var propertyColumns = [];
    for (var propertyName in propertyNameTitleMapping) {
        propertyColumns.push({
            title: l(propertyNameTitleMapping[propertyName]),
            data: propertyName
        })
    }

    var service = eval(serviceCode);
    var createModal = new abp.ModalManager(abp.appPath + createModalSubPath);
    var editModal = new abp.ModalManager(abp.appPath + editModalSubPath);

    var dataTable = $('#' + tableId).DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        autoWidth: false,
        scrollCollapse: true,
        order: [[0, "asc"]],
        ajax: function (requestData, callback, settings) {
            if (callback) {
                eval(`service.get(` + parentEntityKeysCode + `)`).then(function (result) {
                    var listCode = "result." + subEntityListPropertyName;
                    var list = eval(listCode);
                    callback({
                        recordsTotal:list.length,
                        recordsFiltered: list.length,
                        data: list
                    });
                });
            }
        },
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l(editRowActionItemText),
                                visible: editEnable && abp.auth.isGranted(editPermission),
                                action: function (data) {
                                    editModal.open(eval(editKeysCode));
                                }
                            },
                            {
                                text: l(deletionRowActionItemText),
                                visible: deletionEnable && abp.auth.isGranted(deletionPermission),
                                confirmMessage: function (data) {
                                    return eval(deletionConfirmMessageReturnCode);
                                },
                                action: function (data) {
                                    var parentData = eval(`service.get(` + parentEntityKeysCode + `)`);
                                    var list = eval(`parentData.` + subEntityListPropertyName);
                                    var index = eval(findSubEntityIndexCode);
                                    list.splice(index, 1);
                                    eval(`service.update(` + parentEntityKeysCode + `, ` + parentData + `)
                                        .then(function () {
                                                abp.notify.info(l(successfullyDeletedNotificationText));
                                                dataTable.ajax.reload();
                                            }
                                        )`
                                    )
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