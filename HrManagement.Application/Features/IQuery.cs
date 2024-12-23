using MediatR;

namespace HrManagement.Application.Features;

public interface IQuery<out TResponse>: IRequest<TResponse>
{
    
}
