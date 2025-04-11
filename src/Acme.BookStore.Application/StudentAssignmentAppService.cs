using Acme.BookStore.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

namespace Acme.BookStore.Assignments
{
    public class StudentAssignmentAppService : ApplicationService
    {
        private readonly IRepository<StudentAssignment, Guid> _repository;
        private readonly IGuidGenerator _guidGenerator;

        public StudentAssignmentAppService(IRepository<StudentAssignment, Guid> repository, IGuidGenerator guidGenerator)
        {
            _repository = repository;
            _guidGenerator = guidGenerator;
        }

        [Authorize(AssignmentPermissions.StudentAssignments.Create)]
        [HttpPost("api/student-assignments")]
        public async Task<StudentAssignmentDto> CreateAsync(CreateUpdateStudentAssignmentDto input)
        {
            try
            {
                var assignment = new StudentAssignment(
                    _guidGenerator.Create(),
                    input.StudentId,
                    input.AssignmentId,
                    input.SubmissionDate
                );

                await _repository.InsertAsync(assignment, autoSave: true);

                return new StudentAssignmentDto
                {
                    Id = assignment.Id,
                    StudentId = assignment.StudentId,
                    AssignmentId = assignment.AssignmentId,
                    SubmissionDate = assignment.SubmissionDate
                };
            }
            catch (UserFriendlyException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw new UserFriendlyException("An error occurred while creating the assignment.");
            }
        }

        [Authorize(AssignmentPermissions.StudentAssignments.Delete)]
        [HttpDelete("api/student-assignments/{id}")]
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var assignment = await _repository.GetAsync(id);
                if (assignment == null)
                {
                    throw new UserFriendlyException("Assignment not found.");
                }

                await _repository.DeleteAsync(id);
            }
            catch (UserFriendlyException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw new UserFriendlyException("An error occurred while deleting the assignment.");
            }
        }

        [Authorize(AssignmentPermissions.StudentAssignments.Get)]
        [HttpGet("api/student-assignments/{id}")]
        public async Task<StudentAssignmentDto> GetAsync(Guid id)
        {
            try
            {
                var assignment = await _repository.GetAsync(id);
                if (assignment == null)
                {
                    throw new UserFriendlyException("Assignment not found.");
                }

                return new StudentAssignmentDto
                {
                    Id = assignment.Id,
                    StudentId = assignment.StudentId,
                    AssignmentId = assignment.AssignmentId,
                    SubmissionDate = assignment.SubmissionDate
                };
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw new UserFriendlyException("An error occurred while retrieving the assignment.");
            }
        }

        [Authorize(AssignmentPermissions.StudentAssignments.Edit)]
        [HttpPut("api/student-assignments/{id}")]
        public async Task<StudentAssignmentDto> UpdateAsync(Guid id, CreateUpdateStudentAssignmentDto input)
        {
            try
            {
                var assignment = await _repository.GetAsync(id);
                if (assignment == null)
                {
                    throw new UserFriendlyException("Assignment not found.");
                }

                assignment.StudentId = input.StudentId;
                assignment.AssignmentId = input.AssignmentId;
                assignment.SubmissionDate = input.SubmissionDate;

                await _repository.UpdateAsync(assignment, autoSave: true);

                return new StudentAssignmentDto
                {
                    Id = assignment.Id,
                    StudentId = assignment.StudentId,
                    AssignmentId = assignment.AssignmentId,
                    SubmissionDate = assignment.SubmissionDate
                };
            }
            catch (UserFriendlyException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                throw new UserFriendlyException("An error occurred while updating the assignment.");
            }
        }
    }
}