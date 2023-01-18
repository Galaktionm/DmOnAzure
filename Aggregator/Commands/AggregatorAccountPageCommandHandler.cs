using Aggregator.DTOs;
using MediatR;
using UserProto;

namespace Aggregator.Commands
{
    public class AggregatorAccountPageCommandHandler : IRequestHandler<AggregatorAccountPageCommand, AccountPageUserServiceResponse>
    {
        private UserProto.UserProtoService.UserProtoServiceClient userProtoServiceClient { get; set; }

        public AggregatorAccountPageCommandHandler(UserProto.UserProtoService.UserProtoServiceClient userProtoServiceClient)
        {
            this.userProtoServiceClient = userProtoServiceClient;
        }

        public Task<AccountPageUserServiceResponse> Handle(AggregatorAccountPageCommand request, CancellationToken cancellationToken)
        {           
            var result = userProtoServiceClient.ReturnAccountPageResponse(new AccountPageUserServiceRequestGrpc { UserId=request.userId});
            return Task.FromResult(MapToRecord(result));
        }

        private AccountPageUserServiceResponse MapToRecord(AccountPageUserServiceResponseGrpc grpc)
        {
            var result = new AccountPageUserServiceResponse(grpc.Username, grpc.Email, grpc.Balance);
            return result;
        }
    }
}
