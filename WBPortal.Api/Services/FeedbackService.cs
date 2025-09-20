using WBPortal.Api.Data;
using WBPortal.Api.Models;
using Microsoft.EntityFrameworkCore;


namespace WBPortal.Api.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly AppDbContext _context;
        public FeedbackService(AppDbContext context) => _context = context;

        public IEnumerable<Feedback> GetFeedbacksByProject(int projectId) =>
            _context.Feedbacks.Where(f => f.ProjectId == projectId).Include(f => f.User).ToList();

        public void CreateFeedback(Feedback feedback)
        {
            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();
        }
    }
}
