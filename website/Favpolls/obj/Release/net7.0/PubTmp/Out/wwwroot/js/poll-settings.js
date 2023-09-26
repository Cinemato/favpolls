$(document).ready(() => {
    $("#poll-end").change(({target}) => {
        switch($(target).val()) {
            case "deadline":
                $("#settings-deadline").css("display", "block");
                $("#settings-votes").css("display", "none");
                $("#settings-votes").val(null);
                break;
            case "votes":
                $("#settings-deadline").css("display", "none");
                $("#settings-deadline").val(null);
                $("#settings-votes").css("display", "block");
                break;
            default:
                $("#settings-deadline").css("display", "none");
                $("#settings-deadline").val(null);
                $("#settings-votes").css("display", "none");
                $("#settings-votes").val(null);
        }
    })
})