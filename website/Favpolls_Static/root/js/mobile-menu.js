$(document).ready(() => {
    $("#mobile-menu-button").click(() => {
        if($(".mobile-nav-links").css("max-height") === "0px") {
            $(".mobile-nav-links").css({"max-height": "500px", "opacity": "100%"});
            $("#mobile-menu-button").attr("src", "root/images/icons/cross.svg");
        }
        else {
            $(".mobile-nav-links").css({"max-height": "0", "opacity": "0"});
            $("#mobile-menu-button").attr("src", "root/images/icons/menu.svg");
        }
    })
})