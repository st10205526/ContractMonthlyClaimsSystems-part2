using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ContractMonthlyClaimsSystems.Pages.Claims
{
    public class CoordinatorsModel : PageModel
    {
        public List<ClaimsInfo> PendingClaims { get; set; } = new List<ClaimsInfo>();
        public string ErrorMessage { get; set; } = "";
        public string SuccessMessage { get; set; } = "";

        public void OnGet()
        {
            LoadPendingClaims();
        }

        private void LoadPendingClaims()
        {
            string connectionString = "Data Source=labG9AEB3\\SQLEXPRESS;Initial Catalog=ContractMonthlyClaimsSystems;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT Id, HoursWorked, HourlyRate, Notes, SupportingDocuments, Status FROM Claims WHERE Status = 'Pending'";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClaimsInfo claim = new ClaimsInfo
                                {
                                   
                                   HoursWorked = reader["HoursWorked"].ToString(),
                                    HourlyRate = reader["HourlyRate"].ToString(),
                                   SupportingDocuments = reader["SupportingDocuments"].ToString(),
                                    Status=reader["Status"].ToString()
                                };
                                PendingClaims.Add(claim);
                            }
                        }
                    }
                }
                SuccessMessage = "Claims loaded successfully.";
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public IActionResult OnPostUpdateStatus(int id, string status)
        {
            string connectionString = "Data Source=labG9AEB3\\SQLEXPRESS;Initial Catalog=ContractMonthlyClaimsSystems;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE Claims SET Status = @Status WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Status", status);
                        command.Parameters.AddWithValue("@Id", id);
                        command.ExecuteNonQuery();
                    }
                }

                SuccessMessage = "Claim status updated successfully.";
                LoadPendingClaims(); // Reloading claims after updates
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            return Page();
        }
    }
}


