using AspPractice.Models.Domain;
using AspPractice.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace AspPractice.Controllers
{
    public class EmplyController : Controller
    {
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddEmpViewModel model)
        {
            SqlConnection con = new SqlConnection(Helper.Connection);
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into Emply(Name,DeptId,UserName,Password) values('" + model.Name + "','" + model.DeptId + "','" + model.UserName + "','" + model.Password + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("Add");
        }

        [HttpGet]

        public IActionResult List()
        {
            SqlConnection con = new SqlConnection(Helper.Connection);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select Emply.Id, Emply.Name,Dept.Name from Emply join dept on Emply.deptid = dept.id", con);
            SqlDataReader dr = cmd.ExecuteReader();
            List<EmplyDetailsViewModel> emplys = new List<EmplyDetailsViewModel>();
            while (dr.Read())
            {
                EmplyDetailsViewModel emply = new EmplyDetailsViewModel();
                emply.Id = dr.GetInt32(0);
                emply.Name = dr.GetString(1);
                emply.DeptName = dr.GetString(2);
                emplys.Add(emply);
            }
            con.Close();
            return View(emplys);
        }

        public IActionResult Edit(int  id)
        {
            SqlConnection con = new SqlConnection(Helper.Connection);
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Emply where id = " + id, con);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            Emply emply = new Emply();
            emply.Id = dr.GetInt32(0);
            emply.Name = dr.GetString(1);
            emply.DeptId = dr.GetInt32(2);
            emply.UserName = dr.GetString(3);
            emply.Password = dr.GetString(4);
            con.Close();
            return View(emply);
        }

        [HttpPost]
        public IActionResult Edit(Emply model)
        {
            SqlConnection con = new SqlConnection(Helper.Connection);
            con.Open();
            SqlCommand cmd = new SqlCommand("update Emply set Name = '" + model.Name + "', DeptId = '" + model.DeptId + "', UserName = '" + model.UserName + "', Password = '" + model.Password + "' where id = " + model.Id, con);
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("List");
        }

        [HttpPost]

        public IActionResult Delete(Emply emply)
        {
            SqlConnection con = new SqlConnection(Helper.Connection);
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from Emply where id = " + emply.Id, con);
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("List");

        }
    }
}
