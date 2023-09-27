let isAnimating = false;

$(document).ready(() => {
    $(".poll-option").each((index, option) => {
        if (document.getElementsByClassName("submit-button").length === 0) {
            openOption(option, 0, 0);
        }
        else {
            $(option).click(() => {
                if (!isAnimating && !$(option).hasClass("poll-option-selected")) {
                    $(option).addClass("poll-option-selected");
                    $(".option-id").val($(option).find("input:last").val());
                    $(".poll-option").each((index2, option2) => {
                        if (index != index2)
                            $(option2).removeClass("poll-option-selected");

                        if ($(option2).hasClass("poll-option-selected"))
                            openOption(option2, 1, 1);
                        else
                            openOption(option2, 0, 1);
                    })
                }           
            })
        }   
    })
})

function progressColor(progress) {
    const progressP = progress / 100;
    const hue = 48 * (1 - progressP);
    const lightness = 71 - 7 * progressP;
    return `hsl(${hue},100%,${lightness}%)`;
}

function openOption(option, selectedVoteOffset, totalVotesOffset) {
    if ($("#hide-results").val()) {
        return;
    }

    isAnimating = true;

    let prevProgress = parseInt($(option).find("input:first").val());
    let progress = ((parseInt($(option).find("input:eq(1)").val()) + selectedVoteOffset) / (parseInt($(".total-votes").val()) + totalVotesOffset)) * 100;

    if (!progress) {
        progress = 0;
    }

    if (progress == 100) {
        $(option).find(".option-progress").css("border-radius", "15px");
    }
    else {
        $(option).find(".option-progress").css({
            borderTopRightRadius: "0",
            borderBottomRightRadius: "0"
        });
    }

    if (progress < 5) {
        $(option).find("span").addClass("option-progress-offset");
    }
    else {
        $(option).find("span").removeClass("option-progress-offset");
    }

    $(option).find("p:first").css({ "opacity": "0", "overflow": "hidden", "overflow-wrap": "normal" });
    $(option).find("p:last").css("opacity", "100");
    $(option).find("span").css("opacity", "100");

    let timeToFinish = 300;

    $(option).find("span").prop("counter", prevProgress).animate({
        counter: progress
    }, {
        duration: 2000,
        step: function (now) {
            timeToFinish--;
            if (timeToFinish <= 75) {
                $(this).text((Math.round(now * 10) / 10) + "%");
            }
            else {
                $(this).text((Math.ceil(now)) + "%");
            }
        },

        complete: function () {
            isAnimating = false;
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