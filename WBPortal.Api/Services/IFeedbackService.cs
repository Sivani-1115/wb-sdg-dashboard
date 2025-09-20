using WBPortal.Api.Models;
using System.Collections.Generic;

namespace WBPortal.Api.Services
{
    public interface IFeedbackService
    {
        IEnumerable<Feedback> GetFeedbacksByProject(int projectId);
        void CreateFeedback(Feedback feedback);
    }
}
