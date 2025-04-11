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
using static Acme.BookStore.Permissions.AssignmentPermissions;

namespace Acme.BookStore.Assignments
{
    public class StudentAssignmentAppService : ApplicationService
    {
        private readonly IRepository<StudentAssignment, Guid> _studentAssignmentRepository;
        private readonly IGuidGenerator _guidGenerator;

        public StudentAssignmentAppService(IRepository<StudentAssignment, Guid> studentAssignmentRepository, IGuidGenerator guidGenerator)
        {
            _studentAssignmentRepository = studentAssignmentRepository;
            _guidGenerator = guidGenerator;
        }

        [Authorize(AssignmentPermissions.StudentAssignments.Create)]
        [HttpPost("api/student-assignments")]
        public async Task<StudentAssignmentDto> CreateAsync(CreateUpdateStudentAssignmentDto studentAssignmentDto)
        {
            try
            {
                var studentAssignment= new StudentAssignment(
                    _guidGenerator.Create(),
                    studentAssignmentDto.StudentId,
                    studentAssignmentDto.AssignmentId,
                    studentAssignmentDto.SubmissionDate
                );

                await _studentAssignmentRepository.InsertAsync(studentAssignment);

                return new StudentAssignmentDto
                {
                    Id = studentAssignment.Id,
                    StudentId = studentAssignment.StudentId,
                    AssignmentId = studentAssignment.AssignmentId,
                    SubmissionDate = studentAssignment.SubmissionDate
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
                var assignment = await _studentAssignmentRepository.GetAsync(id);
                if (assignment == null)
                {
                    throw new UserFriendlyException("Assignment not found.");
                }

                await _studentAssignmentRepository.DeleteAsync(id);
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

        [Authorize(AssignmentPermissions.StudentAssignments.GetList)]
        [HttpGet("api/student-assignments/{id}")]
        public async Task<StudentAssignmentDto> GetAsync(Guid id)
        {
            try
            {
                var studentAssignment = await _studentAssignmentRepository.GetAsync(id);
                if (studentAssignment == null)
                {
                    throw new UserFriendlyException("Assignment not found.");
                }

                return new StudentAssignmentDto
                {
                    Id = studentAssignment.Id,
                    StudentId = studentAssignment.StudentId,
                    AssignmentId = studentAssignment.AssignmentId,
                    SubmissionDate = studentAssignment.SubmissionDate
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
        public async Task<StudentAssignmentDto> UpdateAsync(Guid id, CreateUpdateStudentAssignmentDto studentAssignmentDto)
        {
            try
            {
                var studentAssignment = await _studentAssignmentRepository.GetAsync(id);
                if (studentAssignment == null)
                {
                    throw new UserFriendlyException("Assignment not found.");
                }

                studentAssignment.StudentId = studentAssignmentDto.StudentId;
                studentAssignment.AssignmentId = studentAssignmentDto.AssignmentId;
                studentAssignment.SubmissionDate = studentAssignmentDto.SubmissionDate;

                await _studentAssignmentRepository.UpdateAsync(studentAssignment);

                return new StudentAssignmentDto
                {
                    Id = studentAssignment.Id,
                    StudentId = studentAssignment.StudentId,
                    AssignmentId = studentAssignment.AssignmentId,
                    SubmissionDate = studentAssignment.SubmissionDate
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