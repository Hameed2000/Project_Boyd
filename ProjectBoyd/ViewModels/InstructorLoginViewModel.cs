using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectBoyd;
using ProjectBoyd.Models;
using ProjectBoyd.Models.HtmlModels;

namespace ProjectBoyd.ViewModels {
    public class InstructorLoginViewModel {

        public int pageDisplay { get; set; }  = 1;
        public string pageInstructionText { get; set; } = "Sign in to your IPE instructor account";
        public Action StateHasChangedDelegate { get; set; }
        public Element hint { get; set; } = new Element("hint", "Nil", "color: transparent;");
        public string passwordType { get; set; } = "password";

        public Dictionary<string, TextInput1> loginInputList = new Dictionary<string, TextInput1> {
            ["email"] = new TextInput1("Email", "color: #ABABAB;", "jackappleseed@example.com", "email", "text", "border-bottom: #ABABAB solid 6px;"),
            ["pword"] = new TextInput1("Password", "color: #ABABAB;", "************", "pword", "password", "border-bottom: #ABABAB solid 6px;"),
        };


        public void signInButton() {

            if (loginInputList["email"].text != "" && loginInputList["pword"].text != "") {
                hint.text = "Nil";
                hint.style = "color: transparent;";
                pageInstructionText = "";
                pageDisplay = 3;
                StateHasChangedDelegate.Invoke();
            } else {
                hint.text = "You are missing some fields";
                hint.style = "color: red;";
            }

        }

    }

}
