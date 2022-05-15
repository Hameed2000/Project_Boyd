using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBoyd.Models.HtmlModels {
    public class TextInput1 : InputElement {

        public TextInput1(string fieldInfo, string secondaryStyle, string placeHolder, string id, string type, string style) : base(fieldInfo, secondaryStyle, placeHolder, id, type, style) {
            Console.WriteLine(this.id);
        }

        public void textInputClicked() {

            if (!this.activated) {

                this.activated = true;
                this.style = "border-bottom: #3377B7 solid 6px;";
                this.secondaryStyle = "color: #3377B7;";


            }
            else if (this.activated && this.text != "") {

                this.activated = false;
                this.style = "border-bottom: #19A41C solid 6px;";
                this.secondaryStyle = "color: #19A41C;";

            }
            else {

                this.style = "border-bottom: #ABABAB solid 6px;";
                this.secondaryStyle = "color: #ABABAB;";

            }

        }

    }

}
