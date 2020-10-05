using ReportGenerationService.Utilities;

namespace ReportGenerationService.ValidationProviders
{
    public interface IValidation<T>
    {
        public HttpResponses Validate(T item);
    }
}
