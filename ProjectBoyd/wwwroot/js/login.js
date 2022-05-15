
// js for login inputs
class textInput {
    constructor(element) {

        this.Container = element;
        this.Label = element.querySelector("label");
        this.Input = element.querySelector(".login-input");

        // connect listeners
        this.Input.addEventListener('focus', (event) => { this.Focus(event) });
        this.Input.addEventListener('blur', (event) => { this.Blur(event) });

        this.Input.addEventListener('change', () => { this.Blur(event) });

        this.Focus();
        this.Blur();

    }

    Focus(event) {

        this.Container.classList.add("focus")

        this.Label.classList.add("focus");
        this.Label.classList.add("content");

        this.Input.classList.add("focus");
        this.Input.classList.add("content");

    }

    Blur(event) {

        const inputContent = this.Input.value;
        console.log(inputContent)
        if (inputContent == "") {

            this.Label.classList.remove("content")

            this.Input.classList.remove("content");

            this.Container.classList.remove("content")

        } else {

            this.Container.classList.add("content")

        }

        this.Label.classList.remove("focus");

        this.Input.classList.remove("focus");

        this.Container.classList.remove("focus");



    }

}

// class for selector
class selectorInput {
    constructor(element) {

        this.Container = element;
        this.Label = element.querySelector("label")
        this.Selector = element.querySelector("select")

        // connect listeners
        this.Selector.addEventListener('focus', (event) => { this.Focus(event) });

        this.Selector.addEventListener('blur', (event) => { this.Blur(event) });

        this.Selector.addEventListener('change', () => { this.Selector.blur(); });

        this.Focus();
        this.Blur();

    }

    Focus(event) {

        this.Container.classList.add("focus");

        this.Label.classList.add("focus");
        this.Label.classList.add("content");

        this.Selector.classList.add("focus");
        this.Selector.classList.add("content");

    }

    Blur(event) {

        const inputContent = this.Selector.value;

        if (inputContent == "") {

            this.Label.classList.remove("content");

            this.Selector.classList.remove("content");

            this.Container.classList.remove("content");

        } else {

            this.Container.classList.add("content");

        }

        this.Label.classList.remove("focus");

        this.Selector.classList.remove("focus");

        this.Container.classList.remove("focus");

    }

}

// tables
const constructorTypes = {

    "text": textInput,
    "list": selectorInput,

}

let loginInputs = []

function initialize() {
    // set up login inputs
    
    const loginInputElements = document.querySelectorAll(".login-input-container")

    loginInputElements.forEach(element => {

        if (!element.classList.contains("has-event")) {

            element.classList.add("has-event");
            const inputType = element.getAttribute("inputType")

            // use the correct constructor that corresponds with inputType
            const inputObj = new constructorTypes[inputType](element) // create new object

            loginInputs.push(inputObj) // add to table

        }

    })


    if (document.getElementById('student-login-class-name') != null) {

        document.getElementById('student-login-class-name').addEventListener('submit', function (event) {
            event.preventDefault();
        });

    }

    if (document.getElementById('student-login-add-member') != null) {

        document.getElementById('student-login-add-member').addEventListener('submit', function (event) {
            event.preventDefault();
        });

    }

    console.log("oh yeah fool");

}


function updateLoginInputElements() {

    loginInputs.forEach(obj => {

        obj.Focus()

        obj.Blur()

    })

}

initialize()