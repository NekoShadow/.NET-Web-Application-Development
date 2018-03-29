using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Entities
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }
        public string Province { get; set; }
    }
    public enum Role
    {
        普通员工,
        快递员,
        一级主管,
        中级主管,
        高级主管
    }
}
