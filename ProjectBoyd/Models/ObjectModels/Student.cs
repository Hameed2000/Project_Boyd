using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectBoyd.Models.EntityModels;


namespace ProjectBoyd.Models.ObjectModels {
    public class Student {

        public StudentEntity studentEntity { get; set; } = new StudentEntity();

        public Student(string firstName, string lastName, string email) {
            studentEntity.FirstName = firstName;
            studentEntity.LastName = lastName;
            studentEntity.Email = email;
        }

    }
}
