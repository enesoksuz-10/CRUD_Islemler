using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Infrastracture.ExceptionHandling.Exceptions
{
    // Business kaynaklı hataları ayırt etmek için kullanıyoruz.
    public class BusinessException :Exception
    {
        // Mesajı base Exception'a gönderiyoruz.
        public BusinessException(string message) : base(message)
        {
        }
    }
}
//-Business hataları ile sistem hatalarını ayırmak için
//- HTTP 400 dönmek için
//- Exception tipine göre davranmak için
