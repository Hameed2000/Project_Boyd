using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectBoyd.Models;

namespace ProjectBoyd.Models {
    public class InputElement : Element {

        public string fieldInfo { get; set; }
        public Boolean activated { get; set; } = false;
        public string secondaryStyle { get; set; }
        public string type { get; set; }

        public InputElement(string fieldInfo, string secondaryStyle, string placeHolder, string id, string type, string style) : base(id, style) {
            this.fieldInfo = fieldInfo;
            this.secondaryStyle = secondaryStyle;
            this.placeHolder = placeHolder;
            this.type = type;
            Console.WriteLine(id);
        }

    }

}
