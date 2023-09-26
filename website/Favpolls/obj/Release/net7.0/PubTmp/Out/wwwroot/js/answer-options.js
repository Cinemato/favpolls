$(document).ready(() => {
    $(".side-button").click(() => {
        let option = `
        <div class="form-option">
            <input name="PollOptions[${$(".form-option").length}].Option" type="text" placeholder="Option ${$(".form-option").length + 1}">
            <img src="/images/icons/cross.svg" alt="remove-option" width="30px" class="remove-option" onclick="removeOption(this)">
        </div>`

        $(".form-options-container").append(option);
        $(".remove-option").css("display", "block");
    })
})

const removeOption = (target) => {
    $(document).ready(() => {
        $(target).parent().remove();

        if($(".form-option").length <= 2) {
            $(".remove-option").css("display", "none");
        }

        $(".form-option").each((index, option) => {
            $(option).find("input:last").attr({
                "placeholder": `Option ${index + 1}`,
                "name": `PollOptions[${index}].Option`
            });
        })
    })
}