using ProjectBoyd.Models.EntityModels;

namespace ProjectBoyd.Models.HtmlModels
{
    public class SendMessageTeamItem
    {
        public string CheckMarkText { get; set; } = "check_box_outline_blank";
        public TeamEntity Team { get; set; }
    }
}
