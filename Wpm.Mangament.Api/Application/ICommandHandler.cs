namespace Wpm.Mangament.Api.Application;

public interface ICommandHandler<T>
{
    Task Handle(T command);
    
}