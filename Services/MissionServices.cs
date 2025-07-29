using Grpc.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using NewMission.Database;
using NewMission.Models;
using NewMission.Protos;

namespace NewMission.Services
{
    public class MissionServices(AppDbContext dbContext) : MissionOptions.MissionOptionsBase
    {
        private readonly AppDbContext _dbContext = dbContext;
        public async override Task<AddMissionReply> AddMission(AddMissionRequest request, ServerCallContext context)
        {
            var mission = new Mission()
            {
                ProjectId = request.ProjectId,
                Title = request.Title,
                Description = request.Description,
                Status = request.Status,
                Priority = request.Priority,
                DueDate = request.DueDate,
            };
            await _dbContext.Missions.AddAsync(mission);
            await _dbContext.SaveChangesAsync();
            return new AddMissionReply { ProjectId = mission.ProjectId, Title = mission.Title, };
        }
        public async override Task<ReadMissionReply> ReadMission(ReadMissionRequest request, ServerCallContext context)
        {
            var missionReader = await _dbContext.Missions.Include(x => x.project).FirstOrDefaultAsync(x => x.ProjectId == request.ProjectId && x.Id == request.MissionId);
            if (missionReader == null)
            {
                throw new RpcException(new(StatusCode.NotFound, "Invaild Arguments"));

            }
            return new ReadMissionReply
            {
                ProjectId = missionReader.project.ProjectId,
                MissionId = missionReader.Id,
                Title = missionReader.Title,
                Description = missionReader.Description,
                Status = missionReader.Status,
                Priority = missionReader.Priority,
                DueDate = missionReader.DueDate,
            };
        }
        public async override Task<UpdateMissionReply> UpdateMission (UpdateMissionRequest request, ServerCallContext context)
        {
            var mission = await _dbContext.Missions.Include(x => x.project).FirstOrDefaultAsync
                (x => x.Id == request.MissionId && x.project.ProjectId == request.ProjectId);
            if (mission == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Invalid Arguments"));
            }
            else
            {
                mission.Title =  request.Title;
                mission.Description = request.Description;
                mission.Status = request.Status;
                mission.Priority = request.Priority;
                mission.DueDate = request.DueDate; 
            }
            await _dbContext.SaveChangesAsync();
            return new UpdateMissionReply {ProjectId = mission.ProjectId , MissionId = mission.Id};
            
        }
    }
}
