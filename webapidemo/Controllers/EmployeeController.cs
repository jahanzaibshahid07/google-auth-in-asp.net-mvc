using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDataClass;

namespace webapidemo.Controllers
{
    public class EmployeeController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetAllEmployee()
        {
            try 
            {
                using (EmployeeDBEntities enities = new EmployeeDBEntities())
                {

                    var query = enities.Employees.ToList();
                    return Request.CreateResponse(HttpStatusCode.OK, query);

                    //if(salary == "40000")
                    //{
                    //    var query = enities.Employees.Where(x => x.Salary == "40000");
                    //    return Request.CreateResponse(HttpStatusCode.OK,query.ToList());
                    //}
                    //else if (salary == "50000")
                    //{
                    //    var query = enities.Employees.Where(x => x.Salary == "50000");
                    //    return Request.CreateResponse(HttpStatusCode.OK, query.ToList());
                    //}
                    //else 
                    //{
                    //    return Request.CreateErrorResponse(HttpStatusCode.BadRequest,"invalid value");
                    //}
                  
                }
            }
            catch(Exception Ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Ex);

            }
            
        }

        [HttpGet]
        public HttpResponseMessage GetEmployeeById(int id)
        {
            try
            {
                using (EmployeeDBEntities enities = new EmployeeDBEntities())
                {
                    var entity =  enities.Employees.FirstOrDefault(x => x.ID == id);
                    
                    if(entity != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else 
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id =" + id.ToString() + " not found");
                    }
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // frombody means from request body
        [HttpPost]
        public HttpResponseMessage CreateEmployee([FromBody] Employee employee)
        {
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    entities.Employees.Add(employee);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created , employee);
                    message.Headers.Location = new Uri(Request.RequestUri + employee.ID.ToString());
                    return message;
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPut]
        public HttpResponseMessage EditEmployee(int id, [FromBody]Employee employee)
        {
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    var entity = entities.Employees.FirstOrDefault(x => x.ID == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Enployee with id = " + id.ToString() + " not found");
                    }
                    else
                    {
                        entity.Firstname = employee.Firstname;
                        entity.Lastname = employee.Lastname;
                        entity.Designation = employee.Designation;
                        entity.Salary = employee.Salary;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteEmployee(int id)
        {
            try 
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    var entity = entities.Employees.FirstOrDefault(x => x.ID == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id =" + id.ToString() + " not found");
                    }
                    else
                    {
                        entities.Employees.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }

                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
