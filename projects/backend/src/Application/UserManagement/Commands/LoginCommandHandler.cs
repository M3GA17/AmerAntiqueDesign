namespace Application.UserManagement.Commands;

//public class LoginCommandHandler(
//    UserManager<DomainUser> userManager,
//    SignInManager<DomainUser> signInManager,
//    ITokenService tokenService,
//    IApplicationDbContext context) : ICommandHandler<LoginCommand, Result<AuthenticationResult>>
//{
//    private readonly UserManager<DomainUser> userManager = userManager;
//    private readonly SignInManager<DomainUser> signInManager = signInManager;
//    private readonly ITokenService tokenService = tokenService;
//    private readonly IApplicationDbContext context = context;

//    public async Task<Result<AuthenticationResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
//    {
//        var user = await userManager.Users
//            .Include(u => u.RefreshTokens)
//            .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

//        if (user == null)
//            throw new UnauthorizedAccessException("Invalid credentials");

//        var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);

//        if (!result.Succeeded)
//            throw new UnauthorizedAccessException("Invalid credentials");

//        if (!user.IsEnabled)
//            throw new UnauthorizedAccessException("Account is deactivated");

//        // Revoke old refresh tokens
//        user.RefreshTokens.RemoveAll(rt => !rt.IsActive);
//        // Generate new tokens
//        var authResult = await tokenService.GenerateAuthenticationResultAsync(user);

//        // Add refresh token to user
//        var refreshToken = tokenService.GenerateRefreshToken(request.IpAddress);
//        refreshToken.Token = authResult.RefreshToken;
//        refreshToken.ExpiresOn = authResult.RefreshTokenExpiration;

//        user.RefreshTokens.Add(refreshToken);
//        user.LastLoginAt = DateTimeOffset.UtcNow;

//        return authResult;
//    }
//}
