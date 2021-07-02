namespace Battle.Application.Commands
{
    public interface IRequest
    { }

    public interface IResponse
    { }

    public interface ICommand<Req, Res>
        where Req : IRequest
        where Res : IResponse
    {
        Res Execute(Req request);
    }
}
