$(document).ready(() => {
    $(".share-button").each((index, button) => {
        $(button).click(() => {
            navigator.clipboard.writeText("https://localhost:7124/Poll/Join?code=" + $(button).next().val());
            $(button).css("color", "#FF5757");
            $(button).toggleClass("share-button-copied");
            $(button).find("span").css("opacity", "100%");
            $(button).prop("disabled", true);

            setTimeout(() => {
                $(button).prop("disabled", false);
                $(button).css("color", "#fff");
                $(button).toggleClass("share-button-copied");
                $(button).find("span").css("opacity", "0");            
            }, "2000")  
        })
    })
})