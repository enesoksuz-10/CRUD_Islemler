using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Model.Entities
{
    public class User
    {
        //Bu alanda = null! yerine required kullanılabilir ancak BaseEntity Kullanmıyorum sürümüm 6.0 olduğu için de BaseEntity olmadan required kullanılması mümkün değildir. Sürüm yükseltilmelidir.
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string TCKN { get; set; } = null!;
        public int RecordStatus { get; set; }

    }
}
