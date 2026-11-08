using EmployeeBenefitsWorkflow.Domain.Enums;

namespace EmployeeBenefitsWorkflow.Domain.Entities;

public class RequestDocuments
{
    public int DocId { get; private set; }

    public ReimbursementRequest? Request { get; private set; }

    public EnumDocumentType DocType { get; private set; }

    public string? FileName { get; private set; }

    public RequestDocuments(int id, ReimbursementRequest request, EnumDocumentType doctype, string fileName)
    {
        DocId = id;
        Request = request;
        DocType = doctype;
        FileName = fileName;
    }
}