using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBoyd.Models {
    public class Element {

        public string id { get; set; }
        public string placeHolder { get; set; }
        public string text { get; set; } = "";
        public string style { get; set; }

        public Element(string id, string style) {
            this.id = id;
            this.style = style;
        }

        public Element(string id, string text, string style) {
            this.id = id;
            this.style = style;
            this.text = text;
        }

    }

}
