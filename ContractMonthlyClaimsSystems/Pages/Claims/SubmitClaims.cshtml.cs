using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ContractMonthlyClaimsSystems.Pages.Claims
{
 
    public class ClaimsModel : PageModel
    {
        public List<ClaimsInfo>ListClaims=new List<ClaimsInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=labG9AEB3\\SQLEXPRESS;Initial Catalog=ContractMonthlyClaimsSystems;Integrated Security=True;Trust Server Certificate=True";
                using (SqlConnection connection=new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Claims";
                    using (SqlCommand command=new SqlCommand(sql, connection))
                    {
                        using(SqlDataReader reader=command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClaimsInfo claimsInfo = new ClaimsInfo();
                                claimsInfo.id = "" + reader.GetInt32(0);
                                 
                                claimsInfo.HoursWorked=reader.GetString(1);
                                claimsInfo.HourlyRate=reader.GetString(2);
                                claimsInfo.notes=reader.GetString(3);
                               claimsInfo.SupportingDocuments=reader.GetString(4);
                               claimsInfo.SubmissionDate=reader.GetDateTime(5).ToString();

                                ListClaims.Add(claimsInfo);

                               

                            }
                        }
                    }
                }
            }
            catch(Exception ex) {
                Console.WriteLine("Exception:" + ex.ToString());
            }
        }
    }
    public class ClaimsInfo
    {
        public String id;
        public string HoursWorked;
        public string HourlyRate;
        public string notes;
        public string SupportingDocuments;
        public string SubmissionDate;
        internal string? Status;
    }
}
