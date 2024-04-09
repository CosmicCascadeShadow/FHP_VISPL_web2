// ------------------------- registeration box popup [yet not working]
function GetRegisterationForm() {
    $.ajax({
        url: "/Home/RegisterationPage",
        type: "GET",
        success: function (data) {
            $("#registrationContainer").html(data);
        },
        error: function (error) {
            console.error("Error loading registration content:", error);
        }
    });
}