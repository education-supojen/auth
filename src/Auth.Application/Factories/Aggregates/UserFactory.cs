using Auth.Application.Interfaces.Services;
using Auth.Domain.Aggregates;
using Auth.Domain.Factories.Aggregates;
using Auth.Domain.ValueObjects;

namespace Auth.Application.Factories.Aggregates;

public class UserFactory : IUserFactory
{
    private readonly IIdentityProducer _identityProducer;

    public UserFactory(IIdentityProducer identityProducer)
    {
        _identityProducer = identityProducer;
    }
    
    /// <summary>
    /// 註冊新用戶
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public User Create(string email, Password password)
    {
        var id = _identityProducer.GetId();
        return new User(id, email, password);
    }

    /// <summary>
    /// 給 Redis 快取使用的
    /// </summary>
    /// <param name="id"></param>
    /// <param name="email"></param>
    /// <param name="securityStamp"></param>
    /// <param name="refreshToken"></param>
    /// <param name="refreshTokenExpiryTime"></param>
    /// <returns></returns>
    public User ReConstruct(
        long id, 
        string email, 
        string securityStamp, 
        string refreshToken,
        DateTime refreshTokenExpiryTime)
    {
        return new User(id, email, securityStamp, refreshToken, refreshTokenExpiryTime);
    }
}