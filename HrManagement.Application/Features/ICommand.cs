using MediatR;

namespace HrManagement.Application.Features;

public interface ICommand<out TResponse>: IRequest<TResponse>
{
    
}