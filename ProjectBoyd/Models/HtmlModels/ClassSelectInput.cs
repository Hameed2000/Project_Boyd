using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBoyd.Models.HtmlModels {
    public class ClassSelectInput : InputElement {

        // Later options will be taken from the database, for now I put some placeholder values
        public List<string> options { get; set; } = new List<string> { "Dunwoody", "Quik Trip", "Reesor Inc" };
        public string classSelected { get; set; } = "";

        public ClassSelectInput(string fieldInfo, string secondaryStyle, string placeHolder, string id, string type, string style) : base(fieldInfo, secondaryStyle, placeHolder, id, type, style) {
        }

        public void selectInputClicked(Boolean focusOut) {
            Console.WriteLine("function called");
            if (!this.activated && !focusOut) {
                Console.WriteLine("1st");
                this.activated = true;
                this.style = "border-bottom: #3377B7 solid 6px;";
                this.secondaryStyle = "color: #3377B7;";

            }
            else if (this.activated && this.classSelected != "") {
                Console.WriteLine("2nd");
                this.activated = false;
                this.style = "border-bottom: #19A41C solid 6px;";
                this.secondaryStyle = "color: #19A41C;";

            }
            else if (this.classSelected == "") {
                Console.WriteLine("3rd");
                this.activated = false;
                this.style = "border-bottom: #ABABAB solid 6px;";
                this.secondaryStyle = "color: #ABABAB;";

            }

        }

    }

}
