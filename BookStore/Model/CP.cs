using AutoMapper;

namespace BookStore.Model
{
    public class CP : Profile
    {
        public CP()
            {
            // Mapping properties from Customer to CustomerProfile  
            CreateMap<Customer, CustomerProfile>();
            }   
    }
}
