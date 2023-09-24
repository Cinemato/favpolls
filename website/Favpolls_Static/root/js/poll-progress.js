$(document).ready(() => {
    $(".poll-option").each((index, option) => {
        $(option).click(() => {
            $(".poll-option").each((index2, option2) => {
                if($(option2).find(".option-progress").css("max-width") == "none")
                    selectOption(option2);
                $(option2).removeClass("poll-option-selected");
            })
            $(option).addClass("poll-option-selected");
        })
    })
})

/*function progressColor(progress) {
    if (progress >= 0 && progress <= 24) return '#FFE169';
    else if (progress >= 25 && progress <= 49) return '#FFDE59';
    else if (progress >= 50 && progress <= 74) return '#FF914D';
    else if (progress >= 75 && progress <= 89) return '#FF7A7A';
    else if (progress >= 90 && progress <= 100) return '#FF5757';
}*/

function progressColor(progress) {
    const progressP = progress / 100;
    const hue = 48 * (1 - progressP);
    const lightness = 71 - 7 * progressP;
    return `hsl(${hue},100%,${lightness}%)`;
}

function selectOption(option) {
    let progress = $(option).find("input").val();
    
    if (progress == 100) {
        $(option).find(".option-progress").css("border-radius", "15px");
    }

    $(option).find("p:first").css("opacity", "0");
    $(option).find("p:last").css("opacity", "100");
    $(option).find("span").css("opacity", "100");

    $(option).find(".option-progress").animate({
        maxWidth: progress + "%"
    }, {
        duration: 1250,
        step: function (progress) {
            $(this).css({
                backgroundColor: progressColor(progress)
            });
        }
    })

    $(option).find("span").prop("counter", 0).animate({
        counter: progress
    }, {
        duration: 2000,
        step: function (now) {
            $(this).text(Math.ceil(now) + "%");
        }
    })

    $(option).addClass("poll-option-opened");
}