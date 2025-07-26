using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using NewMission.Database;
using NewMission.Models;
using NewMission.Protos;

namespace NewMission.Services
{
    public class ProjectServices(AppDbContext dbContext, IPrinter printer) : ProjectOptions.ProjectOptionsBase
    {
        private readonly AppDbContext _dbContext = dbContext;
        private readonly IPrinter _printer = printer;

        public async override Task<CreateProjectReply> CreateProject(CreateProjectRequest request, ServerCallContext context)
        {
            var project = new Project();
            _printer.InputParameter("Title");
            project.Title = Console.ReadLine();
            _printer.InputParameter("Description");
            project.Description = Console.ReadLine();
            project.UserId = request.UserId;
            await _dbContext.Projects.AddAsync(project);
            await _dbContext.SaveChangesAsync();
            return await Task.FromResult(new CreateProjectReply
            {
                StudentId = request.UserId,
                Title = project.Title,

            });
        }
        public async override Task<UpdateProjectReply> UpdateProject(UpdateProjectRequest request, ServerCallContext context)
        {
            var project = _dbContext.Projects.FirstOrDefault(x => x.ProjectId == request.ProjectId && x.UserId == request.UserId);
            if (project == null)
            {
                throw new RpcException(new(StatusCode.InvalidArgument, "This project doesn't exist!!"));

            }
            else
            {
                project.Description = request.Description;
                project.Title = request.Title;
                project.CreatedAt = DateTime.Now;
                await _dbContext.SaveChangesAsync();
                return await Task.FromResult(new UpdateProjectReply
                {
                    ProjectId = project.ProjectId,
                    UserId = project.UserId,
                }
                    );

            }
        }
        public async override Task<DeleteProjectReply> DeleteProject ( DeleteProjectRequest request, ServerCallContext context)
        {
            var project = await _dbContext.Projects.FirstOrDefaultAsync(u=>u.UserId == request.UserId && u.ProjectId == request.ProjectId);
            if (project == null)
            {
                throw new RpcException(new(StatusCode.InvalidArgument, "We Can't find any project with these Ids!!"));
            }
            else
            {
                 _dbContext.Remove(project);
                await _dbContext.SaveChangesAsync();
                return await Task.FromResult(new DeleteProjectReply { ProjectId = project.ProjectId,Title=project.Title});
            }
        }
        public async override Task<ReadProjectReply> ReadProject( ReadProjectRequest request, ServerCallContext context)
        {
            var project = _dbContext.Projects.Include(x => x.user).
                FirstOrDefault(u => u.ProjectId == request.ProjectId && u.UserId == request.UserId);
            if (project == null)
            {
                throw new RpcException(new(StatusCode.InvalidArgument, "We can't find a project with these Ids!!"));
            }
            else
            {
                return await Task.FromResult<ReadProjectReply>(new ReadProjectReply
                {
                    UserId = project.UserId,
                    ProjectId = project.ProjectId,
                    Title = project.Title,
                    Description = project.Description,
                    CreateDate = project.CreatedAt.ToLongDateString(),
                });
            }
        }
       
    }
}
