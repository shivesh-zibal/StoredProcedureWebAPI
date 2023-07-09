using StoredProcedureWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Newtonsoft.Json;

namespace StoredProcedureWebAPI.Controllers
{
    public class ValuesController : ApiController
    {
        Employee emp = new Employee();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["apicon"].ConnectionString);
        // GET api/values
        public string Get(string storedProcedureName,int Id=0)
        {
            if (Id == 0)
            {
                if (storedProcedureName.Trim().ToUpper() == "GETEMPLOYEES")
                {
                    List<Employee> employees = new List<Employee>();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(storedProcedureName, conn);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {

                        foreach (DataRow dr in dt.Rows)
                        {
                            Employee emp = new Employee();
                            emp.Name = dr["Name"].ToString();
                            emp.Id = Convert.ToInt32(dr["Id"]);
                            emp.Age = Convert.ToInt32(dr["Age"]);
                            employees.Add(emp);
                        }
                    }
                    if (employees.Count > 0)
                    {
                        return JsonConvert.SerializeObject(employees);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else
            {
                if (storedProcedureName.Trim().ToUpper() == "GETEMPLOYEEBYID")
                {
                    if (Id != 0)
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(storedProcedureName, conn);
                        dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                        dataAdapter.SelectCommand.Parameters.AddWithValue("@id", Id);
                        DataTable dt = new DataTable();
                        dataAdapter.Fill(dt);
                        Employee emp = new Employee();
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                emp.Name = dr["Name"].ToString();
                                emp.Id = Convert.ToInt32(dr["Id"]);
                                emp.Age = Convert.ToInt32(dr["Age"]);
                            }
                        }
                        if (emp != null)
                        {
                            return JsonConvert.SerializeObject(emp);
                        }
                        else
                        {
                            return "No data found for the given Id";
                        }
                    }
                    else
                    return "Enter any Id greater than 0";
                }
            }
                return "";
        }

        // POST api/values
        public string Post(APIReq req)
        {
            string msg = "";
            if (req.StoredProcName.Trim().ToUpper() == "ADDEMPLOYEE") {
                if (req.Employee != null)
                {
                    SqlCommand cmd = new SqlCommand(req.StoredProcName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@name", req.Employee.Name);
                    cmd.Parameters.AddWithValue("@age", req.Employee.Age);
                    conn.Open();
                    int i = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (i > 0)
                    {
                        msg = "Data inserted successfully";
                    }
                    else
                    {
                        msg = "Error! Some kind of error occured.";
                    }
                }
            }
            return msg;
        }

        // PUT api/values/5
        public string Put(APIReq req)
        {
            string msg = "";
            if (req.StoredProcName.Trim().ToUpper() == "EDITEMPLOYEE")
            {
                if (req.Employee != null)
                {
                    SqlCommand cmd = new SqlCommand(req.StoredProcName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", req.Employee.Id);
                    cmd.Parameters.AddWithValue("@name", req.Employee.Name);
                    cmd.Parameters.AddWithValue("@age", req.Employee.Age);
                    conn.Open();
                    int i = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (i > 0)
                    {
                        msg = "Data edited successfully";
                    }
                    else
                    {
                        msg = "Error! Some kind of error occured.";
                    }
                }
            }
            return msg;
        }
        

        // DELETE api/values/5
        public string Delete(APIReq req)
        {
            string msg = "";
            if (req.StoredProcName.Trim().ToUpper() == "DELETEEMPLOYEE")
            {
                if (req.Id != null && (req.Id??0)!=0)
                {
                    SqlCommand cmd = new SqlCommand(req.StoredProcName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", req.Id);
                    conn.Open();
                    int i = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (i > 0)
                    {
                        msg = "Data deleted successfully";
                    }
                    else
                    {
                        msg = "Error! Some kind of error occured.";
                    }
                }
            }
            return msg;
        }
    }
}
