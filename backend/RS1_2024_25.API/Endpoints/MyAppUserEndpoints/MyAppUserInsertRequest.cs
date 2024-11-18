namespace RS1_2024_25.API.Endpoints.MyAppUserEndpoints
{
    public class MyAppUserInsertRequest
    {
        
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int CityId { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsTutor { get; set; }
        public bool IsStudent { get; set; }
        public string UserType { get; set; }
       
    }
}
