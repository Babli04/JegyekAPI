using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Diagnostics.Eventing.Reader;
using jegyek.Models;
using static jegyek.DTOs;


namespace jegyek.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JegyekController : ControllerBase
    {
        Connect connect = new Connect();
        private readonly List<JegyDto> jegyek = new();

        [HttpGet]
        public ActionResult<IEnumerable<JegyDto>> Get()
        {

            try
            {
                connect.connection.Open();

                string sql = "SELECT * FROM jegyek";
                MySqlCommand cmd = new MySqlCommand(sql, connect.connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var result = new JegyDto(reader.GetGuid("Id"), reader.GetInt32("Jegy"), reader.GetString("Leiras"), reader.GetString("Added"));

                    jegyek.Add(result);
                }
                connect.connection.Close();
                return StatusCode(200, jegyek);

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]

        public ActionResult<JegyDto> Get(Guid id)
        {

            try
            {
                connect.connection.Open();

                string sql = "SELECT * FROM jegyek WHERE Id=@Id";
                MySqlCommand cmd = new MySqlCommand(sql, connect.connection);
                cmd.Parameters.AddWithValue("Id", id);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    var result = new JegyDto(reader.GetGuid("Id"), reader.GetInt32("Jegy"), reader.GetString("Leiras"), reader.GetString("Added"));

                    jegyek.Add(result);
                    connect.connection.Close();
                    return StatusCode(200, jegyek);
                }
                else
                {
                    connect.connection.Close();
                    return StatusCode(404, "Szpoás");
                }

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        public ActionResult<Grades> Post(CreateJegyDto createGrade)
        {
            DateTime dateTime = DateTime.Now;
            string time = dateTime.ToString("yyyy-MM-dd:HH:mm:ss");
            var jegyek = new Grades
            {
                Id = Guid.NewGuid(),
                Jegy = createGrade.Jegy,
                Leiras = createGrade.Leiras,
                Added = time
            };

            try
            {
                connect.connection.Open();

                string sql = "INSERT INTO jegyek (`Id`, `Jegy`, `Leiras`, `Added`)" + $"VALUES ('{jegyek.Id}', '{jegyek.Jegy}','{jegyek.Leiras}','{jegyek.Added}')";

                MySqlCommand cmd = new MySqlCommand(sql, connect.connection);
                cmd.ExecuteNonQuery();

                connect.connection.Close();

                return StatusCode(201, jegyek);
            }
            catch (Exception e)
            {

                return BadRequest();
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult<Grades> Put(UpdateJegyDto updateGrade, Guid id)
        {
            try
            {
                connect.connection.Open();

                string sql = "UPDATE `jegyek` SET `Jegy`='@Jegy',`Leiras`='@Leiras' WHERE Id=@Id";

                MySqlCommand cmd = new MySqlCommand(sql, connect.connection);

                cmd.Parameters.AddWithValue("Id", id);
                cmd.Parameters.AddWithValue("Jegy", updateGrade.Jegy);
                cmd.Parameters.AddWithValue("Leiras", updateGrade.Leiras);
                cmd.ExecuteNonQuery();

                connect.connection.Close();

                return StatusCode(201);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            try
            {
                connect.connection.Open();

                string sql = $"DELETE FROM `jegyek` WHERE Id=@Id";

                MySqlCommand cmd = new MySqlCommand( sql, connect.connection);

                cmd.Parameters.AddWithValue("Id", id);
                cmd.ExecuteNonQuery ();

                connect.connection.Close();
                return StatusCode(200);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}