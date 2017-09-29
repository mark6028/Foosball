using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Foosball.Models
{
    public class BaseDTO : Entity
    {
        //TODO: replace ConvertDTOToModel() in BaseApiController to this method
        public Entity ConvertToEntity()
        {
            return JsonConvert.DeserializeObject<Entity>(JsonConvert.SerializeObject(this));
        }
    }

}
