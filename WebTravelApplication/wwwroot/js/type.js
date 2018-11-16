    $(function () {
        var myUrl = '@Url.Action("SendRequestAsyncSchedule", "TravelSchedule")';
        $("#bnt_save").click(function () {

            $.ajax({
                type: "POST",
                url: myUrl,
                data: new TravelSchedule(),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    alert("Hello: " + response.Name + " .\nCurrent Date and Time: " + response.DateTime);
                },
                failure: function (response) {
                    alert(response.responseText);
                },
                error: function (response) {
                    alert(response.responseText);
                }
            });
        });
    });