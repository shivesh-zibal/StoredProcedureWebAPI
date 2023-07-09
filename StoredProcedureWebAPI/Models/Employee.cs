using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoredProcedureWebAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public int  Age { get; set; }
        public string Name { get; set; }
    }

    public class APIReq
    {
        public string StoredProcName { get; set; }
        public Employee Employee { get; set; }
        public int? Id { get; set; }
       
    }
    public class APIResponse
    {
        public List<Employee> Employees { get; set; }
    }
}