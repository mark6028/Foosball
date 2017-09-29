using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foosball.Models
{
    public abstract class Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //TODO: replace ConvertModelToDTO() in BaseApiController to this method
        public BaseDTO ConvertToDTO()
        {
            return JsonConvert.DeserializeObject<BaseDTO>(JsonConvert.SerializeObject(this));
        }
    }
}
