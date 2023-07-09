$(document).ready(() => {
    $(".id-input").each((index, input) => {
        $(input).keyup((e) => {
            if(e.which === 32 && $(input).val() == " ") {
                $(input).val("");
            }
            if($(input).length === 1 && (e.key.length === 1 || e.key === "Backspace") && e.which !== 32) {
                if(e.key === "Backspace" && index > 0) {
                    $(".id-input")[index - 1].focus();
                }
                else if (!(($(".id-input").length - 1) === index && $(input).length === 1) && e.key.length === 1) {
                    $(".id-input")[index + 1].focus();
                }              
            }
        })
    }) 
})