using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ContractMonthlyClaimsSystems.Pages.Claims
{
    public class StatusCheckModel : PageModel
    {
        public List<ClaimsInfo> ClaimsList { get; set; } = new List<ClaimsInfo>();
        public string ErrorMessage { get; set; } = "";
        public string SuccessMessage { get; set; } = "";

        public void OnGet()
        {
            
        }

        public void OnPost()
        {
            string connectionString = "Data Source=labG9AEB3\\SQLEXPRESS;Initial Catalog=ContractMonthlyClaimsSystems;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Claims WHERE Status = @Status"; // Adjust based on your schema
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Status", Request.Form["Status"]); // Assuming status is passed via form

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClaimsStatus claimsStatus = new ClaimsStatus
                                {
                                    HoursWorked = reader["HoursWorked"].ToString(),
                                    HourlyRate = reader["HourlyRate"].ToString(),
                                    Notes = reader["Notes"].ToString(),
                                    SupportingDocuments = reader["SupportingDocuments"].ToString(),
                                    Status = reader["Status"].ToString() // Assuming there's a Status field
                                };
                               
                            }
                        }
                    }
                }
                SuccessMessage = "Claims retrieved successfully.";
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }

    public class ClaimsStatus
    {
        public string HoursWorked { get; set; }
        public string HourlyRate { get; set; }
        public string Notes { get; set; }
        public string SupportingDocuments { get; set; }
        public string Status { get; set; } // Assuming there's a Status property
    }
}

