using HrManagement.Domain.Shared.Dtos;

namespace HrManagement.Application.Features.AppUser.Commands.Login;

public record LoginAppUserCommandResponse(ServiceResult<TokenDto> Result);