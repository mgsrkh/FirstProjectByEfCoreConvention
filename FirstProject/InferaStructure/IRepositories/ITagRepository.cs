using FirstProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstProject.InferaStructure.IRepositories
{
    public interface ITagRepository
    {
        List<Tag> GetAll();
        Tag GetById(int id);
        int Insert(Tag tag);
        int Update(Tag tag);
        int Delete(Tag tag);
        int DeleteById(int id);
    }
}
