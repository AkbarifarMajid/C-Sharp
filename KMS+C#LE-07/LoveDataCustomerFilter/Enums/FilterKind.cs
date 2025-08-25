using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveDataCustomerFilter.Enums
{
    public enum FilterKind
    {
        CityGraz,                  
        AgeUnder30,               
        OrderValueOver100,       
        CategoryElectronics,      
        OrderDateAfter20230101,   
        SortByNameAZ,              
        GroupByCityFlattened,     
        Top3Oldest                
    }
}
