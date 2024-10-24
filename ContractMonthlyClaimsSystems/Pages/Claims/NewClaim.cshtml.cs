using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ContractMonthlyClaimsSystems.Pages.Claims
{
    public class NewClaimModel : PageModel
    {
        public ClaimsInfo ClaimsInfo = new ClaimsInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }
        public void OnPost()
        {

            ClaimsInfo.HoursWorked = Request.Form["HoursWorked"];
            ClaimsInfo.HourlyRate = Request.Form["HourlyRate"];
            ClaimsInfo.notes = Request.Form["Notes"];
            ClaimsInfo.SupportingDocuments = Request.Form["Supporting Documents"];

            if (ClaimsInfo.HoursWorked.Length == 0 || ClaimsInfo.HourlyRate.Length == 0 ||
               ClaimsInfo.notes.Length == 0 || ClaimsInfo.SupportingDocuments.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            }

            //Saving Data into database
        
        try
            {
                String connectionString = "Data Source=labG9AEB3\\SQLEXPRESS;Initial Catalog=ContractMonthlyClaimsSystems;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
            using(SqlConnection connection=new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "Insert into claims" + "(HoursWorked,HourlyRate,notes,supporting documents) VALUES" +
                        "(@HoursWorked,@HourlyRate,@notes,@supportingDocuments);";

                    using(SqlCommand command=new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@HoursWorked",ClaimsInfo.HoursWorked);
                        command.Parameters.AddWithValue("@HourlyRate", ClaimsInfo.HourlyRate);
                        command.Parameters.AddWithValue("@notes", ClaimsInfo.notes);
                        command.Parameters.AddWithValue("@supportingDocuments", ClaimsInfo.SupportingDocuments);
command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            ClaimsInfo.HoursWorked = ""; ClaimsInfo.HourlyRate = ""; ClaimsInfo.notes = ""; ClaimsInfo.SupportingDocuments = "";
            successMessage = "new Claims added successfully";
        }
    }
}
        
        

