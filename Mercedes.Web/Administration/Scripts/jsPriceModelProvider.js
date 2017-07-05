function PriceModelProvider() {
    var defaultSelectors = {
        ModelId: "#ModelId",
        tbPriceModel: "#tb-price-model",
        linkGetAjax: "#linkGetAjax",
        linkUpdateAjax: "#linkUpdateAjax",
        linkDeleteAjax: "#linkDeleteAjax"
    };

    this.selectors = defaultSelectors;
    this.isProcessing = false;
    this.datatable = null;
}
PriceModelProvider.prototype.Init = function () {
    var me = this;
    var linkGetPrice = $(me.selectors.linkGetAjax).val();
    var linkUpdatePrice = $(me.selectors.linkUpdateAjax).val();
    var linkDeletePrice = $(me.selectors.linkDeleteAjax).val();
    this.datatable = $(me.selectors.tbPriceModel).DataTable(
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
                            return data.RentType.RentTypeName
                        }
                    },
                    { title: "Price", data: "Price" },
                    {
                        //Edit column
                        mRender: function (data, type, fullObject) {
                            var Id = fullObject.Id;
                            var url = linkUpdatePrice +'/?Id=' + Id;
                            return '<a id="btn-update" data-toggle="modal" onclick="getResource(\'' + url + '\')" href="#frm-upd-new-model" class="edit"> Edit </a>';
                        }
                    },
                    {
                        //Delete column
                        mRender: function (data, type, fullObject) {
                            var Id = fullObject.Id;
                            var url = linkDeletePrice + '/?Id=' + Id;
                            return '<a class="delete" onclick="deleteResource(\'' + url + '\')" href=""> Delete </a>';
                        }
                    }
                ],
            });

    $(me.selectors.ModelId).change(function () {
        var modelId = this.value;
        me.datatable.ajax.url(linkGetPrice + '/?modelId=' + modelId).reload();
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