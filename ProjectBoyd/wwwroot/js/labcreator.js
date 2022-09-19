function DisplayLabIconSelector() {

    document.getElementById("create-lab-icon-selector").classList.add("active");
    document.getElementById("create-lab-modal-container").classList.add("active");
    
}


function HideLabIconSelector() {

    document.getElementById("create-lab-icon-selector").classList.remove("active");
    document.getElementById("create-lab-modal-container").classList.remove("active");

}


function ShowCustomEquipmentModal() {

    document.getElementById("create-lab-equipment-modal").classList.add("active");
    document.getElementById("create-lab-modal-container").classList.add("active");

}

function HideCustomEquipmentModal() {

    document.getElementById("create-lab-equipment-modal").classList.remove("active");
    document.getElementById("create-lab-modal-container").classList.remove("active");

}


function ShowUploadImageModal() {

    document.getElementById("create-lab-upload-image").classList.add("active");
    document.getElementById("create-lab-modal-container").classList.add("active");

}

function HideUploadImageModal() {

    document.getElementById("create-lab-upload-image").classList.remove("active");

    if (!document.getElementById("create-lab-tag-creator").classList.contains("active")) {
        document.getElementById("create-lab-modal-container").classList.remove("active");
    }

}


function ShowCreateTagModal() {

    document.getElementById("create-lab-tag-creator").classList.add("active");
    document.getElementById("create-lab-modal-container").classList.add("active");

}

function HideCreateTagModal() {

    document.getElementById("create-lab-tag-creator").classList.remove("active");
    document.getElementById("create-lab-modal-container").classList.remove("active");

}


function ShowAddTaskModal() {

    document.getElementById("create-lab-equipment-modal").classList.add("active");
    document.getElementById("create-lab-modal-container").classList.add("active");

}

function HideAddTaskModal() {

    document.getElementById("create-lab-equipment-modal").classList.remove("active");
    document.getElementById("create-lab-modal-container").classList.remove("active");

}

function ShowAddTipsModal() {

    document.getElementById("create-lab-equipment-modal").classList.add("active");
    document.getElementById("create-lab-modal-container").classList.add("active");

}


function HideAddTipsModal() {

    document.getElementById("create-lab-equipment-modal").classList.remove("active");
    document.getElementById("create-lab-modal-container").classList.remove("active");

}

function DisableForms() {

    var forms = document.forms
    for (form of forms) {
        form.addEventListener('submit', function (event) {
            event.preventDefault();
        });
    }

}