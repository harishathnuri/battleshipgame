namespace Battle.Application.Queries
{
    public interface IRequest
    { }

    public interface IResponse
    { }

    public interface IQuery<Req, Res>
        where Req : IRequest
        where Res : IResponse
    {
        Res Execute(Req request);
    }
}
