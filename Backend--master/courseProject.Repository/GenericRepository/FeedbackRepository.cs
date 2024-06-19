﻿using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseProject.Repository.GenericRepository
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly projectDbContext dbContext;

        public FeedbackRepository(projectDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task addFeedbackAsync(Feedback feedback)
        {
            await dbContext.feedbacks.AddAsync(feedback);
        }

        public async Task<IReadOnlyList<Feedback>> GetAllFeedbacksAsync()
        {
            return await dbContext.feedbacks.Include(x => x.User).Include(x => x.User.student)
                .OrderByDescending(x=>x.dateOfAdded)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Feedback>> GetFeedbacksByTypeAsync(string type, Guid? instructorId, Guid? courseId)
        {
            // Start with the base query
            var query = dbContext.feedbacks.Include(x => x.User).ThenInclude(x => x.student).Where(x => x.type == type);

            // Apply filters conditionally based on instructorId and courseId
            if (instructorId != null)
            {
                // Filter by instructorId
                query = query.Where(x => x.InstructorId == instructorId);
            }
            else if (courseId != null)
            {
                // Filter by courseId
                query = query.Where(x => x.CourseId == courseId);
            }

            // Execute the query and return the result
            return await query.OrderByDescending(x => x.dateOfAdded).ToListAsync();
        }

        public async Task<Feedback> GetFeedbackByIdAsync(Guid id)
        {
            return await dbContext.feedbacks.Include(x => x.User).ThenInclude(x => x.student).FirstOrDefaultAsync(x => x.Id == id);
        }

    }
}
