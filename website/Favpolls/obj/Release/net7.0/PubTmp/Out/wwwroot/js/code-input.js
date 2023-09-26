$(document).ready(() => {
    $(".code-input").each((index, input) => {
        $(input).on('keyup input', (e) => {
            let max = 1;

            if ($(input).val().length > max) {
                $(input).val($(input).val().substr(0, max));
            }

            if(e.which === 32 && $(input).val() == " ") {
                $(input).val("");
            }

            if($(input).length === 1 && e.which !== 32) {
                if (e.key === "Backspace" && index > 0) {
                    $(".code-input")[index - 1].focus();
                }
                else if (e.key && index < 5 && e.key !== "Backspace") {
                    console.log(e.key)
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