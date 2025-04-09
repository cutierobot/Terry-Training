namespace TerryTraining.Application.DTO;

public class CustomerDTO
{
    public int Id { get; set; }
    public string GivenName { get; set; }
    public string Surname { get; set; }
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string City { get; set; }
    public string PostCode { get; set; }
    public string Country { get; set; }
    
    public ICollection<OrderDTO> Orders { get; }
}