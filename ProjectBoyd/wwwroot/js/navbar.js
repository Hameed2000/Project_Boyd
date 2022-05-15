function InstructorNavBarSelect(selected) {

    var buttons = document.getElementsByClassName("side-container-button");
    for (var i = 0; i < buttons.length; i++) {
        if (buttons.item(i).id == selected) {
            buttons.item(i).classList.add("selected")
        } else {
            buttons.item(i).classList.remove("selected")
        }
    }

}

function RequestHelp(selected) {
    var button = document.getElementById("Help-sidebar");
    if (selected) {
        // Turn on
        button.classList.add("selectedHelp");
    } else {
        // Turn off
        button.classList.remove("selectedHelp");
    }
}