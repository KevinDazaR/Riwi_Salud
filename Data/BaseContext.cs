using Microsoft.EntityFrameworkCore;
using Riwi.Models;

namespace RiwiSalud.Data
{
    public class BaseContext :DbContext 
    {

        public BaseContext(DbContextOptions<BaseContext> options) : base(options){}
        
        /* Registro de los modelos que se usan en la db  */
        

    }
}