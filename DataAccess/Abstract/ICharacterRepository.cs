using Core.DataAccess;
using Disney_Characters.Models;



namespace DataAccess.Abstract
{
    public interface ICharacterRepository : IEntityRepository<CharacterDto>
    {
       
    }
}
