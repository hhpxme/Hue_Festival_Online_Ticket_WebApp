using AutoMapper;
using HF_WEB_API.Data;
using HF_WEB_API.Helper;
using HF_WEB_API.Helper.UserServices;
using HF_WEB_API.Models.News;
using Microsoft.EntityFrameworkCore;

namespace HF_WEB_API.Repositories.News
{
    public class NewsRepository : INewsRepository
    {

        private readonly WebDbContext _context;
        private readonly IMapper _mapper;

        public NewsRepository(WebDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<Data.News> AddNewsAsync(AddNewsModel model, string authorId)
        {
            var news = new Data.News()
            {
                Title = model.Title,
                ReleaseDate = DateTime.Now,
                NewsContent = model.NewsContent,
                AccountId = authorId,
            };

            _context.Newses.Add(news);
            await _context.SaveChangesAsync();

            return news;
        }

        public async Task DeleteNewsAsync(int id)
        {
            var news = _context.Newses!.SingleOrDefault(p => p.Id == id);
            if (news != null)
            {
                _context.Newses!.Remove(news);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<NewsModel>> GetAllNewsAsync()
        {
            var newses = await _context.Newses!.ToListAsync();
            return _mapper.Map<List<NewsModel>>(newses);
        }

        public async Task<List<NewsModel>> GetAllNewsSortAsync(bool inc)
        {
            var newses = await _context.Newses!.ToListAsync();
            newses.Sort((dt1, dt2) => DateTime.Compare(dt1.ReleaseDate, dt2.ReleaseDate));
            if (inc)
            {
                return _mapper.Map<List<NewsModel>>(newses);
            }
            else
            {
                newses.Reverse();
                return _mapper.Map<List<NewsModel>>(newses);
            }
        }

        public async Task<NewsModel> GetNewsAsync(int id)
        {
            var news = await _context.Newses!.FindAsync(id);
            return _mapper.Map<NewsModel>(news);
        }

        public async Task UpdateNewsAsync(int id, AddNewsModel model)
        {
            var news = _context.Newses!.SingleOrDefault(p => p.Id == id); 
            if (news != null)
            {
                news.Title = model.Title;
                news.NewsContent = model.NewsContent;
                news.ReleaseDate = DateTime.Now;

                _context.Newses!.Update(news);
                await _context.SaveChangesAsync();
            }
        }
    }
}
