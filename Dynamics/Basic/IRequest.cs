namespace Dynamics.Basic
{
    public interface IRequest
    {
        string body { get; set; } // For POST, PATCH and PUT
        string entityName { get; set; } // For all (?) operations 
        string expand { get; set; } // Expand GET queries
        string fieldName { get; set; } // For PUT 
        string filter { get; set; } // Filter by attributes in GET queries
        string recordId { get; set; } // Target single records for GET, PATCH and DELETE
        string select { get; set; } // Select attributes to retrieve in query
    }
}