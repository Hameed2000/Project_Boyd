using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ProjectBoyd.Components;
using ProjectBoyd.Data;
using ProjectBoyd.Models;
using ProjectBoyd.Models.EntityModels;
using ProjectBoyd.Models.ObjectModels;

namespace ProjectBoyd.ViewModels {
    public class AdminPanelViewModel {

        public UserManager<ApplicationUser> userManager;
        public ApplicationDbContext dbContext;

        //Constructor
        public AdminPanelViewModel(UserManager<ApplicationUser> UserManager, ApplicationDbContext DbContext, Action StateHasChanged) {
            userManager = UserManager;
            dbContext = DbContext;
        }


        //Declaring lists for content of member/requests
        public IList<ApplicationUser> requestList { get; set; }
        public IList<ApplicationUser> adminList { get; set; }
        public IList<ApplicationUser> instructorList { get; set; }
        public List<memberItem> memberElementList { get; set; } = new List<memberItem>();
        public List<memberItem> requestElementList { get; set; } = new List<memberItem>();
        


        //Returns list of users pending for instructor/admin roles
        public async Task<IList<ApplicationUser>> PendingUsers() {

            // var pendingRole = await roleManager.FindByNameAsync("Pending");

            var pendingUsers = await userManager.GetUsersInRoleAsync("Pending");

            return pendingUsers;

        }


        // Toggles display between members and requests page
        public string display { get; set; } = "Members";
        public string selectionLineLeftCSS { get; set; } = "left:0%";

        public void ToggleDisplay(int check) {

            if (check == 1) {
                display = "Members";
                selectionLineLeftCSS = "left:0%";
            }
            else {
                display = "Requests";
                selectionLineLeftCSS = "left:50%";
            }

        }


        // Handles confirmation pop-up for deleting a member
        public Confirmation ConfirmationComponent { get; set; }
        public memberItem storedMember { get; set; }

        const string DeleteConfirmationHeader = "Member Deletion";
        const string DeleteConfirmationBody = "Delete this member?";
        const string DeleteConfirmationButtonOne = "";
        const string DeleteConfirmationButtonTwo = "Delete";
        const string DeleteConfirmationButtonThree = "Cancel";

        public void DeleteClicked(memberItem mem) {

            storedMember = mem;
            ConfirmationComponent.Show(true, DeleteConfirmationHeader, DeleteConfirmationBody, DeleteConfirmationButtonOne, DeleteConfirmationButtonTwo, DeleteConfirmationButtonThree);

        }

        public void MemberDeletion() {

            storedMember.DeleteMember(userManager, dbContext, memberElementList);
            Cancel();

        }

        public void Cancel() {

            storedMember = null;
            ConfirmationComponent.Hide();

        }

    }

}
