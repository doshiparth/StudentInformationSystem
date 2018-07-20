using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StudentInformationSystem.Entities.ViewModels
{
    public class StateModel
    {
        public Guid StateID { get; set; }

          [Display(Name = "State Name")]
        public string StateName { get; set; }
    }
}
