using HF_WEB_API.Data;
using HF_WEB_API.Models.News;

namespace HF_WEB_API.Repositories.News
{
    public interface INewsRepository
    {
        public Task<List<NewsModel>> GetAllNewsAsync();
        public Task<List<NewsModel>> GetAllNewsSortAsync(bool inc);
        public Task<NewsModel> GetNewsAsync(int id);
        public Task<Data.News> AddNewsAsync(AddNewsModel model, string authorId);
        public Task UpdateNewsAsync(int id, AddNewsModel model);

        //public Task UpdateNewsAsync(int id, bool isApprove);

        public Task DeleteNewsAsync(int id);
    }
}
