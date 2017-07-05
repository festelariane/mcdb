function PriceModelProvider() {
    var defaultSelectors = {
        ModelId: "#ModelId",
        tbPriceModel: "#tb-price-model",
        linkGetAjax: "#linkGetAjax",
        linkUpdateAjax: "#linkUpdateAjax",
        linkDeleteAjax: "#linkDeleteAjax",
        btnNew: "#btnNew",
        btnSave:""
    };

    this.selectors = defaultSelectors;
    this.isProcessing = false;
    this.datatable = null;
}
PriceModelProvider.prototype.Init = function () {
    var me = this;
    var nEditing = null;
    var nNew = false;

    var linkGetPrice = $(me.selectors.linkGetAjax).val();
    var linkUpdatePrice = $(me.selectors.linkUpdateAjax).val();
    var linkDeletePrice = $(me.selectors.linkDeleteAjax).val();
    this.datatable = $(me.selectors.tbPriceModel).dataTable(
            {
                paging: false,
                ajax:
                {
                    url: linkGetPrice,
                    type: "POST",
                    contentType: "application/json; charset=utf-8"
                },
                columns: [
                    {
                        title: "Loại hình thuê", data: function (data, type, fullObject) {
                            if (null != data.RentType) {
                                return data.RentType.RentTypeName
                            } else {
                                return "";
                            }
                        }
                    },
                    {
                        title: "Price", data: function (data, type, value) {
                            if (null != data.Price) {
                                return data.Price
                            } else {
                                return "";
                            }
                        }
                    },
                    {
                        //Edit column
                        mRender: function (data, type, fullObject) {
                            //var Id = fullObject.Id;
                            //var url = linkUpdatePrice +'/?Id=' + Id;
                            return '<a class="edit" href="javascript:;"> Edit </a>';
                        }
                    },
                    {
                        //Delete column
                        mRender: function (data, type, fullObject) {
                            var Id = fullObject.Id;
                            var url = linkDeletePrice + '/?Id=' + Id;
                            //return '<a class="delete" onclick="deleteResource(\'' + url + '\')" href=""> Delete </a>';
                            return ' <a class="delete" href="javascript:;"> Delete </a>';
                        }
                    }
                ],
            });

    $(me.selectors.ModelId).change(function () {
        var modelId = this.value;
        me.datatable.ajax.url(linkGetPrice + '/?modelId=' + modelId).load();
    });
    me.datatable.on('click', '.edit', function (e) {
        e.preventDefault();

        /* Get the row as a parent of the link that was clicked on */
        var nRow = $(this).parents('tr')[0];

        if (nEditing !== null && nEditing != nRow) {
            /* Currently editing - but not this row - restore the old before continuing to edit mode */
            restoreRow(me.datatable, nEditing);
            editRow(me.datatable, nRow);
            nEditing = nRow;
        } else if (nEditing == nRow && this.innerHTML == "Save") {
            /* Editing this row and want to save it */
            saveRow(me.datatable, nEditing);
            nEditing = null;
            //alert("Updated! Do not forget to do some ajax to sync with backend :)");
        } else {
            /* No edit in progress - let's start one */
            editRow(me.datatable, nRow);
            nEditing = nRow;
        }
    });
    $(me.selectors.btnNew).click(function (e) {
        e.preventDefault();

        if (nNew && nEditing) {
            if (confirm("Previose row not saved. Do you want to save it ?")) {
                saveRow(me.datatable, nEditing); // save
                //$(nEditing).find("td:first").html("Untitled");
                nEditing = null;
                nNew = false;

            } else {
                me.datatable.fnDeleteRow(nEditing); // cancel
                nEditing = null;
                nNew = false;

                return;
            }
        }
        var emptyItem = JSON.parse('{"RentType":{"RentTypeName":""},"Price":"0","RentTypeId":"1"}');
        var aiNew = me.datatable.fnAddData(emptyItem);
        var nRow = me.datatable.fnGetNodes(aiNew[0]);
        editRow(me.datatable, nRow);
        nEditing = nRow;
        nNew = true;
    });
}

function deleteResource(url) {
    if (confirm('Delete this record?')) {
        $.ajax({
            url: url,
            type: "post",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data) {
                    alert("Delete Successful");
                    resourceTable.ajax.reload();
                } else {
                    alert("Delete Fail");
                }
            },
            error: function (xhr, status, errThrow) {
                alert(errThrow);
            }
        });
    }
}

function restoreRow(oTable, nRow) {
    var aData = oTable.fnGetData(nRow);
    var jqTds = $('>td', nRow);

    for (var i = 0, iLen = jqTds.length; i < iLen; i++) {
        oTable.fnUpdate(aData[i], nRow, i, false);
    }

    oTable.fnDraw();
}

function editRow(oTable, nRow) {
    var aData = oTable.fnGetData(nRow);
    var jqTds = $('>td', nRow);
    jqTds[0].innerHTML = document.getElementById("RentTypes_T").outerHTML.replace("RentTypes_T", "RentTypes").replace("hiddenfield", ""); // '<input type="text" class="form-control input-small" value="' + aData[0] + '">';
    $("#RentTypes").val(aData.RentTypeId);
    jqTds[1].innerHTML = '<input type="text" class="form-control input-small" value="' + aData.Price + '">';
    //jqTds[2].innerHTML = '<input type="text" class="form-control input-small" value="' + aData[2] + '">';
    //jqTds[3].innerHTML = '<input type="text" class="form-control input-small" value="' + aData[3] + '">';
    jqTds[2].innerHTML = '<a class="edit" href="">Save</a>';
    jqTds[3].innerHTML = '<a class="cancel" href="">Cancel</a>';
}

function saveRow(oTable, nRow) {
    var jqInputs = $('input,select', nRow);
    oTable.fnUpdate($("#RentTypes option:selected").text(), nRow, 0, false);
    oTable.fnUpdate(jqInputs[1].value, nRow, 1, false);
    //oTable.fnUpdate(jqInputs[2].value, nRow, 2, false);
    //oTable.fnUpdate(jqInputs[3].value, nRow, 3, false);
    oTable.fnUpdate('<a class="edit" href="">Edit</a>', nRow, 2, false);
    oTable.fnUpdate('<a class="delete" href="">Delete</a>', nRow, 3, false);
    oTable.fnDraw();
}

function cancelEditRow(oTable, nRow) {
    var jqInputs = $('input', nRow);
    oTable.fnUpdate(jqInputs[0].value, nRow, 0, false);
    oTable.fnUpdate(jqInputs[1].value, nRow, 1, false);
    //oTable.fnUpdate(jqInputs[2].value, nRow, 2, false);
    //oTable.fnUpdate(jqInputs[3].value, nRow, 3, false);
    oTable.fnUpdate('<a class="edit" href="">Edit</a>', nRow, 3, false);
    oTable.fnDraw();
}
