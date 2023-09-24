$(document).ready(() => {
    $(".account-poll-settings").find("button").each((index, button) => {
        let settings = $(button).next();
        $(button).click(() => {
            $(settings).toggleClass("dropdown-active");

            if ($(button).hasClass("dropdown-button-active")) {
                $(button).removeClass("dropdown-button-active");
                $(button).addClass("dropdown-button-inactive");
                $(button).html("Open poll settings");
            }
            else {
                $(button).removeClass("dropdown-button-inactive");
                $(button).addClass("dropdown-button-active");
                $(button).html("Close poll settings");
            }
        })     
    })
})