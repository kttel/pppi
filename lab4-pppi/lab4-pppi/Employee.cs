using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4_pppi
{
    internal class Employee
    {
        public DateTime dateOfRegister = DateTime.Now;
        public string[] activityJournal = Array.Empty<String>();
        protected int id;
        private int salary;
        public Boolean hasMentor = false;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        private string Position { get; set; }
        protected Boolean IsActive { get; set; }
        public int ExperienceYears { get; set; }
        public Employee(string fname, string lname, int age, string position = "None")
        {
            this.FirstName = fname;
            this.LastName = lname;
            this.Age = age;
            this.Position = position;
            this.ExperienceYears = 0;
            this.IsActive = true;
        }
        public string getFullName()
        {
            return $"{this.FirstName} {this.LastName}";
        }
        public void changeAge(int ageDifference = 0)
        {
            this.Age += ageDifference;
        }
        protected void changeActiveStatus()
        {
            this.IsActive = !this.IsActive;
        }
        private void increaseExperience()
        {
            this.ExperienceYears++;
        }
    }
}
