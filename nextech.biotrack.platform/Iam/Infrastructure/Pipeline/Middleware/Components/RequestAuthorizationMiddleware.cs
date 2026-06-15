using nextech.biotrack.platform.Iam.Application.Internal.OutboundServices;
using nextech.biotrack.platform.Iam.Application.QueryServices;
using nextech.biotrack.platform.Iam.Domain.Model.Queries;
using nextech.biotrack.platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;

namespace nextech.biotrack.platform.Iam.Infrastructure.Pipeline.Middleware.Components;

public class RequestAuthorizationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(
        HttpContext context,
        IUserQueryService userQueryService,
        ITokenService tokenService,
        CancellationToken cancellationToken)
    {
        var allowAnonymous = context.Request.HttpContext.GetEndpoint()!
            .Metadata.Any(m => m.GetType() == typeof(AllowAnonymousAttribute));

        if (allowAnonymous)
        {
            await next(context);
            return;
        }

        var token = context.Request.Headers["Authorization"]
            .FirstOrDefault()?.Split(" ").Last();

        if (token == null) throw new Exception("Null or invalid token.");

        var userId = await tokenService.ValidateToken(token);
        if (userId == null) throw new Exception("Invalid token.");

        var user = await userQueryService.Handle(new GetUserByIdQuery(userId.Value), cancellationToken);
        context.Items["User"] = user;

        await next(context);
    }
}
