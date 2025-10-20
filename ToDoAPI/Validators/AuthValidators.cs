using TodoApi.DTOs;

namespace TodoApi.Validators
{
    public static class AuthValidators
    {
        public static void ValidateRegister(RegisterDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FullName))
                throw new ApplicationException("FullName is required.");

            if (string.IsNullOrWhiteSpace(dto.Email) || !dto.Email.Contains("@"))
                throw new ApplicationException("Valid Email is required.");

            if (string.IsNullOrWhiteSpace(dto.Password) || dto.Password.Length < 6)
                throw new ApplicationException("Password must be at least 6 characters.");
        }

        public static void ValidateLogin(LoginDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || !dto.Email.Contains("@"))
                throw new ApplicationException("Valid Email is required.");

            if (string.IsNullOrWhiteSpace(dto.Password))
                throw new ApplicationException("Password is required.");
        }
    }
}
