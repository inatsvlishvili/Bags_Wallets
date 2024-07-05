$(document).Ready(function () {
    $("#SearchText").on("keyup", function () {
        var searchtext = $("#SearchText").val();
        $.ajax({
            type: "post",
            url: "/BiodataModels/SearchBiodataJson?SearchText=" + searchtext,
            success: function (result) {
                $("#tblBiodata").html("");

                if (result.length == 0) {
                    $("#tblBiodata").append("<tr><td colspan='4'>No Record Matched</td></tr>")
                }
                else {
                    $.each(result, function (index, value) {
                        var data = "<tr><td>" + value.StaffId + "</td>" + "<td>" + value.SurName + "</td>" + "<td>" + value.OtherName + "</td>" + "<td>" + value.MDA + "</td>";

                        $('#tblBiodata').append(data);
                    })
                } // if
            } // success
        }) // ajax
    }); // on #SearchText keyup
}); // ready

//$('#txtSearch').keyup(function () {
//    debugger
//    var typeValue = $(this).val();
//    $('tbody tr').each(function () {
//        if ($(this).text().search(new RegExp(typeValue, "i")) < 0) {
//            $(this).fadeOut();
//        }
//        else {
//            $(this).show();
//        }
//    })

//});