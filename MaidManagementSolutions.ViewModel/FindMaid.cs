using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace MaidManagementSolutions.ViewModel
{
    public class FindMaid
    {
        public string Community { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string[] Language { get; set; }
        public string[] Gender { get; set; }
        public string[] Working { get; set; }
        public string[] Services { get; set; }
        public bool IsSaveSearch { get; set; }
    }
}


//SELECT*
//FROM Maid
//WHERE
//    (Community = :community OR :community = '')
//    AND(Location = :location OR: location = '')
//    AND(Language @> :languages OR: languages IS NULL)
//    AND(Gender @> :genders OR: genders IS NULL)
//    AND(Working @> :workingStatuses OR: workingStatuses IS NULL)
//    AND(Services @> :services OR: services IS NULL)
