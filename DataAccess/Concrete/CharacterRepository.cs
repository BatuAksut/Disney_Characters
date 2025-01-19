using Disney_Characters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;

namespace DataAccess.Concrete
{
    public class CharacterRepository : EfEntityRepositoryBase<CharacterDto, CharacterContext>,ICharacterRepository
    {
    }
}
