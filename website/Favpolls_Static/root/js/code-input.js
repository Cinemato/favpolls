$(document).ready(() => {
    $(".code-input").each((index, input) => {
        $(input).keyup((e) => {
            if(e.which === 32 && $(input).val() == " ") {
                $(input).val("");
            }

            if($(input).length === 1 && (e.key.length === 1 || e.key === "Backspace") && e.which !== 32) {
                if(e.key === "Backspace" && index > 0) {
                    $(".code-input")[index - 1].focus();
                }
                else if (!(($(".code-input").length - 1) === index && $(input).length === 1) && e.key.length === 1) {
                    $(".code-input")[index + 1].focus();
                }              
            }

            $(".code-input-hidden").val("");

            $(".code-input").each((index, input) => {
                $(".code-input-hidden").val($(".code-input-hidden").val() + $(input).val())
            });
        })
    }) 
})