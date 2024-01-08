using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Model
{
    public class CustomerProfile 
    {
        public string? name { get; set;}
        public string? email { get; set; }
        public string?   phone { get; set; }
        public string? address { get; set; }
    }
}
