$(document).ready(() => {
    $(".poll-option").each((index, option) => {
        $(option).click(() => {
            $(option).addClass("poll-option-selected");
            $(".option-id").val($(option).find("input:last").val());
            $(".poll-option").each((index2, option2) => {
                if (index != index2)
                    $(option2).removeClass("poll-option-selected");

                if ($(option2).hasClass("poll-option-selected"))
                    openOption(option2, 1);
                else
                    openOption(option2, 0);   
            })     
        })
    })
})

function progressColor(progress) {
    if (progress >= 0 && progress <= 24) return '#FFE169';
    else if (progress >= 25 && progress <= 49) return '#FFDE59';
    else if (progress >= 50 && progress <= 74) return '#FF914D';
    else if (progress >= 75 && progress <= 89) return '#FF7A7A';
    else if (progress >= 90 && progress <= 100) return '#FF5757';
}

function openOption(option, offset) {
    let prevProgress = parseInt($(option).find("input:first").val());
    let progress = ((parseInt($(option).find("input:eq(1)").val()) + offset) / (parseInt($(".total-votes").val()) + 1)) * 100;

    if (progress == 100) {
        $(option).find(".option-progress").css("border-radius", "15px");
    }
    else {
        $(option).find(".option-progress").css({
            borderTopRightRadius: "0",
            borderBottomRightRadius: "0"
        });
    }

    $(option).find("p:first").css("opacity", "0");
    $(option).find("p:last").css("opacity", "100");
    $(option).find("span").css("opacity", "100");

    $(option).find("span").prop("counter", prevProgress).animate({
        counter: progress
    }, {
        duration: 2000,
        step: function (now) {
            $(this).text(Math.ceil(now) + "%");
        }
    })

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

    $(option).addClass("poll-option-opened");
    $(option).find("input:first").val(progress);
}