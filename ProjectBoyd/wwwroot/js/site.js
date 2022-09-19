// Javascript Functions



// Confirmation Component Functions
function ConfirmationShow(TwoButtons) {

    document.getElementById("main-modal-container").classList.add("active");
    document.getElementById("confirmation-modal").classList.add("active");
    document.getElementById("basic-button-container").classList.add("active");

    if (TwoButtons == true) {

        document.getElementById("confirmation-modal-two-option").classList.add("active");

    } else {

        document.getElementById("confirmation-modal-one-option").classList.add("active");

    }

}

function ConfirmationHide() {

    document.getElementById("main-modal-container").classList.remove("active");
    document.getElementById("confirmation-modal").classList.remove("active");
    document.getElementById("confirmation-modal-two-option").classList.remove("active");
    document.getElementById("confirmation-modal-one-option").classList.remove("active");
    document.getElementById("basic-button-container").classList.remove("active");

}



// Join Session Code 
window.clipboardCopy = {
    copyText: function (codeElement) {
        navigator.clipboard.writeText(codeElement.textContent).then(function () {
            alert("Copied to clipboard!");
        })
            .catch(function (error) {
                alert(error);
            });
    }
}

// Add Instructor Component Functions

function AddInstructorShow() {

    document.getElementById("session-page-modal-container").classList.add("active");
    document.getElementById("add-instructor-modal").classList.add("active");

}

function AddInstructorHide() {

    document.getElementById("session-page-modal-container").classList.remove("active");
    document.getElementById("add-instructor-modal").classList.remove("active");

}

// Student List Component Functions

function StudentListShow() {

    document.getElementById("session-page-modal-container").classList.add("active");
    document.getElementById("student-list-modal").classList.add("active");

}

function StudentListHide() {

    document.getElementById("session-page-modal-container").classList.remove("active");
    document.getElementById("student-list-modal").classList.remove("active");

}


function SwitchClassroomDash() {
    document.getElementById("main-dashboard-y").classList.remove("active");
    document.getElementById("class-dashboard-y").classList.add("active");
}

function SwitchInstructorDash() {
    document.getElementById("class-dashboard-y").classList.remove("active");
    document.getElementById("main-dashboard-y").classList.add("active");
}



function DisableFormss() {

    var forms = document.forms
    for (form of forms) {
        form.addEventListener('submit', function (event) {
            event.preventDefault();
        });
    }

}