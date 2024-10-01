using AspPractice.Models.Domain;
using AspPractice.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace AspPractice.Controllers
{
    public class DeptController : Controller
    {
        public IActionResult Add()
        {
            return View();
        }

// ###################Create###################

        [HttpPost]
        public IActionResult Add(AddDeptReqViewModel model)
        {
            SqlConnection conn = new SqlConnection(Helper.Connection);
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = conn;
            string sql = $"Insert into Dept(Name) Values('{model.Name}')";
            sqlCommand.CommandText = sql;
            conn.Open();
            sqlCommand.ExecuteNonQuery();
            conn.Close();
            return View();
        }

        // ###################Read###################

        [HttpGet]
        public IActionResult List()
        {

            SqlConnection conn = new SqlConnection(Helper.Connection);

            SqlCommand sqlCommand = new SqlCommand("select * from Dept", conn);

            conn.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            List<Dept> depts = new List<Dept>();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);

                string Name = reader.GetString(1);
                var dept = new Dept { Id = id, Name = Name };
                depts.Add(dept);
            }
            reader.Close();
            conn.Close();
            return View(depts);
        }

        [HttpGet]
        // ###################Update###################
        public IActionResult Edit(int id)
        {
            SqlConnection conn = new SqlConnection(Helper.Connection);
            SqlCommand sqlCommand = new SqlCommand($"select * from Dept where Id={id}", conn);
            conn.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            reader.Read();
            string Name = reader.GetString(1);
            var dept = new Dept { Id = id, Name = Name };
            reader.Close();
            conn.Close();
            return View(dept);
        }

        [HttpPost]
        public IActionResult Edit(Dept dept)
        {
            SqlConnection conn = new SqlConnection(Helper.Connection);
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = conn;
            string sql = $"Update Dept set Name='{dept.Name}' where Id={dept.Id}";
            sqlCommand.CommandText = sql;
            conn.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            reader.Read();
            reader.Close();
            conn.Close();
            return RedirectToAction("List");
        }

        [HttpPost]
        // ###################Delete###################
        public IActionResult Delete(Dept dept)
        {
            SqlConnection conn = new SqlConnection(Helper.Connection);
            string sql = $"Delete from dept Where Id = {dept.Id} ";
            SqlCommand sqlCommand = new SqlCommand(sql, conn);
            conn.Open();
            sqlCommand.ExecuteNonQuery();
            conn.Close();
            return RedirectToAction("List");

        }
    }
}
