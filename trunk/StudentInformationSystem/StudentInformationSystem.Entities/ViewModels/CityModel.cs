using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StudentInformationSystem.Entities.ViewModels
{
    public class CityModel
    {
        public System.Guid CityID { get; set; }

        [Required(ErrorMessage="City Name is requires")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Use letters only")]
        [Display(Name = "City Name")]
        public string CityName { get; set; }
        public System.Guid StateID { get; set; }
       // public string StateName { get; set; }
    
    }
}
