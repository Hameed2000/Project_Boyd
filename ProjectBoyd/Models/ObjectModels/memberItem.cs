using Microsoft.AspNetCore.Identity;
using ProjectBoyd.Data;
using ProjectBoyd.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBoyd.Models.ObjectModels {
    public class memberItem {

        public Action StateHasChangedDelegate { get; set; }

        public ApplicationUser user;
        public string role = "";

        //Member Constructor
        public memberItem(IList<ApplicationUser> adminList, ApplicationUser instructor) {

            user = instructor;

            if (adminList.Contains(user)) {
                role = "Admin";
            }
            else {
                role = "Instructor";
            }

        }

        //Request Constructor
        public memberItem(ApplicationUser instructor) {

            user = instructor;
            role = user.RoleRequest;

        }

        
        //Enables dropdown for member/request cards
        public string dropdownDisplay = "none";
        public void ToggleDropDown(bool on) {

            if (on == true) {
                dropdownDisplay = "block";
            }
            else {
                dropdownDisplay = "none";
            }

        }


        //Adds user to admin role
        public async void AddAdmin(UserManager<ApplicationUser> userManager) {

            await userManager.AddToRoleAsync(user, "Admin");
            role = "Admin";
            StateHasChangedDelegate.Invoke();

        }

        //Removes user from admin role
        public async void RemoveAdmin(UserManager<ApplicationUser> userManager) {

            await userManager.RemoveFromRoleAsync(user, "Admin");
            role = "Instructor";
            StateHasChangedDelegate.Invoke();

        }


        //Deletes member from intrusctor context, user account, and memberlist
        public async void DeleteMember(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext, List<memberItem> memberElementList) {

            dbContext.Instructors.Remove(dbContext.Instructors.Single(i => i.UserId == user.Id));
            dbContext.SaveChanges();

            if (user != null) {
                await userManager.DeleteAsync(user);
            }

            memberElementList.Remove(this);

            StateHasChangedDelegate.Invoke();

        }


        // Handles requests page

        //Changes role for request page
        public void ChangeRole(int val) {

            if (val == 1) {
                role = "Instructor";
            }
            else {
                role = "Admin";
            }

            StateHasChangedDelegate.Invoke();

        }

        //Accept user requests to become an instructor/admin
        //Removes pending role
        //Adds Instructor/Admin roles, and to instructor dbcontext
        public async void AcceptRequest(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext, List<memberItem> requestElementList, List<memberItem> memberElementList) {

            bool isPending = await userManager.IsInRoleAsync(user, "Pending");

            if (user != null && isPending) {

                if (role == "Admin") {
                    await userManager.AddToRoleAsync(user, "Admin");
                }

                await userManager.AddToRoleAsync(user, "Instructor");
                await userManager.RemoveFromRoleAsync(user, "Pending");

                InstructorEntity instructorEntry = new InstructorEntity { UserId = user.Id };
                dbContext.Instructors.Add(instructorEntry);
                dbContext.SaveChanges();

                requestElementList.Remove(this);
                memberElementList.Add(this);

                StateHasChangedDelegate.Invoke();

            }
            
        }

        //Rejects a user request to become an instructor/admin
        public async void RejectRequest(UserManager<ApplicationUser> userManager, List<memberItem> requestElementList) {

            if (user != null) {
                await userManager.DeleteAsync(user);
            }

            requestElementList.Remove(this);

            StateHasChangedDelegate.Invoke();

        }


    }

}
