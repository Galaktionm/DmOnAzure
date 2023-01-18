using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UserProto;
using UserService.Configurations;
using UserService.Entities;
using UserService.Commands;

namespace UserService.Grpc
{
    public class UserGrpcService : UserProtoService.UserProtoServiceBase
    {
        private readonly UserManager<User> manager;
        private readonly UserDatabaseContext databaseContext;
        private IMediator mediator;
        public UserGrpcService(UserManager<User> manager, UserDatabaseContext databaseContext, IMediator mediator)
        {
            this.manager = manager;
            this.databaseContext = databaseContext;
            this.mediator = mediator;
         }

        public async override Task<OrderBalancetValidationResult> ValidateOrderBalance(OrderBalanceValidationRequest request, ServerCallContext context)
        {
            var user = await manager.FindByIdAsync(request.UserId);
            if (user.Balance >= request.Price)
            {
                return new OrderBalancetValidationResult
                {
                    OrderId = request.OrderId,
                    Result = true
                };
            }

            return new OrderBalancetValidationResult
            {
                OrderId = request.OrderId,
                Result = false
            };
        }

        public async override Task<AccountPageUserServiceResponseGrpc> ReturnAccountPageResponse(AccountPageUserServiceRequestGrpc request, ServerCallContext context)
        {
            try
            {
                var command =new AccountPageCommand(request);
                var result = await mediator.Send(command);
                return result;
            } catch (ArgumentException ex)
            {
                return new AccountPageUserServiceResponseGrpc { };
            }
    
        }
    }
}
