using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectBoyd;
using ProjectBoyd.Models;
using ProjectBoyd.Models.HtmlModels;
using ProjectBoyd.Models.ObjectModels;

namespace ProjectBoyd.ViewModels {
    public class StudentLoginViewModel {

        public int pageDisplay { get; set; }  = 1;
        public string pageInstructionText { get; set; } = "Select your class name";
        public Action StateHasChangedDelegate { get; set; }
        public Element hint { get; set; } = new Element("hint", "Nil", "color: transparent;");
        public Team team { get; set; }
        public ClassSelectInput classSelect { get; set; } = new ClassSelectInput("classes", "color: #ABABAB;", "", "class-dropdown", "select", "border-bottom: #ABABAB solid 6px;");

        public Dictionary<string, TextInput1> membersInputList { get; set; } = new Dictionary<string, TextInput1> {
            ["fname"] = new TextInput1("First Name", "color: #ABABAB;", "Jack", "fname", "text", "border-bottom: #ABABAB solid 6px;"),
            ["lname"] = new TextInput1("Last Name", "color: #ABABAB;", "Appleseed", "lname", "text", "border-bottom: #ABABAB solid 6px;"),
            ["email"] = new TextInput1("Email Address", "color: #ABABAB;", "jackappleseed@company.com", "email", "text", "border-bottom: #ABABAB solid 6px;")
        };


        // Handles the next button
        // Confirms a class is selected and displays the add team member page
        public async void nextButton() {

            if (classSelect.classSelected != "") {

                await Task.Delay(250);

                hint.text = "Nil";
                hint.style = "color: transparent";

                pageDisplay = 2;
                pageInstructionText = "Enter all members of your team";

                //team = new Team(classSelect.classSelected);

                StateHasChangedDelegate.Invoke();

            }
            else {

                hint.text = "Please select a class";
                hint.style = "color: red;";

            }

        }

        // Handles the add member button
        // Takes the information entered in the fields and creates a student object
        // Adds student object to team member list
        public void addMemberButton() {

            // TO-DO: check to make sure they don't add repeated entries
            // make models for everything
            // add verification on email/name fields

            Boolean validEntry = true;
            foreach (KeyValuePair<string, TextInput1> info in membersInputList) {
                if (info.Value.text == "") {
                    validEntry = false;
                    break;
                }
            }

            if (validEntry) {

                Student entry = new Student(membersInputList["fname"].text, membersInputList["lname"].text, membersInputList["email"].text);

                team.addMember(entry);

                hint.text = entry.studentEntity.FirstName + " has been added";
                hint.style = "color: #19A41C";

                foreach (KeyValuePair<string, TextInput1> info in membersInputList) {
                    info.Value.text = "";
                    info.Value.style = "border-bottom: #ABABAB solid 6px;";
                    info.Value.secondaryStyle = "color: #ABABAB;";
                }

            }
            else {

                hint.text = "You are missing some fields";
                hint.style = "color: red;";

            }

        }

        // Handles join session button
        // Brings up loading screen, in the future it will actually join a session, for now just displays loading screen
        public async void joinSessionButton() {

            Boolean fnameCheck = membersInputList["fname"].text != "";
            Boolean lnameCheck = membersInputList["lname"].text != "";
            Boolean emailCheck = membersInputList["email"].text != "";
            Boolean forgotField = (fnameCheck && (!lnameCheck || !emailCheck)) || (lnameCheck && (!fnameCheck || !emailCheck)) || (emailCheck && (!fnameCheck || !lnameCheck));

            if (fnameCheck && lnameCheck && emailCheck) {

                Student entry = new Student(membersInputList["fname"].text, membersInputList["lname"].text, membersInputList["email"].text);

                team.addMember(entry);

            }

            if (team.count() == 0 || forgotField) {

                hint.text = "You are missing some fields";
                hint.style = "color: red;";
                return;

            }

            await Task.Delay(250);

            hint.text = "Nil";
            hint.style = "color: transparent;";

            pageDisplay = 3;

            team.printAll();

            pageInstructionText = "";

            StateHasChangedDelegate.Invoke();

        }

    }

}
