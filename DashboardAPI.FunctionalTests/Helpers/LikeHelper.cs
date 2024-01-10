using System.Net.Http;
using DashboardAPI.Models.DTOs.Like;
using DashboardDBAccess.Data;

namespace DashboardAPI.FunctionalTests.Helpers
{
    public class LikeHelper : AEntityHelper<GetLikeDto, AddLikeDto, UpdateLikeDto>
    {
        public LikeHelper(HttpClient client, string baseUrl = "/likes") : base(baseUrl, client)
        {
        }

        public override bool Equals(UpdateLikeDto first, GetLikeDto second)
        {
            if (first == null || second == null)
                return false;
            return first.User == second.User &&
                   first.Post == second.Post &&
                   first.LikeableType == second.LikeableType &&
                   first.Comment == second.Comment;
        }

        public override bool Equals(UpdateLikeDto first, UpdateLikeDto second)
        {
            if (first == null || second == null)
                return false;
            return first.User == second.User &&
                   first.Post == second.Post &&
                   first.LikeableType == second.LikeableType &&
                   first.Comment == second.Comment;
        }

        public override bool Equals(GetLikeDto first, GetLikeDto second)
        {
            if (first == null || second == null)
                return false;
            return first.User == second.User &&
                   first.Post == second.Post &&
                   first.LikeableType == second.LikeableType &&
                   first.Comment == second.Comment;
        }

        public override UpdateLikeDto GenerateTUpdate(int id, GetLikeDto entity)
        {
            return new UpdateLikeDto {Id = id, LikeableType = LikeableType.Post, Post = 1, User = entity.User};
        }
    }
}
