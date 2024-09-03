using TabloidMVC.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace TabloidMVC.Repositories
{
    public interface ITagRepository
    {
        List<Tag> GetAllTags();
        Tag GetTagById(int id);
        //void AddTag(Tag tag);
    }
}