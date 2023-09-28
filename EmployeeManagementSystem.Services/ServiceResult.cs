using System.Net;

namespace EmployeeManagementSystem.Services
{
    public class ServiceResult<T>
    {
        public T Entity { get; set; } = (T)Activator.CreateInstance(typeof(T));

        private readonly List<string> _errors = new List<string>();

        public HttpStatusCode StatusCode = HttpStatusCode.OK;

        public void AddError(string message)
        {
            _errors.Add(message);
        }

        public void AddErrors(List<string> errors)
        {
            _errors.AddRange(errors);
        }

        public bool HasErrors()
        {
            return _errors.Any();
        }

        public string ErrorMessage => string.Join(", ", _errors);

        public List<string> ErrorList => _errors;
    }
}
