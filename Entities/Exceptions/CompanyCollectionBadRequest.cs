namespace Entities.Exceptions
{
    public sealed class CompanyCollectionBadRequest : BadRequestException
    {
        public CompanyCollectionBadRequest()
                               : base($"Company Collection is null.")
        {
        }
    }
}