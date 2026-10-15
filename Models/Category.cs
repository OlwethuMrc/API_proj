using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_proj.Models
{
    public class Category
    {
        public int CategoryId { get; set; }


        public string Name { get; set; } = string.Empty;


        public string Description { get; set; }

    }
}
