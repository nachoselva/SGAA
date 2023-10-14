namespace SGAA.Service.Contracts
{
    using SGAA.Models;
    using System.Threading.Tasks;

    public interface IPostulacionService
    {
        Task<PostulacionGetModel> AddPostulacion(PostulacionPostModel model);
    }
}
