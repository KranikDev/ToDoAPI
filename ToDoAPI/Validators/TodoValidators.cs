using TodoApi.DTOs;

namespace TodoApi.Validators
{
    public static class TodoValidators
    {
        public static void ValidateCreate(TodoCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                throw new ApplicationException("Title is required.");
        }

        public static void ValidateUpdate(TodoUpdateDto dto)
        {
            if (dto.Title != null && dto.Title.Trim().Length == 0)
                throw new ApplicationException("Title cannot be empty string.");

        }
    }
}
