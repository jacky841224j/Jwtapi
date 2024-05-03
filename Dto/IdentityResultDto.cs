using Jwtapi.Enum;

namespace Jwtapi.Dto
{
    public class IdentityResultDto
    {
        public string AccessToken { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;

        public long Expires { get; set; }

        public Guid CompanyID { get; set; }

        public string Account { get; set; } = string.Empty;

        public string NickName { get; set; } = string.Empty;

        public UserTypes UserType { get; set; }
    }
}
