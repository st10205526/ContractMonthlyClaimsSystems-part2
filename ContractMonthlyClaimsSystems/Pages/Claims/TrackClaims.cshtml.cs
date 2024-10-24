using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using System.Data.SqlClient;
using System.Security.Claims;

namespace ContractMonthlyClaimsSystems.Pages.Claims
{

    public class TrackClaimsModel : PageModel
    {
        public List<ClaimInfo> Claims { get; set; } = new List<ClaimInfo>();
        public string ErrorMessage { get; set; } = "";
        public string SuccessMessage { get; set; } = "";

        public void OnGet()
        {
            LoadClaims();
        }

        private void LoadClaims()
        {
            try
            {
                string connectionString = "Data Source=labG9AEB3\\SQLEXPRESS;Initial Catalog=ContractMonthlyClaimsSystems;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT Id, HoursWorked, HourlyRate, Notes, SupportingDocuments, Status FROM Claims";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Claims.Add(new ClaimInfo
                                {
                                    Id = reader.GetInt32(0),
                                    HoursWorked = reader.GetInt32(1),
                                    HourlyRate = reader.GetDecimal(2),
                                    Notes = reader.GetString(3),
                                    SupportingDocuments = reader.GetString(4),
                                    Status = reader.GetString(5)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public IActionResult OnPostUpdateStatus(int id, string status)
        {
            try
            {
                string connectionString = "Data Source=labG9AEB3\\SQLEXPRESS;Initial Catalog=ContractMonthlyClaimsSystems;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
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
                LoadClaims(); // Reload claims to reflect the updated status
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            return Page();
        }
    }

    public class ClaimInfo
    {
        public int Id { get; set; }
        public int HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public string Notes { get; set; }
        public string SupportingDocuments { get; set; }
        public string Status { get; set; }
    }
}




