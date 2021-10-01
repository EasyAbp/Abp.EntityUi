$(function () {

    var l = abp.localization.getResource(localizationResourceName);

    var propertyColumns = [];
    for (var propertyName in propertyNameTitleMapping) {
        propertyColumns.push({
            title: l(propertyNameTitleMapping[propertyName]),
            data: propertyName
        })
    }

    function isEmptyOrSpaces(str){
        return str === null || str.match(/^ *$/) !== null;
    }

    var service = eval(serviceCode);
    var createModal = new abp.ModalManager(abp.appPath + createModalSubPath);
    var editModal = new abp.ModalManager(abp.appPath + editModalSubPath);

    var dataTable = $('#' + tableId).DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: false,
        paging: false,
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
                                visible: editEnable && (isEmptyOrSpaces(editPermission) || abp.auth.isGranted(editPermission)),
                                action: function (data) {
                                    eval('editModal.open(' + editKeysCode + ')');
                                }
                            },
                            {
                                text: l(deletionRowActionItemText),
                                visible: deletionEnable && (isEmptyOrSpaces(deletionPermission) || abp.auth.isGranted(deletionPermission)),
                                confirmMessage: function (data) {
                                    return eval(deletionConfirmMessageReturnCode);
                                },
                                action: function (data) {
                                    eval(`service.get(` + parentEntityKeysCode + `)
                                        .then(function (parentData) {
                                            deleteSubEntity(data.record, parentData);
                                        })
                                    `)
                                }
                            }
                        ]
                }
            },
            ... propertyColumns
        ]
    }));
    
    function deleteSubEntity(data, parentData) {
        var list = eval('parentData.' + subEntityListPropertyName);
        var index = eval(findSubEntityIndexCode);
        list.splice(index, 1);
        eval(`service.update(` + parentEntityKeysCode + `, parentData)
            .then(function () {
                    abp.notify.info(l(successfullyDeletedNotificationText));
                    dataTable.ajax.reload();
                }
            )`
        )
    }

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#' + newButtonId).click(function (e) {
        e.preventDefault();
        eval('createModal.open(' + parentEntityKeysCodeForCreateSubEntityModal + ')');
    });

});