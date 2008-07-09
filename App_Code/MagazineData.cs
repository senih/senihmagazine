using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Data.SqlClient;

namespace Magazine
{
    public class MagazineData
    {
        public static void CreateIssue(Issue newIssue)
        {
            string status = "Error!";
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection conncetion = new SqlConnection(connectionString);
            string procedure = "CreateIssue";
            SqlCommand cmd = new SqlCommand(procedure, conncetion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@IssueID", SqlDbType.VarChar, 50).Value = newIssue.IssueId;
            cmd.Parameters.Add("@IssueDirectory", SqlDbType.NVarChar, 255).Value = newIssue.IssueDirectory;
            cmd.Parameters.Add("@Resolution", SqlDbType.VarChar, 10).Value = newIssue.Resolution;
            cmd.Parameters.Add("@Quality", SqlDbType.Int).Value = newIssue.Quality;
            cmd.Parameters.Add("@TextAntialiasing", SqlDbType.VarChar, 10).Value = newIssue.TextAntialiasing;
            cmd.Parameters.Add("@GraphAntialiasing", SqlDbType.VarChar, 10).Value = newIssue.GraphicsAntialiasing;

            try
            {
                conncetion.Open();
                int rowsaffected = cmd.ExecuteNonQuery();
                conncetion.Close();
                status = "Succes!";
            }
            catch (Exception ex)
            {
                status = ex.ToString();
            }
        }

        public static Issue OpenIssue(string issueId)
        {
            Issue existingIssue = new Issue();
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection conncetion = new SqlConnection(connectionString);
            string command = string.Format("SELECT * FROM Issues WHERE IssueID='{0}'", issueId);
            SqlCommand cmd = new SqlCommand(command, conncetion);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conncetion.Open();
            reader = cmd.ExecuteReader();
            reader.Read();

            existingIssue.IssueId = reader.GetString(1);
            existingIssue.IssueDirectory = reader.GetString(2);
            existingIssue.Resolution = reader.GetString(3);
            existingIssue.Quality = reader.GetString(4);
            existingIssue.TextAntialiasing = reader.GetString(5);
            existingIssue.GraphicsAntialiasing = reader.GetString(6);

            conncetion.Close();
            return existingIssue;
        }

        public static string UpdateIssue(Issue existingIssue)
        {
            string status = "Error!";
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection conncetion = new SqlConnection(connectionString);
            string procedure = "UpdateIssue";
            SqlCommand cmd = new SqlCommand(procedure, conncetion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@IssueID", SqlDbType.VarChar, 50).Value = existingIssue.IssueId;
            cmd.Parameters.Add("@Resolution", SqlDbType.VarChar, 10).Value = existingIssue.Resolution;
            cmd.Parameters.Add("@Quality", SqlDbType.VarChar, 10).Value = existingIssue.Quality;
            cmd.Parameters.Add("@TextAntialiasing", SqlDbType.VarChar, 10).Value = existingIssue.TextAntialiasing;
            cmd.Parameters.Add("@GraphAntialiasing", SqlDbType.VarChar, 10).Value = existingIssue.GraphicsAntialiasing;
            try
            {
                conncetion.Open();
                int rowsaffected = cmd.ExecuteNonQuery();
                conncetion.Close();
                status = "Issue updated!";
            }
            catch (Exception ex)
            {
                status = ex.ToString();
            }
            return status;
        }

        public static string DeleteIssue(string issueId)
        {
            string status = "Error";
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection conncetion = new SqlConnection(connectionString);
            string command = string.Format("DELETE FROM Issues WHERE IssueID='{0}'", issueId);
            SqlCommand cmd = new SqlCommand(command, conncetion);
            cmd.CommandType = CommandType.Text;
            try
            {
                conncetion.Open();
                int rowsaffected = cmd.ExecuteNonQuery();
                conncetion.Close();
                status = "Issue deleted!";
            }
            catch (Exception ex)
            {
                status = ex.ToString();
            }
            return status;
        }
    }
}
